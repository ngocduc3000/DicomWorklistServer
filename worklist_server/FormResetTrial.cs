using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace worklist_server
{
    public partial class FormResetTrial : Form
    {
        private string RegistryPath = "";
        private string InstallDateKey = "";
        private const int TrialDays = 30; // Số ngày dùng thử
        public FormResetTrial()
        {
            InitializeComponent();
            LoadTrialInfo();

        }
        private void LoadTrialInfo()
        {
            RegistryPath = ConfigManager.Settings.Registry.RegistryPath;
            if (!String.IsNullOrEmpty(RegistryPath))
            {
                int daysLeft = GetDaysLeft();
                lblTrialInfo.Text = daysLeft > 0
                    ? $"Còn {daysLeft} ngày dùng thử."
                    : "Trial đã hết hạn!";

                // Chỉ bật nút reset nếu hết hạn
                btnReset.Enabled = (daysLeft <= 0);
            }
        }

        private int GetDaysLeft()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath, true) ??
                                     Registry.CurrentUser.CreateSubKey(RegistryPath))
            {
                object value = key.GetValue(InstallDateKey);
                if (value == null)
                {
                    // Nếu chưa có, lưu ngày cài đặt lần đầu
                    key.SetValue(InstallDateKey, DateTime.Now.ToBinary());
                    return TrialDays;
                }

                long installDateBinary = Convert.ToInt64(value);
                DateTime installDate = DateTime.FromBinary(installDateBinary);
                TimeSpan elapsed = DateTime.Now - installDate;

                return Math.Max(0, TrialDays - (int)elapsed.TotalDays); // Không cho về số âm
            }
        }
        private void btaddmodality_Click(object sender, EventArgs e)
        {
            ResetTrial();
            MessageBox.Show("Trial period has been reset!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        private void ResetTrial()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath, true))
            {
                if (key != null)
                {
                    key.DeleteValue(InstallDateKey, false);
                    key.SetValue(InstallDateKey, DateTime.Now.ToBinary()); // Cập nhật lại ngày mới
                    Application.Restart();
                }
            }
        }
    }
}
