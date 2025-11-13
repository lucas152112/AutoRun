namespace AutoRun
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvSchedules;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtExe;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtArgs;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.CheckBox chkMon;
        private System.Windows.Forms.CheckBox chkTue;
        private System.Windows.Forms.CheckBox chkWed;
        private System.Windows.Forms.CheckBox chkThu;
        private System.Windows.Forms.CheckBox chkFri;
        private System.Windows.Forms.CheckBox chkSat;
        private System.Windows.Forms.CheckBox chkSun;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnAddUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblExe;
        private System.Windows.Forms.Label lblArgs;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.GroupBox grpDays;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.CheckBox chkRunScheduler;
        private System.Windows.Forms.CheckBox chkMinToTray;
        private System.Windows.Forms.CheckBox chkRunAtStartup;
        private System.Windows.Forms.CheckBox chkAutoStartOnMin;
        private System.Windows.Forms.CheckBox chkEveryDay;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvSchedules = new DataGridView();
            txtName = new TextBox();
            txtExe = new TextBox();
            btnBrowse = new Button();
            txtArgs = new TextBox();
            dtpTime = new DateTimePicker();
            chkMon = new CheckBox();
            chkTue = new CheckBox();
            chkWed = new CheckBox();
            chkThu = new CheckBox();
            chkFri = new CheckBox();
            chkSat = new CheckBox();
            chkSun = new CheckBox();
            chkEnabled = new CheckBox();
            btnNew = new Button();
            btnAddUpdate = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            lblName = new Label();
            lblExe = new Label();
            lblArgs = new Label();
            lblTime = new Label();
            grpDays = new GroupBox();
            lstLog = new ListBox();
            lblLog = new Label();
            chkRunScheduler = new CheckBox();
            chkMinToTray = new CheckBox();
            chkRunAtStartup = new CheckBox();
            chkAutoStartOnMin = new CheckBox();
            chkEveryDay = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dgvSchedules).BeginInit();
            grpDays.SuspendLayout();
            SuspendLayout();
            // 
            // dgvSchedules
            // 
            dgvSchedules.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSchedules.ColumnHeadersHeight = 29;
            dgvSchedules.Location = new Point(15, 215);
            dgvSchedules.Margin = new Padding(4);
            dgvSchedules.MultiSelect = false;
            dgvSchedules.Name = "dgvSchedules";
            dgvSchedules.ReadOnly = true;
            dgvSchedules.RowHeadersVisible = false;
            dgvSchedules.RowHeadersWidth = 51;
            dgvSchedules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSchedules.Size = new Size(939, 466);
            dgvSchedules.TabIndex = 0;
            dgvSchedules.SelectionChanged += dgvSchedules_SelectionChanged;
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtName.Location = new Point(90, 15);
            txtName.Margin = new Padding(4);
            txtName.Name = "txtName";
            txtName.Size = new Size(457, 27);
            txtName.TabIndex = 1;
            // 
            // txtExe
            // 
            txtExe.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtExe.Location = new Point(90, 52);
            txtExe.Margin = new Padding(4);
            txtExe.Name = "txtExe";
            txtExe.Size = new Size(813, 27);
            txtExe.TabIndex = 2;
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowse.Location = new Point(912, 52);
            btnBrowse.Margin = new Padding(4);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(42, 29);
            btnBrowse.TabIndex = 3;
            btnBrowse.Text = "...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // txtArgs
            // 
            txtArgs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtArgs.Location = new Point(90, 89);
            txtArgs.Margin = new Padding(4);
            txtArgs.Name = "txtArgs";
            txtArgs.Size = new Size(863, 27);
            txtArgs.TabIndex = 4;
            // 
            // dtpTime
            // 
            dtpTime.CustomFormat = "HH:mm";
            dtpTime.Format = DateTimePickerFormat.Custom;
            dtpTime.Location = new Point(90, 125);
            dtpTime.Margin = new Padding(4);
            dtpTime.Name = "dtpTime";
            dtpTime.ShowUpDown = true;
            dtpTime.Size = new Size(91, 27);
            dtpTime.TabIndex = 5;
            // 
            // chkMon
            // 
            chkMon.AutoSize = true;
            chkMon.Location = new Point(13, 28);
            chkMon.Margin = new Padding(4);
            chkMon.Name = "chkMon";
            chkMon.Size = new Size(46, 23);
            chkMon.TabIndex = 6;
            chkMon.Text = "一";
            chkMon.UseVisualStyleBackColor = true;
            // 
            // chkTue
            // 
            chkTue.AutoSize = true;
            chkTue.Location = new Point(69, 28);
            chkTue.Margin = new Padding(4);
            chkTue.Name = "chkTue";
            chkTue.Size = new Size(46, 23);
            chkTue.TabIndex = 7;
            chkTue.Text = "二";
            chkTue.UseVisualStyleBackColor = true;
            // 
            // chkWed
            // 
            chkWed.AutoSize = true;
            chkWed.Location = new Point(126, 28);
            chkWed.Margin = new Padding(4);
            chkWed.Name = "chkWed";
            chkWed.Size = new Size(46, 23);
            chkWed.TabIndex = 8;
            chkWed.Text = "三";
            chkWed.UseVisualStyleBackColor = true;
            // 
            // chkThu
            // 
            chkThu.AutoSize = true;
            chkThu.Location = new Point(183, 28);
            chkThu.Margin = new Padding(4);
            chkThu.Name = "chkThu";
            chkThu.Size = new Size(46, 23);
            chkThu.TabIndex = 9;
            chkThu.Text = "四";
            chkThu.UseVisualStyleBackColor = true;
            // 
            // chkFri
            // 
            chkFri.AutoSize = true;
            chkFri.Location = new Point(239, 28);
            chkFri.Margin = new Padding(4);
            chkFri.Name = "chkFri";
            chkFri.Size = new Size(46, 23);
            chkFri.TabIndex = 10;
            chkFri.Text = "五";
            chkFri.UseVisualStyleBackColor = true;
            // 
            // chkSat
            // 
            chkSat.AutoSize = true;
            chkSat.Location = new Point(296, 28);
            chkSat.Margin = new Padding(4);
            chkSat.Name = "chkSat";
            chkSat.Size = new Size(46, 23);
            chkSat.TabIndex = 11;
            chkSat.Text = "六";
            chkSat.UseVisualStyleBackColor = true;
            // 
            // chkSun
            // 
            chkSun.AutoSize = true;
            chkSun.Location = new Point(352, 28);
            chkSun.Margin = new Padding(4);
            chkSun.Name = "chkSun";
            chkSun.Size = new Size(46, 23);
            chkSun.TabIndex = 12;
            chkSun.Text = "日";
            chkSun.UseVisualStyleBackColor = true;
            // 
            // chkEnabled
            // 
            chkEnabled.AutoSize = true;
            chkEnabled.Location = new Point(206, 128);
            chkEnabled.Margin = new Padding(4);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(61, 23);
            chkEnabled.TabIndex = 13;
            chkEnabled.Text = "啟用";
            chkEnabled.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            btnNew.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNew.Location = new Point(555, 125);
            btnNew.Margin = new Padding(4);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(96, 29);
            btnNew.TabIndex = 14;
            btnNew.Text = "新增";
            btnNew.UseVisualStyleBackColor = true;
            btnNew.Click += btnNew_Click;
            // 
            // btnAddUpdate
            // 
            btnAddUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddUpdate.Location = new Point(660, 125);
            btnAddUpdate.Margin = new Padding(4);
            btnAddUpdate.Name = "btnAddUpdate";
            btnAddUpdate.Size = new Size(96, 29);
            btnAddUpdate.TabIndex = 15;
            btnAddUpdate.Text = "加入/更新";
            btnAddUpdate.UseVisualStyleBackColor = true;
            btnAddUpdate.Click += btnAddUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.Location = new Point(764, 125);
            btnDelete.Margin = new Padding(4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(96, 29);
            btnDelete.TabIndex = 16;
            btnDelete.Text = "刪除";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Location = new Point(868, 125);
            btnSave.Margin = new Padding(4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(96, 29);
            btnSave.TabIndex = 17;
            btnSave.Text = "存檔";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(15, 19);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 19);
            lblName.TabIndex = 18;
            lblName.Text = "名稱";
            // 
            // lblExe
            // 
            lblExe.AutoSize = true;
            lblExe.Location = new Point(15, 56);
            lblExe.Margin = new Padding(4, 0, 4, 0);
            lblExe.Name = "lblExe";
            lblExe.Size = new Size(39, 19);
            lblExe.TabIndex = 19;
            lblExe.Text = "程式";
            // 
            // lblArgs
            // 
            lblArgs.AutoSize = true;
            lblArgs.Location = new Point(15, 92);
            lblArgs.Margin = new Padding(4, 0, 4, 0);
            lblArgs.Name = "lblArgs";
            lblArgs.Size = new Size(39, 19);
            lblArgs.TabIndex = 20;
            lblArgs.Text = "參數";
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(15, 130);
            lblTime.Margin = new Padding(4, 0, 4, 0);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(39, 19);
            lblTime.TabIndex = 21;
            lblTime.Text = "時間";
            // 
            // grpDays
            // 
            grpDays.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpDays.Controls.Add(chkEveryDay);
            grpDays.Controls.Add(chkMon);
            grpDays.Controls.Add(chkTue);
            grpDays.Controls.Add(chkWed);
            grpDays.Controls.Add(chkThu);
            grpDays.Controls.Add(chkFri);
            grpDays.Controls.Add(chkSat);
            grpDays.Controls.Add(chkSun);
            grpDays.Location = new Point(15, 162);
            grpDays.Margin = new Padding(4);
            grpDays.Name = "grpDays";
            grpDays.Padding = new Padding(4);
            grpDays.Size = new Size(942, 51);
            grpDays.TabIndex = 22;
            grpDays.TabStop = false;
            grpDays.Text = "星期";
            // 
            // lstLog
            // 
            lstLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lstLog.FormattingEnabled = true;
            lstLog.ItemHeight = 19;
            lstLog.Location = new Point(962, 209);
            lstLog.Margin = new Padding(4);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(415, 460);
            lstLog.TabIndex = 23;
            // 
            // lblLog
            // 
            lblLog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblLog.AutoSize = true;
            lblLog.Location = new Point(965, 186);
            lblLog.Margin = new Padding(4, 0, 4, 0);
            lblLog.Name = "lblLog";
            lblLog.Size = new Size(69, 19);
            lblLog.TabIndex = 24;
            lblLog.Text = "執行紀錄";
            // 
            // chkRunScheduler
            // 
            chkRunScheduler.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkRunScheduler.AutoSize = true;
            chkRunScheduler.Location = new Point(442, 128);
            chkRunScheduler.Margin = new Padding(4);
            chkRunScheduler.Name = "chkRunScheduler";
            chkRunScheduler.Size = new Size(106, 23);
            chkRunScheduler.TabIndex = 25;
            chkRunScheduler.Text = "啟動排程器";
            chkRunScheduler.UseVisualStyleBackColor = true;
            chkRunScheduler.CheckedChanged += chkRunScheduler_CheckedChanged;
            // 
            // chkMinToTray
            // 
            chkMinToTray.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkMinToTray.AutoSize = true;
            chkMinToTray.Location = new Point(589, 19);
            chkMinToTray.Margin = new Padding(4);
            chkMinToTray.Name = "chkMinToTray";
            chkMinToTray.Size = new Size(136, 23);
            chkMinToTray.TabIndex = 26;
            chkMinToTray.Text = "最小化至系統匣";
            chkMinToTray.UseVisualStyleBackColor = true;
            // 
            // chkRunAtStartup
            // 
            chkRunAtStartup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkRunAtStartup.AutoSize = true;
            chkRunAtStartup.Location = new Point(782, 19);
            chkRunAtStartup.Margin = new Padding(4);
            chkRunAtStartup.Name = "chkRunAtStartup";
            chkRunAtStartup.Size = new Size(121, 23);
            chkRunAtStartup.TabIndex = 27;
            chkRunAtStartup.Text = "開機自動啟動";
            chkRunAtStartup.UseVisualStyleBackColor = true;
            chkRunAtStartup.CheckedChanged += chkRunAtStartup_CheckedChanged;
            // 
            // chkAutoStartOnMin
            // 
            chkAutoStartOnMin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkAutoStartOnMin.AutoSize = true;
            chkAutoStartOnMin.Location = new Point(945, 19);
            chkAutoStartOnMin.Margin = new Padding(4);
            chkAutoStartOnMin.Name = "chkAutoStartOnMin";
            chkAutoStartOnMin.Size = new Size(166, 23);
            chkAutoStartOnMin.TabIndex = 28;
            chkAutoStartOnMin.Text = "最小化自動啟動排程";
            chkAutoStartOnMin.UseVisualStyleBackColor = true;
            // 
            // chkEveryDay
            // 
            chkEveryDay.AutoSize = true;
            chkEveryDay.Location = new Point(878, 26);
            chkEveryDay.Margin = new Padding(4);
            chkEveryDay.Name = "chkEveryDay";
            chkEveryDay.Size = new Size(61, 23);
            chkEveryDay.TabIndex = 29;
            chkEveryDay.Text = "每天";
            chkEveryDay.UseVisualStyleBackColor = true;
            chkEveryDay.CheckedChanged += chkEveryDay_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1394, 697);
            Controls.Add(chkAutoStartOnMin);
            Controls.Add(chkRunAtStartup);
            Controls.Add(chkMinToTray);
            Controls.Add(chkRunScheduler);
            Controls.Add(lblLog);
            Controls.Add(lstLog);
            Controls.Add(grpDays);
            Controls.Add(lblTime);
            Controls.Add(lblArgs);
            Controls.Add(lblExe);
            Controls.Add(lblName);
            Controls.Add(btnSave);
            Controls.Add(btnDelete);
            Controls.Add(btnAddUpdate);
            Controls.Add(btnNew);
            Controls.Add(chkEnabled);
            Controls.Add(dtpTime);
            Controls.Add(txtArgs);
            Controls.Add(btnBrowse);
            Controls.Add(txtExe);
            Controls.Add(txtName);
            Controls.Add(dgvSchedules);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "自動啟動排程工具";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSchedules).EndInit();
            grpDays.ResumeLayout(false);
            grpDays.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
