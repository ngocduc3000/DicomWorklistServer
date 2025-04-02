using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // Thêm namespace này
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FellowOakDicom.Network;
using Newtonsoft.Json;
using static worklist_server.DatabaseHelper;

namespace worklist_server
{
    public partial class MainForm : Form
    {
        private IDicomServer _dicomServer;
        private const int DEFAULT_PORT = 104;
        private Dictionary<string, string> modalities;
        private const string CONFIG_FILE = "modalities.json";
        private BindingList<KeyValuePair<string, string>> bindingList;

        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private ToolStripMenuItem serverMenuItem;
        private bool isServerRunning = true; // Trạng thái server
        private string lastNotificationMessage;

        public MainForm()
        {
            InitializeComponent();
            loadconfigsystem();
            StartServer();
        }
        private void InitializeTrayIcon()
        {
            if (trayIcon == null)
            {
                // Tạo menu chuột phải
                trayMenu = new ContextMenuStrip();
                trayMenu.Items.Add("Show Dicom Worklist Server", null, (s, e) => ShowFromTray());

                // Tạo menu Start/Stop server
                serverMenuItem = new ToolStripMenuItem("Stop server \uf28d", null, ToggleServer);
                trayMenu.Items.Add(serverMenuItem);



                // Thêm mục "Help" để mở FormReset
                trayMenu.Items.Add("Help", null, (s, e) => ShowResetForm());
                trayMenu.Items.Add("Exit", null, (s, e) => ExitWithConfirm());

                // Tạo biểu tượng system tray
                trayIcon = new NotifyIcon()
                {
                    Icon = new Icon("app.ico"), // Đường dẫn tương đối
                    Text = "\uf0f8 DICOM Worklist Server",
                    ContextMenuStrip = trayMenu,
                    Visible = true
                };

                // Xử lý sự kiện click
                trayIcon.BalloonTipClicked += (s, e) => ShowFromTray();
                trayIcon.DoubleClick += (s, e) => ShowFromTray();
            }

        }

        private void ShowResetForm()
        {
            FormResetTrial resetForm = new FormResetTrial();
            resetForm.ShowDialog(); // Mở form ở dạng modal (người dùng phải đóng trước khi tiếp tục)
        }

        private void ToggleServer(object sender, EventArgs e)
        {
            if (isServerRunning)
            {
                StopServerWithConfirm();
                serverMenuItem.Text = "Start Server \uf04b"; // Unicode ▶ (play)
            }
            else
            {
                StartServer();
                serverMenuItem.Text = "Stop Server \uf28d"; // Unicode ⏹ (stop)
            }

            isServerRunning = !isServerRunning;
        }
        protected override void OnLoad(EventArgs e)
        {
            InitializeTrayIcon();
            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            // Ẩn vào system tray khi minimize
            if (this.WindowState == FormWindowState.Minimized)
            {
                HideToTray();
            }

            base.OnResize(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Chặn đóng form thông thường
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                HideToTray();
            }

            base.OnFormClosing(e);
        }
        private void HideToTray()
        {
            this.Hide();
            this.ShowInTaskbar = false;
            if(ConfigManager.Settings.Feature.IsEnableNotice)
            trayIcon.ShowBalloonTip(100, "DICOM Server", "Worklist Server running in systembar", ToolTipIcon.Info);

        }

        private void ShowFromTray()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void StopServerWithConfirm()
        {
            if (MessageBox.Show("Stop Server. Are you sure ?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                
                    _dicomServer?.Dispose();
                    btnStart.Enabled = true;
                    btstop.Enabled = false;
                   btaddmodality.Enabled = true;


                lblLog.Text = this.Text = $" Stopping... DICOM Worklist Server";
              
            }
        }

        private void ExitWithConfirm()
        {
            if (MessageBox.Show("Stop Server and Exit Server. Are you sure?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _dicomServer?.Dispose();
                ExitApplication();
            }
        }

        private void ExitApplication()
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        private Dictionary<string, string> LoadModalitiesFromConfig(string filePath)
        {
            if (!File.Exists(filePath))
            {
                var defaultModalities = new Dictionary<string, string>
                {
                    { "MODALITY_AE", "localhost" }
                };
                File.WriteAllText(filePath, JsonConvert.SerializeObject(defaultModalities, Formatting.Indented));
                return defaultModalities;
            }
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(filePath)) ?? new Dictionary<string, string>();
        }


        private void LoadModalitiesToDataGridView()
        {
            modalities = new Dictionary<string, string>();
            modalities = LoadModalitiesFromConfig(CONFIG_FILE);

            // Đọc dữ liệu từ JSON nếu có
            if (File.Exists(CONFIG_FILE))
            {
                string json = File.ReadAllText(CONFIG_FILE);
                modalities = JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            }

            // Khởi tạo bindingList và gán vào DataGridView
            bindingList = new BindingList<KeyValuePair<string, string>>(modalities.ToList());
            BindingSource bindingSource = new BindingSource { DataSource = bindingList };
            dataGridView1.DataSource = bindingSource;

            // Đặt tên cột
            dataGridView1.Columns[0].HeaderText = "Modality Name";
            dataGridView1.Columns[1].HeaderText = "IP Address";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Thêm cột nút Xóa nếu chưa tồn tại
            if (!dataGridView1.Columns.Contains("Delete"))
            {
                var deleteButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    HeaderText = "Action",
                    Text = "❌ Xóa",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Flat,
                    Width = 80  // Fixed width cho đẹp
                };

                deleteButtonColumn.DefaultCellStyle.BackColor = Color.LightCoral;
                deleteButtonColumn.DefaultCellStyle.ForeColor = Color.White;

                dataGridView1.Columns.Add(deleteButtonColumn);

                // Đăng ký sự kiện click (chỉ 1 lần)
                dataGridView1.CellContentClick += HandleDeleteButtonClick;
            }
        }

        private void HandleDeleteButtonClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra có phải click vào cột Delete không
            if (e.RowIndex < 0 || e.ColumnIndex != dataGridView1.Columns["Delete"].Index)
                return;

            // Xác nhận trước khi xóa
            if (MessageBox.Show("Are you sure?", "Confirm",
                MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            // Xóa khỏi BindingList
            bindingList.RemoveAt(e.RowIndex);

            // Cập nhật ngay vào file JSON
            SaveDataToJson();

            MessageBox.Show("Khởi động lại server để áp dụng.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveDataToJson()
        {
            if (bindingList == null) return; // Tránh lỗi nếu bindingList chưa khởi tạo
            Dictionary<string, string> updatedData = bindingList.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            File.WriteAllText(CONFIG_FILE, JsonConvert.SerializeObject(updatedData, Formatting.Indented));
        }


        public static List<string> GetAllLocalIPAddresses()
        {
            var ipList = new List<string>();
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    var ipProps = ni.GetIPProperties();

                    foreach (UnicastIPAddressInformation ip in ipProps.UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipList.Add(ip.Address.ToString());
                        }
                    }
                }
            }
            return ipList;
        }

        private void StartServer()
        {
            LoadModalitiesToDataGridView();
            WorklistService.OnLogReceived += AppendLog;

            btnStart.Enabled = false;
            btstop.Enabled = true;
            btaddmodality.Enabled = false;
          //  dataGridView1.Enabled = false;
          //  tabControl1.TabPages.Remove(tabconfig);
            try
            {
                if (_dicomServer == null || !_dicomServer.IsListening)
                {
                    // Cập nhật danh sách Modality trước khi khởi động Server
                    WorklistService.SetAllowedModalities(modalities);
                    if (String.IsNullOrEmpty(ConfigManager.Settings.Registry.AppName)) ConfigManager.Settings.Registry.AppName = "MWL_SCP";
                    WorklistService.ServerAETitle = ConfigManager.Settings.Registry.AppName;

                    // Khởi động DICOM Server
                    _dicomServer = DicomServerFactory.Create<WorklistService>(DEFAULT_PORT);
                  //  AppendLog($" DICOM Worklist Server IP: {_dicomServer.IPAddress}:{DEFAULT_PORT} - Name:{WorklistService.ServerAETitle}");
                    var ipList = GetAllLocalIPAddresses();
                    foreach (var ip in ipList)
                    {
                        //   AppendLog($" DICOM Worklist Server IP: {ip}:{DEFAULT_PORT} - Name: {WorklistService.ServerAETitle}");
                        lblLog.Text= this.Text = $" Starting DICOM Worklist Server IP: {ip}:{DEFAULT_PORT} - Name: {WorklistService.ServerAETitle}";
                  
                    }
                }
            }
            catch (Exception ex)
            {
                AppendLog($"❌ Error starting DICOM Worklist Server: {ex.Message}");
            }
        }

        private void AppendLog(string message)
        {
            string requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                // 1. Xác định đường dẫn thư mục và file log
                string logDirectory = "logs";
                string logFileName = $"log_{DateTime.Now:yyyyMMdd}.log";
                string logFilePath = Path.Combine(logDirectory, logFileName);

                // 2. Đảm bảo thư mục tồn tại
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                // 3. Tạo nội dung log với timestamp
                string logEntry = message;

                // 4. Ghi vào file log (tự động tạo file nếu chưa có)
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);

                // 5. Hiển thị lên UI (nếu cần)
                if (richTextBox1.InvokeRequired)
                {
                    richTextBox1.Invoke(new Action(() =>
                    {
                        richTextBox1.AppendText(logEntry + Environment.NewLine);
                        richTextBox1.ScrollToCaret();
                    }));
                }
                else
                {
                    richTextBox1.AppendText(logEntry + Environment.NewLine);
                    richTextBox1.ScrollToCaret();
                }
                if (chkEnableNotice.Checked)
                {
                    // Nếu ứng dụng đang minimized và có log mới
                    if (this.WindowState == FormWindowState.Minimized || !this.Visible)
                    {
                        lastNotificationMessage = message;
                        ShowNewLogNotification();
                    }
                }

            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
               MessageBox.Show($"Lỗi khi ghi log: {ex.Message}");
            }
        }

        private void ShowNewLogNotification()
        {
            string truncatedMessage = lastNotificationMessage.Length > 50
         ? lastNotificationMessage.Substring(0, 50) + "..."
         : lastNotificationMessage;
            if (ConfigManager.Settings.Feature.IsEnableNotice)
                trayIcon.ShowBalloonTip(
                1000, // Hiển thị trong 3 giây
                "DICOM Server - Thông báo mới",
                truncatedMessage,
                ToolTipIcon.Info
            );
        }

        private void btnStart_Click(object sender, EventArgs e) => StartServer();
        private void btnStop_Click(object sender, EventArgs e)
        {
            StopServerWithConfirm();
          
        }
         
        

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại nhập dữ liệu
            string modalityName = Microsoft.VisualBasic.Interaction.InputBox("Nhập tên Modality:", "Thêm mới");
            if (string.IsNullOrWhiteSpace(modalityName)) return;

            string ipAddress = Microsoft.VisualBasic.Interaction.InputBox("Nhập IP Address:", "Thêm mới");
            if (string.IsNullOrWhiteSpace(ipAddress)) return;

            // Kiểm tra nếu Modality đã tồn tại
            if (bindingList.Any(item => item.Key == modalityName))
            {
                MessageBox.Show("Modality đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }          
            // Thêm vào danh sách
            bindingList.Add(new KeyValuePair<string, string>(modalityName, ipAddress));
            // Lưu vào JSON
            SaveDataToJson();
            MessageBox.Show("Khởi động lại server để áp dụng.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadMappingToDataGridView();
        }

        private void ckNotice_CheckedChanged(object sender, EventArgs e)
        {
            // Cập nhật giá trị trong cấu hình
            ConfigManager.Settings.Feature.IsEnableNotice = chkEnableNotice.Checked;

            // Lưu thay đổi vào file JSON
            ConfigManager.SaveConfig();

        }

        private void btSaveSQL_Click(object sender, EventArgs e)
        {
            // Validate dữ liệu
if (string.IsNullOrWhiteSpace(txtServer.Text) || 
    string.IsNullOrWhiteSpace(txtDatabase.Text))
{
    MessageBox.Show("Server và Database không được trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    return;
}

try
{
    // Tạo connection string
    var builder = new System.Data.SqlClient.SqlConnectionStringBuilder
    {
        DataSource = txtServer.Text,
        InitialCatalog = txtDatabase.Text,
        UserID = string.IsNullOrWhiteSpace(txtUsername.Text) ? null : txtUsername.Text,
        Password = string.IsNullOrWhiteSpace(txtPassword.Text) ? null : txtPassword.Text,
        IntegratedSecurity = string.IsNullOrWhiteSpace(txtUsername.Text) // Dùng Windows Auth nếu không có username
    };

    // Thử kết nối trước khi lưu
    using (var connection = new System.Data.SqlClient.SqlConnection(builder.ConnectionString))
    {
        connection.Open();  // Mở kết nối (ném ra exception nếu thất bại)
        connection.Close(); // Đóng kết nối nếu thành công
    }

    // Lưu cấu hình nếu kết nối thành công
    ConfigManager.Settings.Database.ConnectionString = builder.ConnectionString;
    ConfigManager.SaveConfig();

    MessageBox.Show("Lưu thành công! Vui lòng khởi động lại ứng dụng.", 
                   "Thành công", 
                   MessageBoxButtons.OK, 
                   MessageBoxIcon.Information);
}
catch (System.Data.SqlClient.SqlException sqlEx)
{
    MessageBox.Show($"Không thể kết nối đến CSDL:\n{sqlEx.Message}", 
                   "Lỗi kết nối", 
                   MessageBoxButtons.OK, 
                   MessageBoxIcon.Error);
}
catch (Exception ex)
{
    MessageBox.Show($"Lỗi không xác định:\n{ex.Message}", 
                   "Lỗi", 
                   MessageBoxButtons.OK, 
                   MessageBoxIcon.Error);
}
        }
        private void LoadConnectionSettings()
        {
            // Giải mã chuỗi kết nối
            string connStr = ConfigManager.Settings.Database.ConnectionString;

            // Phân tích chuỗi (dùng SqlConnectionStringBuilder)
            var builder = new System.Data.SqlClient.SqlConnectionStringBuilder(connStr);

            // Hiển thị lên TextBox
            txtServer.Text = builder.DataSource;
            txtDatabase.Text = builder.InitialCatalog;
            txtUsername.Text = builder.UserID;
            txtPassword.Text = builder.Password;
        }
        private void LoadDatabaseSelection()
        {
           // MessageBox.Show(ConfigManager.Settings.SelectedDatabase?.ToLower() + ConfigManager.Settings.IsEnableNotice.ToString());
            switch (ConfigManager.Settings.Database.SelectedDatabase?.ToLower())
            {
                case "sqlserver":
                    chkSelectSQLServer.Checked = true;
                    break;
                case "postgresql":
                   // radMySQL.Checked = true;
                    break;
                case "oracle":
                   // radOracle.Checked = true;
                    break;
                default:
                   // radSQLServer.Checked = true; // Mặc định
                    break;
            }
        }
        private void loadconfigsystem()
        {
            chkEnableNotice.Checked = ConfigManager.Settings.Feature.IsEnableNotice;
            txtAETitle.Text = ConfigManager.Settings.Registry.AppName;

            LoadDatabaseSelection();
            txtTable.Text = DicomQueryConfig.Table;
        }
        private void chkSelectSQLServer_CheckedChanged(object sender, EventArgs e)
        {
            LoadConnectionSettings();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Nhập tên bảng mới từ TextBox
            string newTable = txtTable.Text;

            // Cập nhật vào config
         //   var config = DicomQueryConfig.LoadConfig();
            DicomQueryConfig.Table = newTable;
            DicomQueryConfig.SaveConfig();
        }

        private void LoadMappingToDataGridView()
        {
            // Xóa dữ liệu cũ
            dataGridView2.Rows.Clear();

            // Thiết lập cột nếu chưa có
            if (dataGridView2.Columns.Count == 0)
            {
                dataGridView2.Columns.Add("DICOMField", "DICOMTag");
                dataGridView2.Columns.Add("DBField", "DataField");

                // Thêm cột nút xóa
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.Name = "Delete";
                btnDelete.Text = "Delete";
                btnDelete.UseColumnTextForButtonValue = true;
                dataGridView2.Columns.Add(btnDelete);
            }

            // Đổ dữ liệu từ Dictionary vào DataGridView
            foreach (var item in DicomQueryConfig.Mapping)
            {
                int rowIndex = dataGridView2.Rows.Add();
                dataGridView2.Rows[rowIndex].Cells["DICOMField"].Value = item.Key;
                dataGridView2.Rows[rowIndex].Cells["DBField"].Value = item.Value;
            }

            // Tùy chỉnh giao diện
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btaddMapping_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Nhập thông tin mapping
                string dicomTag = Microsoft.VisualBasic.Interaction.InputBox("Nhập DICOM Tag (VD: (0010,0020)):", "Thêm mapping mới");
                if (string.IsNullOrWhiteSpace(dicomTag)) return;

                string dbField = Microsoft.VisualBasic.Interaction.InputBox($"Nhập tên trường CSDL tương ứng cho {dicomTag}:", "Ánh xạ trường");
                if (string.IsNullOrWhiteSpace(dbField)) return;

                // 2. Kiểm tra DICOM Tag hợp lệ
                if (!Regex.IsMatch(dicomTag, @"^\([0-9A-Fa-f]{4},[0-9A-Fa-f]{4}\)$"))
                {
                    MessageBox.Show("DICOM Tag phải có dạng (XXXX,XXXX)", "Lỗi định dạng",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 3. Kiểm tra trùng lặp
                if (DicomQueryConfig.Mapping.ContainsKey(dicomTag))
                {
                    MessageBox.Show($"DICOM Tag {dicomTag} đã tồn tại!", "Lỗi",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. Thêm mapping và condition
                DicomQueryConfig.AddMappingWithCondition(dicomTag, dbField);
                DicomQueryConfig.SaveConfig();

                // 5. Cập nhật giao diện
                LoadMappingToDataGridView();

                MessageBox.Show($"Đã thêm {dicomTag} → {dbField}. Xin khởi động lại server.", "Thành công",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm mapping: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                string dicomTag = dataGridView2.Rows[e.RowIndex].Cells["DICOMField"].Value.ToString();

                if (MessageBox.Show($"Xóa mapping {dicomTag}?", "Xác nhận",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Gọi phương thức xóa mới
                    if (DicomQueryConfig.RemoveMappingWithCondition(dicomTag))
                    {
                        DicomQueryConfig.SaveConfig();
                        LoadMappingToDataGridView();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtAETitle.Text)) ConfigManager.Settings.Registry.AppName = txtAETitle.Text;
            ConfigManager.SaveConfig();
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Lấy giá trị DicomTag và DbField từ dòng được chỉnh sửa
            string dicomTag = dataGridView2.Rows[e.RowIndex].Cells["DICOMField"].Value?.ToString();
            string dbField = dataGridView2.Rows[e.RowIndex].Cells["DBField"].Value?.ToString();

            if (string.IsNullOrEmpty(dicomTag) || string.IsNullOrEmpty(dbField))
            {
                MessageBox.Show("Giá trị không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Cập nhật Mapping Dictionary
            if (DicomQueryConfig.Mapping.ContainsKey(dicomTag))
            {
                DicomQueryConfig.Mapping[dicomTag] = dbField;
            }
            else
            {
                DicomQueryConfig.Mapping.Add(dicomTag, dbField);
            }

            // Ghi lại vào file JSON
            DicomQueryConfig.SaveConfig();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}



