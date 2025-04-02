using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom;
using FellowOakDicom.Network;
using Microsoft.Extensions.Logging;

namespace worklist_server
{
    public class WorklistService : DicomService, IDicomServiceProvider, IDicomCFindProvider
    {
        public static string ServerAETitle;// Đặt AE Title
        public static event Action<string> OnLogReceived; // Sự kiện gửi log lên MainForm
        private DatabaseHelper _dbHelper;

        private readonly string _remoteIP; // Lưu IP của modality
        private string _AE_modality; // Lưu IP của modality
        private static Dictionary<string, string> _allowedModalities;
        public static void Log(string message)
        {
            OnLogReceived?.Invoke(message);
        }
        public static void SetAllowedModalities(Dictionary<string, string> allowedModalities)
        {
            _allowedModalities = allowedModalities;
        }

        public WorklistService(INetworkStream stream, Encoding encoding, ILogger logger, DicomServiceDependencies dependencies)
       : base(stream, encoding, logger, dependencies)
        {
           
            _remoteIP = stream.RemoteHost;
            _dbHelper = new DatabaseHelper();

        }

        public async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            _AE_modality = association.CallingAE;
            string requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


            if (_allowedModalities == null || !_allowedModalities.ContainsKey(_AE_modality) || _allowedModalities[_AE_modality] != _remoteIP)
            {
                OnLogReceived?.Invoke($"[{requestTime}] - [{_AE_modality}-{_remoteIP}]: Rejected: Unauthorized modality");
                await SendAssociationRejectAsync(
                    DicomRejectResult.Permanent,
                    DicomRejectSource.ServiceUser,
                    DicomRejectReason.CallingAENotRecognized);
                return;
            }

            if (!string.Equals(association.CalledAE, ServerAETitle, StringComparison.OrdinalIgnoreCase))
            {
                WorklistService.OnLogReceived?.Invoke($"[{requestTime}] - [{_AE_modality}-{_remoteIP}]: Rejected: Unknown Called AE Title {association.CalledAE}");
                await SendAssociationRejectAsync(
                    DicomRejectResult.Permanent,
                    DicomRejectSource.ServiceUser,
                    DicomRejectReason.CalledAENotRecognized);
                return;
            }

            foreach (var context in association.PresentationContexts)
            {
                WorklistService.OnLogReceived?.Invoke($"[{requestTime}] - [{_AE_modality}-{_remoteIP}]: {context.AbstractSyntax}");
                context.SetResult(DicomPresentationContextResult.Accept);
            }

            await SendAssociationAcceptAsync(association);
        }

        public Task OnReceiveAssociationReleaseRequestAsync()
        {
             return SendAssociationReleaseResponseAsync();
        }

        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            string requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            WorklistService.OnLogReceived?.Invoke($"[{requestTime}] - [{_AE_modality}-{_remoteIP}]: Association aborted by {source}, reason: {reason}");
        }

        public void OnConnectionClosed(Exception exception)
        {
            string requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (exception != null)
            {
                WorklistService.OnLogReceived?.Invoke($"[{requestTime}] - [{_AE_modality}-{_remoteIP}]: Connection closed with error: {exception.Message}");
            }
            else
            {
                WorklistService.OnLogReceived?.Invoke($"[{requestTime}] - [{_AE_modality}-{_remoteIP}]: Connection closed normally.");
            }
        }
        public async IAsyncEnumerable<DicomCFindResponse> OnCFindRequestAsync(DicomCFindRequest request)
        {
            string requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (request.Dataset != null)
            {
                StringBuilder requestTags = new StringBuilder();
                requestTags.AppendLine(" DICOM Request Tags:");

                foreach (var item in request.Dataset)
                {
                    string tagName = item.Tag.DictionaryEntry?.Name ?? "Unknown Tag";
                    string tagValue = request.Dataset.GetSingleValueOrDefault(item.Tag, "N/A");
                    requestTags.AppendLine($"{tagName}: {tagValue}");
                }

                // Gửi toàn bộ log lên sự kiện hiển thị (giả sử có một RichTextBox tên richTextBoxLog)
                WorklistService.OnLogReceived?.Invoke($"[{requestTime}] - [{_AE_modality}-{_remoteIP}]:{requestTags}");
            }
            // Khởi tạo _dbHelper nếu chưa có
            if (_dbHelper == null)
            {
                _dbHelper = new DatabaseHelper();
            }
            if (request.Dataset == null)
            {
                WorklistService.OnLogReceived?.Invoke($"[ {requestTime}] - [{_AE_modality}-{_remoteIP}]: No Dataset received in C-FIND request.");
                yield return new DicomCFindResponse(request, DicomStatus.QueryRetrieveUnableToProcess);
                yield break;
            }
            bool isUnicodeSupported = request.Dataset.TryGetSingleValue(DicomTag.SpecificCharacterSet, out string charset)
                             && charset == "ISO_IR 192";
             List<DicomDataset> worklistItems = await Task.Run(() => _dbHelper.GetMatchingProceduresSQL(request.Dataset));
         
            foreach (var item in worklistItems)
            {
               // string patientName = item.TryGetString(DicomTag.PatientName, out string tempName) ? tempName : "UNKNOWN";
               // if (!isUnicodeSupported)
               // {
               //     patientName = RemoveDiacritics(patientName).Trim();
               // }
               // item.AddOrUpdate(DicomTag.PatientName, patientName);
               //item.AddOrUpdate(DicomTag.SpecificCharacterSet, isUnicodeSupported ? "ISO_IR 192" : "ISO_IR 100");
              //  WorklistService.OnLogReceived?.Invoke($"[{requestTime}] - [{_AE_modality}-{_remoteIP}]: PatientName in DICOM Dataset: {patientName}");
                yield return new DicomCFindResponse(request, DicomStatus.Pending) { Dataset = item };
            }
            yield return new DicomCFindResponse(request, DicomStatus.Success);
        }
        public static string RemoveDiacritics(string text)  // Xoa dau
        {
            if (string.IsNullOrEmpty(text)) return text;

            string normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char c in normalized)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

  }

}
