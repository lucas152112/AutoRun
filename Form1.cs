using System.ComponentModel;
using Microsoft.Win32;

namespace AutoRun
{
    public partial class Form1 : Form
    {
        private readonly BindingList<ScheduleItem> _items = new();
        private readonly BindingSource _binding = new();
        private readonly SchedulerService _scheduler = new();
        private ScheduleItem? _selected;
        private NotifyIcon? _trayIcon;
        private ContextMenuStrip? _trayMenu;
        private AppSettings _settings = new();
        private ToolStripMenuItem? _menuToggleScheduler;

        public Form1()
        {
            InitializeComponent();
            
            dgvSchedules.AutoGenerateColumns = false;
            dgvSchedules.Columns.Clear();
            dgvSchedules.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "啟用",
                DataPropertyName = nameof(ScheduleItem.Enabled),
                Width = 50
            });
            dgvSchedules.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "名稱",
                DataPropertyName = nameof(ScheduleItem.Name),
                Width = 150  // 縮短名稱欄位長度
            });
            dgvSchedules.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "程式",
                DataPropertyName = nameof(ScheduleItem.ExePath),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill  // 程式路徑使用剩餘空間
            });
            dgvSchedules.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "時間",
                DataPropertyName = nameof(ScheduleItem.TimeDisplay),
                Width = 60
            });
            dgvSchedules.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "週期",  // 改為週期
                DataPropertyName = nameof(ScheduleItem.DaysDisplay),
                Width = 120  // 拉長週期欄位
            });

            _binding.DataSource = _items;
            dgvSchedules.DataSource = _binding;

            _scheduler.OnRun += (s, msg) => BeginInvoke(new Action(() =>
            {
                lstLog.Items.Insert(0, msg);
                UpdateTrayTextAndIcon();
                _trayIcon?.ShowBalloonTip(2000, "排程執行", msg, ToolTipIcon.Info);
            }));

            InitializeTray();
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            // 暫時移除 SelectionChanged 事件處理，避免載入時觸發
            dgvSchedules.SelectionChanged -= dgvSchedules_SelectionChanged;
            
            var loaded = await Storage.LoadAsync();
            _items.Clear();
            foreach (var i in loaded)
            {
                _items.Add(i);
            }
            
            _binding.ResetBindings(false);

            _settings = await Storage.LoadSettingsAsync();
            ApplySettingsToUI();
            ApplyToScheduler();

            // 清除 Grid 的預設選擇，不顯示第一筆資料
            dgvSchedules.ClearSelection();
            _selected = null;
            
            // 清空輸入欄位內容
            ClearInputs();
            
            // 禁用所有輸入欄位，等待使用者按下「新增」或選擇項目
            DisableInputs();
            
            // 重新加入 SelectionChanged 事件處理
            dgvSchedules.SelectionChanged += dgvSchedules_SelectionChanged;
        }

        private async void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            await Storage.SaveAsync(_items);
            await Storage.SaveSettingsAsync(ReadSettingsFromUI());
            _scheduler.Stop();
            _scheduler.Dispose();
            _trayIcon?.Dispose();
        }

        private void InitializeTray()
        {
            _trayMenu = new ContextMenuStrip();
            _menuToggleScheduler = new ToolStripMenuItem("啟動排程器", null, (s, e) => ToggleScheduler());
            _trayMenu.Items.Add("開啟視窗", null, (s, e) => RestoreFromTray());
            _trayMenu.Items.Add(_menuToggleScheduler);
            _trayMenu.Items.Add(new ToolStripSeparator());
            _trayMenu.Items.Add("退出", null, (s, e) => Close());

            _trayIcon = new NotifyIcon
            {
                Text = "自動啟動排程工具",
                Visible = true,
                Icon = SystemIcons.Application,
                ContextMenuStrip = _trayMenu
            };
            _trayIcon.DoubleClick += (s, e) => RestoreFromTray();
            UpdateTrayTextAndIcon();
        }

        private void UpdateTrayTextAndIcon()
        {
            if (_trayIcon == null) return;
            var running = _scheduler.IsRunning ? "執行中" : "已停止";
            _trayIcon.Text = $"排程器: {running}";
            _menuToggleScheduler!.Text = _scheduler.IsRunning ? "停止排程器" : "啟動排程器";
        }

        private void ToggleScheduler()
        {
            if (_scheduler.IsRunning) _scheduler.Stop();
            else _scheduler.Start();
            chkRunScheduler.Checked = _scheduler.IsRunning;
            UpdateTrayTextAndIcon();
        }

        private void RestoreFromTray()
        {
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (WindowState == FormWindowState.Minimized && chkMinToTray.Checked)
            {
                if (chkAutoStartOnMin.Checked && !_scheduler.IsRunning)
                {
                    _scheduler.Start();
                    chkRunScheduler.Checked = true;
                }
                Hide();
                UpdateTrayTextAndIcon();
                _trayIcon?.ShowBalloonTip(1500, "已最小化", _scheduler.IsRunning ? "排程器執行中" : "排程器已停止", ToolTipIcon.Info);
            }
        }

        private void ApplySettingsToUI()
        {
            chkMinToTray.Checked = _settings.MinimizeToTray;
            chkRunAtStartup.Checked = _settings.RunOnStartup;
            chkAutoStartOnMin.Checked = _settings.MinimizeAutoStartScheduler;
            ApplyRunAtStartup(_settings.RunOnStartup);
            UpdateTrayTextAndIcon();
        }

        private AppSettings ReadSettingsFromUI() => new AppSettings
        {
            MinimizeToTray = chkMinToTray.Checked,
            RunOnStartup = chkRunAtStartup.Checked,
            MinimizeAutoStartScheduler = chkAutoStartOnMin.Checked
        };

        private void ApplyRunAtStartup(bool enabled)
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", writable: true);
                if (key == null) return;
                var appName = "AutoRunScheduler";
                if (enabled)
                {
                    var exe = Application.ExecutablePath;
                    key.SetValue(appName, '"' + exe + '"');
                }
                else
                {
                    if (key.GetValue(appName) != null)
                        key.DeleteValue(appName, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "設定開機啟動失敗: " + ex.Message);
            }
        }

        private void chkRunAtStartup_CheckedChanged(object? sender, EventArgs e)
        {
            ApplyRunAtStartup(chkRunAtStartup.Checked);
        }

        private void btnBrowse_Click(object? sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Filter = "Executables|*.exe;*.bat;*.cmd;*.ps1|All Files|*.*"
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                txtExe.Text = dlg.FileName;
            }
        }

        private void btnNew_Click(object? sender, EventArgs e)
        {
            _selected = null;
            dgvSchedules.ClearSelection();
            EnableInputs();  // 啟用輸入欄位
            ClearInputs();
        }

        private async void btnAddUpdate_Click(object? sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var exe = txtExe.Text.Trim();
            var args = txtArgs.Text.Trim();
            var tod = dtpTime.Value.TimeOfDay;
            var enabled = chkEnabled.Checked;
            var days = GetSelectedDays();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(this, "請輸入名稱", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(exe))
            {
                MessageBox.Show(this, "請選擇程式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (days.Count == 0)
            {
                MessageBox.Show(this, "請選擇至少一天", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_selected != null)
            {
                // 更新現有項目
                _selected.Name = name;
                _selected.ExePath = exe;
                _selected.Arguments = args;
                _selected.TimeOfDay = tod;
                _selected.Enabled = enabled;
                _selected.Days = days;
            }
            else
            {
                // 新增項目
                var newItem = new ScheduleItem
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    ExePath = exe,
                    Arguments = args,
                    TimeOfDay = tod,
                    Enabled = enabled,
                    Days = days
                };
                
                _items.Add(newItem);
                _selected = newItem;
            }

            // 重新整理綁定
            _binding.ResetBindings(false);
            dgvSchedules.Refresh();

            // 選取剛才新增或更新的項目
            if (_selected != null)
            {
                var idx = _items.ToList().FindIndex(x => x.Id == _selected.Id);
                
                if (idx >= 0 && idx < dgvSchedules.Rows.Count)
                {
                    dgvSchedules.ClearSelection();
                    dgvSchedules.Rows[idx].Selected = true;
                    dgvSchedules.CurrentCell = dgvSchedules.Rows[idx].Cells[0];
                }
            }

            ApplyToScheduler();
            
            // 儲存到檔案
            try
            {
                await Storage.SaveAsync(_items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"儲存失敗: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (_selected == null) return;
            var confirm = MessageBox.Show(this, $"確定要刪除 '{_selected.Name}'?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            _items.Remove(_selected);
            _binding.ResetBindings(false);

            _selected = null;
            DisableInputs();  // 刪除後禁用輸入欄位
            ClearInputs();
            ApplyToScheduler();
        }

        private async void btnSave_Click(object? sender, EventArgs e)
        {
            await Storage.SaveAsync(_items);
            await Storage.SaveSettingsAsync(ReadSettingsFromUI());
            MessageBox.Show(this, "已存檔", "訊息");
        }

        private void dgvSchedules_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvSchedules.CurrentRow?.DataBoundItem is ScheduleItem item)
            {
                _selected = item;
                EnableInputs();  // 啟用輸入欄位
                LoadToInputs(item);
            }
            else
            {
                // 當沒有選擇時，清除 _selected
                if (dgvSchedules.Rows.Count == 0 || dgvSchedules.SelectedRows.Count == 0)
                {
                    _selected = null;
                    DisableInputs();  // 禁用輸入欄位
                }
            }
        }

        private void chkRunScheduler_CheckedChanged(object? sender, EventArgs e)
        {
            if (chkRunScheduler.Checked) _scheduler.Start();
            else _scheduler.Stop();
            UpdateTrayTextAndIcon();
        }

        private void chkEveryDay_CheckedChanged(object? sender, EventArgs e)
        {
            var disable = chkEveryDay.Checked;
            chkMon.Enabled = chkTue.Enabled = chkWed.Enabled = chkThu.Enabled = chkFri.Enabled = chkSat.Enabled = chkSun.Enabled = !disable;
        }

        private void LoadToInputs(ScheduleItem item)
        {
            txtName.Text = item.Name;
            txtExe.Text = item.ExePath;
            txtArgs.Text = item.Arguments;
            dtpTime.Value = DateTime.Today.Add(item.TimeOfDay);
            chkEnabled.Checked = item.Enabled;

            var allDays = new[]
            {
                DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday
            };
            var isEvery = item.Days.Count == 7 && allDays.All(d => item.Days.Contains(d));
            chkEveryDay.Checked = isEvery;
            chkMon.Checked = item.Days.Contains(DayOfWeek.Monday);
            chkTue.Checked = item.Days.Contains(DayOfWeek.Tuesday);
            chkWed.Checked = item.Days.Contains(DayOfWeek.Wednesday);
            chkThu.Checked = item.Days.Contains(DayOfWeek.Thursday);
            chkFri.Checked = item.Days.Contains(DayOfWeek.Friday);
            chkSat.Checked = item.Days.Contains(DayOfWeek.Saturday);
            chkSun.Checked = item.Days.Contains(DayOfWeek.Sunday);

            chkMon.Enabled = chkTue.Enabled = chkWed.Enabled = chkThu.Enabled = chkFri.Enabled = chkSat.Enabled = chkSun.Enabled = !chkEveryDay.Checked;
        }

        private void ClearInputs()
        {
            txtName.Clear();
            txtExe.Clear();
            txtArgs.Clear();
            dtpTime.Value = DateTime.Today.AddHours(9);
            chkEnabled.Checked = true;
            chkEveryDay.Checked = true; // 預設勾選每天
            chkMon.Checked = chkTue.Checked = chkWed.Checked = chkThu.Checked = chkFri.Checked = chkSat.Checked = chkSun.Checked = false;
            chkMon.Enabled = chkTue.Enabled = chkWed.Enabled = chkThu.Enabled = chkFri.Enabled = chkSat.Enabled = chkSun.Enabled = false; // 因為每天已勾選，所以禁用個別選項
        }

        private List<DayOfWeek> GetSelectedDays()
        {
            if (chkEveryDay.Checked)
            {
                return new List<DayOfWeek>
                {
                    DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                    DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday
                };
            }
            var days = new List<DayOfWeek>();
            if (chkMon.Checked) days.Add(DayOfWeek.Monday);
            if (chkTue.Checked) days.Add(DayOfWeek.Tuesday);
            if (chkWed.Checked) days.Add(DayOfWeek.Wednesday);
            if (chkThu.Checked) days.Add(DayOfWeek.Thursday);
            if (chkFri.Checked) days.Add(DayOfWeek.Friday);
            if (chkSat.Checked) days.Add(DayOfWeek.Saturday);
            if (chkSun.Checked) days.Add(DayOfWeek.Sunday);
            return days;
        }

        private void ApplyToScheduler()
        {
            _scheduler.SetItems(_items);
            UpdateTrayTextAndIcon();
        }

        private void EnableInputs()
        {
            txtName.Enabled = true;
            txtExe.Enabled = true;
            txtArgs.Enabled = true;
            btnBrowse.Enabled = true;
            dtpTime.Enabled = true;
            chkEnabled.Enabled = true;
            chkEveryDay.Enabled = true;
            
            // 星期選項根據「每天」的狀態決定
            var enableDays = !chkEveryDay.Checked;
            chkMon.Enabled = enableDays;
            chkTue.Enabled = enableDays;
            chkWed.Enabled = enableDays;
            chkThu.Enabled = enableDays;
            chkFri.Enabled = enableDays;
            chkSat.Enabled = enableDays;
            chkSun.Enabled = enableDays;
            
            btnAddUpdate.Enabled = true;
        }

        private void DisableInputs()
        {
            txtName.Enabled = false;
            txtExe.Enabled = false;
            txtArgs.Enabled = false;
            btnBrowse.Enabled = false;
            dtpTime.Enabled = false;
            chkEnabled.Enabled = false;
            chkEveryDay.Enabled = false;
            chkMon.Enabled = false;
            chkTue.Enabled = false;
            chkWed.Enabled = false;
            chkThu.Enabled = false;
            chkFri.Enabled = false;
            chkSat.Enabled = false;
            chkSun.Enabled = false;
            btnAddUpdate.Enabled = false;
        }
    }
}
