using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace worklist_server
{
   
    internal static class Program
    {
        private static Mutex mutex = new Mutex(true, "DICOM_Worklist_Server");
        private const int TrialDays = 30;

        [STAThread]
        static void Main()
        {
            // Kiểm tra nếu ứng dụng đã chạy
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("App is running....!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Thoát chương trình nếu đã có instance khác chạy
            }

            // Kiểm tra thời gian sử dụng
            if (IsTrialExpired())
            {
                MessageBox.Show("Trial period has expired! Please reset the application.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormResetTrial());
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            mutex.ReleaseMutex();
        }

        private static bool IsTrialExpired()
        {

            if (String.IsNullOrEmpty(ConfigManager.Settings.Registry.RegistryPath) || String.IsNullOrEmpty(ConfigManager.Settings.Registry.InstallDateKey))
            {
                ConfigManager.Settings.Registry.RegistryPath = @"SOFTWARE\DICOM_Worklist_Server";
                ConfigManager.Settings.Registry.InstallDateKey = "InstallDate";
                ConfigManager.SaveConfig();
            }

                // Kiểm tra xem khóa Registry có tồn tại không
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(ConfigManager.Settings.Registry.RegistryPath, true))
                {
                    if (key == null)
                    {
                        // Nếu chưa có, tạo mới khóa Registry để lưu ngày cài đặt
                        using (RegistryKey newKey = Registry.CurrentUser.CreateSubKey(ConfigManager.Settings.Registry.RegistryPath))
                        {
                            newKey.SetValue(ConfigManager.Settings.Registry.InstallDateKey, DateTime.Now.ToBinary()); // Lưu ngày cài đặt
                        }
                        return false; // Chưa hết hạn vì mới cài
                    }

                    // Đọc ngày cài đặt từ Registry
                    object value = key.GetValue(ConfigManager.Settings.Registry.InstallDateKey);
                    if (value == null)
                    {
                        return false; // Nếu không có giá trị, coi như chưa hết hạn
                    }

                    long installDateBinary = Convert.ToInt64(value);
                    DateTime installDate = DateTime.FromBinary(installDateBinary);
                    TimeSpan elapsed = DateTime.Now - installDate;

                    return elapsed.TotalDays > TrialDays; // Trả về true nếu hết hạn
                }

        }
    }
}
