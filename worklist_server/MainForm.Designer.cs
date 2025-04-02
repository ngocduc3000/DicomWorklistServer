namespace worklist_server
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btstop = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblLog = new System.Windows.Forms.Label();
            this.btaddmodality = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabconfig = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtAETitle = new System.Windows.Forms.TextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btaddMapping = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTable = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkSelectSQLServer = new System.Windows.Forms.CheckBox();
            this.btSaveSQL = new System.Windows.Forms.Button();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.chkEnableNotice = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabconfig.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // _refreshTimer
            // 
            this._refreshTimer.Interval = 1000;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Coral;
            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(82, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "▶️ Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(9, 51);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(628, 503);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // btstop
            // 
            this.btstop.Enabled = false;
            this.btstop.Location = new System.Drawing.Point(100, 12);
            this.btstop.Name = "btstop";
            this.btstop.Size = new System.Drawing.Size(90, 23);
            this.btstop.TabIndex = 7;
            this.btstop.Text = "⏹️ Stop";
            this.btstop.UseVisualStyleBackColor = true;
            this.btstop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(540, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Clear logs";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabconfig);
            this.tabControl1.Location = new System.Drawing.Point(12, 41);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1011, 586);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.AliceBlue;
            this.tabPage1.Controls.Add(this.lblLog);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.btaddmodality);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1003, 560);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "System Logs";
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(15, 22);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(31, 13);
            this.lblLog.TabIndex = 12;
            this.lblLog.Text = "[Log]";
            // 
            // btaddmodality
            // 
            this.btaddmodality.Enabled = false;
            this.btaddmodality.Location = new System.Drawing.Point(900, 22);
            this.btaddmodality.Name = "btaddmodality";
            this.btaddmodality.Size = new System.Drawing.Size(97, 23);
            this.btaddmodality.TabIndex = 11;
            this.btaddmodality.Text = "Add Modality";
            this.btaddmodality.UseVisualStyleBackColor = true;
            this.btaddmodality.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(640, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "List of Modality Avaible";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(643, 51);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(354, 503);
            this.dataGridView1.TabIndex = 1;
            // 
            // tabconfig
            // 
            this.tabconfig.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabconfig.Controls.Add(this.linkLabel1);
            this.tabconfig.Controls.Add(this.label7);
            this.tabconfig.Controls.Add(this.button3);
            this.tabconfig.Controls.Add(this.txtAETitle);
            this.tabconfig.Controls.Add(this.tabControl2);
            this.tabconfig.Controls.Add(this.chkEnableNotice);
            this.tabconfig.Location = new System.Drawing.Point(4, 22);
            this.tabconfig.Name = "tabconfig";
            this.tabconfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabconfig.Size = new System.Drawing.Size(1003, 560);
            this.tabconfig.TabIndex = 1;
            this.tabconfig.Text = "Configuration";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(436, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "AE Title";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(642, 42);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 31;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtAETitle
            // 
            this.txtAETitle.Location = new System.Drawing.Point(498, 44);
            this.txtAETitle.Name = "txtAETitle";
            this.txtAETitle.Size = new System.Drawing.Size(138, 20);
            this.txtAETitle.TabIndex = 30;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Location = new System.Drawing.Point(40, 71);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(902, 458);
            this.tabControl2.TabIndex = 19;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btaddMapping);
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.txtTable);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.chkSelectSQLServer);
            this.tabPage3.Controls.Add(this.btSaveSQL);
            this.tabPage3.Controls.Add(this.txtDatabase);
            this.tabPage3.Controls.Add(this.txtUsername);
            this.tabPage3.Controls.Add(this.txtServer);
            this.tabPage3.Controls.Add(this.txtPassword);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(894, 432);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "SQL Database";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btaddMapping
            // 
            this.btaddMapping.Location = new System.Drawing.Point(788, 23);
            this.btaddMapping.Name = "btaddMapping";
            this.btaddMapping.Size = new System.Drawing.Size(100, 23);
            this.btaddMapping.TabIndex = 29;
            this.btaddMapping.Text = "Add DicomTag";
            this.btaddMapping.UseVisualStyleBackColor = true;
            this.btaddMapping.Click += new System.EventHandler(this.btaddMapping_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(444, 51);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(444, 375);
            this.dataGridView2.TabIndex = 28;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEndEdit);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(679, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 27;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(441, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Table Get Data";
            // 
            // txtTable
            // 
            this.txtTable.Location = new System.Drawing.Point(535, 24);
            this.txtTable.Name = "txtTable";
            this.txtTable.Size = new System.Drawing.Size(138, 20);
            this.txtTable.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Database";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Server";
            // 
            // chkSelectSQLServer
            // 
            this.chkSelectSQLServer.AutoSize = true;
            this.chkSelectSQLServer.Location = new System.Drawing.Point(33, 28);
            this.chkSelectSQLServer.Name = "chkSelectSQLServer";
            this.chkSelectSQLServer.Size = new System.Drawing.Size(130, 17);
            this.chkSelectSQLServer.TabIndex = 20;
            this.chkSelectSQLServer.Text = "SQL Database Server";
            this.chkSelectSQLServer.UseVisualStyleBackColor = true;
            this.chkSelectSQLServer.CheckedChanged += new System.EventHandler(this.chkSelectSQLServer_CheckedChanged);
            // 
            // btSaveSQL
            // 
            this.btSaveSQL.Location = new System.Drawing.Point(130, 189);
            this.btSaveSQL.Name = "btSaveSQL";
            this.btSaveSQL.Size = new System.Drawing.Size(75, 23);
            this.btSaveSQL.TabIndex = 14;
            this.btSaveSQL.Text = "Save";
            this.btSaveSQL.UseVisualStyleBackColor = true;
            this.btSaveSQL.Click += new System.EventHandler(this.btSaveSQL_Click);
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(130, 93);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(250, 20);
            this.txtDatabase.TabIndex = 15;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(130, 121);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(250, 20);
            this.txtUsername.TabIndex = 17;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(130, 65);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(250, 20);
            this.txtServer.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(130, 149);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(250, 20);
            this.txtPassword.TabIndex = 16;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(894, 432);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "PostSQL";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(894, 432);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "API";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // chkEnableNotice
            // 
            this.chkEnableNotice.AutoSize = true;
            this.chkEnableNotice.Location = new System.Drawing.Point(44, 37);
            this.chkEnableNotice.Name = "chkEnableNotice";
            this.chkEnableNotice.Size = new System.Drawing.Size(97, 17);
            this.chkEnableNotice.TabIndex = 0;
            this.chkEnableNotice.Text = "Turn on Notice";
            this.chkEnableNotice.UseVisualStyleBackColor = true;
            this.chkEnableNotice.CheckedChanged += new System.EventHandler(this.ckNotice_CheckedChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(840, 55);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(95, 13);
            this.linkLabel1.TabIndex = 30;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Show Query String";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1035, 639);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btstop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Worklist Server";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabconfig.ResumeLayout(false);
            this.tabconfig.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer _refreshTimer;
        private System.Windows.Forms.Button btnStart;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btstop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabconfig;
        private System.Windows.Forms.Button btaddmodality;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.CheckBox chkEnableNotice;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Button btSaveSQL;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkSelectSQLServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTable;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btaddMapping;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtAETitle;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

