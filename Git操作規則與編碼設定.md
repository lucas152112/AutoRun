# PowerShell Profile 自動設定 UTF-8 編碼

## 重要提醒

**從現在開始，所有 Git 指令都應該使用批次檔執行，以確保中文正確顯示。**

## 設定方式

### 方法 1: 永久設定 PowerShell Profile（推薦）

#### 步驟 1: 開啟 PowerShell Profile
```powershell
notepad $PROFILE
```

如果檔案不存在，選擇「是」建立新檔案。

#### 步驟 2: 加入以下內容
```powershell
# ===========================================
# AutoRun 專案 - UTF-8 編碼自動設定
# ===========================================

# 設定主控台輸出編碼為 UTF-8
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

# 設定 PowerShell 輸出編碼為 UTF-8
$OutputEncoding = [System.Text.Encoding]::UTF8

# 設定環境變數
$env:LANG = 'zh_TW.UTF-8'
$env:LC_ALL = 'zh_TW.UTF-8'

# 顯示提示訊息
Write-Host "? UTF-8 編碼已自動設定" -ForegroundColor Green

# Git 別名 - 自動使用正確編碼
function Git-Status { git status }
function Git-Log { git log --oneline -10 --graph --decorate }
function Git-Commit { 
    param([string]$message)
    git add .
    git commit -m $message
}
function Git-Push { git push origin main }
function Git-Pull { git pull origin main }

# 設定別名
Set-Alias gs Git-Status
Set-Alias gl Git-Log
Set-Alias gc Git-Commit
Set-Alias gp Git-Push
Set-Alias gpu Git-Pull

Write-Host "? Git 別名已設定 (gs, gl, gc, gp, gpu)" -ForegroundColor Green
```

#### 步驟 3: 儲存並關閉

#### 步驟 4: 重新載入 Profile
```powershell
. $PROFILE
```

或重新啟動 PowerShell。

### 方法 2: 使用批次檔（目前方式）

**這是目前推薦的方式，因為最穩定可靠。**

#### 可用的批次檔

| 批次檔 | 用途 | 使用方式 |
|--------|------|----------|
| `git-menu.bat` | 互動式選單 | 雙擊執行或 `git-menu.bat` |
| `git-status.bat` | 查看狀態 | 雙擊執行或 `git-status.bat` |
| `git-log.bat` | 查看日誌 | 雙擊執行或 `git-log.bat` |
| `git-commit.bat` | 提交變更 | 雙擊執行或 `git-commit.bat` |
| `git-push.bat` | 推送到遠端 | 雙擊執行或 `git-push.bat` |
| `run-utf8.bat` | 執行任何指令 | `run-utf8.bat <指令>` |

## 操作規則

### ? 應該使用的方式

#### 1. 使用互動式選單（最推薦）
```cmd
git-menu.bat
```

#### 2. 使用專用批次檔
```cmd
git-status.bat
git-log.bat
git-commit.bat
git-push.bat
```

#### 3. 使用通用執行器
```cmd
run-utf8.bat git status
run-utf8.bat git log --oneline -5
run-utf8.bat git diff
```

#### 4. 如果已設定 PowerShell Profile
```powershell
# 使用別名
gs          # git status
gl          # git log
gc "訊息"   # git commit
gp          # git push
gpu         # git pull

# 或直接執行（Profile 已設定編碼）
git status
git log
```

### ? 不應該使用的方式

#### 1. 直接在 PowerShell 執行（沒有編碼設定）
```powershell
# ? 錯誤 - 會有亂碼
git status
git log
```

#### 2. 直接在 CMD 執行（沒有設定 UTF-8）
```cmd
REM ? 錯誤 - 會有亂碼
git status
git log
```

## 日常工作流程

### 完整的提交流程

#### 使用批次檔（推薦）
```cmd
1. git-menu.bat
   選擇 1 -> 查看狀態
   選擇 3 -> 提交變更（輸入訊息）
   選擇 4 -> 推送
   選擇 2 -> 確認結果
   選擇 0 -> 離開
```

#### 使用個別批次檔
```cmd
git-status.bat
git-commit.bat
git-push.bat
git-log.bat
```

#### 使用 PowerShell（需先設定 Profile）
```powershell
gs              # 查看狀態
gc "更新文件"   # 提交變更
gp              # 推送
gl              # 查看日誌
```

## 快速參考

### Git 操作對照表

| 操作 | 批次檔 | PowerShell (Profile) | 原始指令 |
|------|--------|---------------------|----------|
| 查看狀態 | `git-status.bat` | `gs` | `git status` |
| 查看日誌 | `git-log.bat` | `gl` | `git log --oneline -10` |
| 提交變更 | `git-commit.bat` | `gc "訊息"` | `git add . && git commit -m "訊息"` |
| 推送 | `git-push.bat` | `gp` | `git push origin main` |
| 拉取 | - | `gpu` | `git pull origin main` |
| 自訂指令 | `run-utf8.bat <指令>` | 直接執行 | - |

### 常用操作範例

#### 查看變更
```cmd
# 方法 1: 批次檔
git-status.bat

# 方法 2: 通用執行器
run-utf8.bat git status

# 方法 3: PowerShell (已設定 Profile)
gs
```

#### 提交並推送
```cmd
# 方法 1: 使用選單
git-menu.bat
# -> 3 (提交)
# -> 4 (推送)

# 方法 2: 使用批次檔
git-commit.bat
git-push.bat

# 方法 3: PowerShell (已設定 Profile)
gc "feat: 新增功能"
gp
```

#### 查看歷史
```cmd
# 方法 1: 批次檔
git-log.bat

# 方法 2: 通用執行器
run-utf8.bat git log --oneline -5

# 方法 3: PowerShell (已設定 Profile)
gl
```

## 檢查編碼設定

### 驗證 PowerShell Profile 是否生效

```powershell
# 查看當前編碼
[Console]::OutputEncoding

# 應該顯示:
# BodyName        : utf-8
# HeaderName      : utf-8
```

### 驗證 Git 設定

```powershell
# 查看 Git 配置
git config --get core.quotepath

# 應該顯示: false
```

### 測試中文顯示

```cmd
# 使用批次檔測試
git-log.bat

# 或使用 PowerShell (已設定 Profile)
gl

# 應該看到中文正常顯示，沒有亂碼
```

## 疑難排解

### 問題 1: PowerShell Profile 無法執行

**錯誤訊息**: 因為這個系統上已停用指令碼執行...

**解決方式**:
```powershell
# 以管理員身分執行 PowerShell
Set-ExecutionPolicy RemoteSigned -Scope CurrentUser
```

### 問題 2: 批次檔執行後仍有亂碼

**檢查事項**:
1. 確認檔案編碼是 UTF-8
2. 確認使用的是正確的批次檔
3. 嘗試使用 `git-menu.bat` 互動式選單

**解決方式**:
```cmd
# 重新下載或重新創建批次檔
# 確保檔案內容正確
```

### 問題 3: PowerShell 別名無法使用

**原因**: Profile 未載入或有錯誤

**解決方式**:
```powershell
# 重新載入 Profile
. $PROFILE

# 如果有錯誤，檢查 Profile 內容
notepad $PROFILE
```

## 建議的工作習慣

### 優先順序

1. **?? git-menu.bat** - 日常開發的最佳選擇
   - 提供完整的互動式介面
   - 所有功能一應俱全
   - 最簡單易用

2. **?? 專用批次檔** - 快速單一操作
   - 雙擊即可執行
   - 適合快速查看

3. **?? PowerShell Profile** - 指令行愛好者
   - 需要先設定 Profile
   - 適合習慣使用指令行的使用者

### 團隊協作建議

如果團隊成員遇到中文編碼問題：

1. 將批次檔複製給他們
2. 或請他們設定 PowerShell Profile
3. 或提供 `Git批次檔工具使用說明.md` 文件

## 更新紀錄

- **2025-01-20**: 初始版本
  - 提供批次檔解決方案
  - 提供 PowerShell Profile 設定方式
  - 建立完整的操作規則

## 相關文件

- `Git批次檔工具使用說明.md` - 批次檔詳細說明
- `PowerShell中文編碼解決方案.md` - 編碼問題完整說明
- `Git批次檔工具創建總結.md` - 工具創建總結

---

**記住**: 從現在開始，所有 Git 操作都應該使用批次檔或設定好的 PowerShell Profile，以確保中文正確顯示！
