namespace worklist_server
{
    partial class FormResetTrial
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
            this.btnReset = new System.Windows.Forms.Button();
            this.lblTrialInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Enabled = false;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.btnReset.Location = new System.Drawing.Point(120, 137);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(295, 120);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "RESET TRIAL";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btaddmodality_Click);
            // 
            // lblTrialInfo
            // 
            this.lblTrialInfo.AutoSize = true;
            this.lblTrialInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lblTrialInfo.ForeColor = System.Drawing.Color.Blue;
            this.lblTrialInfo.Location = new System.Drawing.Point(123, 93);
            this.lblTrialInfo.Name = "lblTrialInfo";
            this.lblTrialInfo.Size = new System.Drawing.Size(86, 31);
            this.lblTrialInfo.TabIndex = 13;
            this.lblTrialInfo.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(75, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(385, 31);
            this.label1.TabIndex = 14;
            this.label1.Text = "Phiên bản thử nghiệm và demo";
            // 
            // FormResetTrial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 321);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTrialInfo);
            this.Controls.Add(this.btnReset);
            this.Name = "FormResetTrial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblTrialInfo;
        private System.Windows.Forms.Label label1;
    }
}