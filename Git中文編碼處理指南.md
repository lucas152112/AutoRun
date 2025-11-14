# Git 中文編碼處理指南

## 問題說明

在 Windows PowerShell 中執行 git 指令時，經常會遇到中文檔名或訊息顯示為亂碼或轉義字元的問題。

### 常見問題
```
Untracked files:
	Git銝剜?蝺函Ⅳ摰閫?捱?寞?-?蝯蜇蝯?md
	靽格迤隤芣?-靽??撠??賊????md
```

## 解決方案

### 方法 1：使用 git-helper.ps1（推薦）

**一次性設定並執行指令**：

```powershell
# 執行單一 git 指令
.\git-helper.ps1 git status

# 執行 git add
.\git-helper.ps1 git add .

# 執行 git commit
.\git-helper.ps1 git commit -m "更新功能"

# 執行 git push
.\git-helper.ps1 git push
```

**腳本功能**：
- ? 自動設定 UTF-8 編碼
- ? 設定 Git 全域編碼配置
- ? 使用 `cmd /c chcp 65001` 確保正確編碼
- ? 顯示清楚的執行訊息

### 方法 2：使用 git-commit-push.ps1（快捷方式）

**一鍵完成 add、commit、push**：

```powershell
# 使用預設訊息
.\git-commit-push.ps1

# 自訂 commit 訊息
.\git-commit-push.ps1 "feat: 新增最小化到托盤功能"
```

**腳本執行流程**：
1. 顯示 `git status`
2. 執行 `git add .`
3. 執行 `git commit -m "訊息"`
4. 執行 `git push`

### 方法 3：手動設定（一次性）

在 PowerShell 中執行以下指令：

```powershell
# 設定 PowerShell 編碼
$OutputEncoding = [System.Text.Encoding]::UTF8
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

# 設定 Git 全域配置
git config --global core.quotepath false
git config --global gui.encoding utf-8
git config --global i18n.commit.encoding utf-8
git config --global i18n.logoutputencoding utf-8

# 執行 git 指令（使用 cmd 確保編碼）
cmd /c "chcp 65001 >nul && git status"
```

### 方法 4：建立 PowerShell Profile（永久設定）

**步驟 1：檢查是否有 Profile**
```powershell
Test-Path $PROFILE
```

**步驟 2：建立或編輯 Profile**
```powershell
# 如果不存在，建立 Profile
if (!(Test-Path $PROFILE)) {
    New-Item -Path $PROFILE -ItemType File -Force
}

# 編輯 Profile
notepad $PROFILE
```

**步驟 3：加入以下內容**
```powershell
# Git UTF-8 編碼設定
$OutputEncoding = [System.Text.Encoding]::UTF8
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$PSDefaultParameterValues['*:Encoding'] = 'utf8'

# Git 指令別名（使用 UTF-8）
function Git-UTF8 {
    param([Parameter(ValueFromRemainingArguments=$true)]$args)
    $command = "git $args"
    cmd /c "chcp 65001 >nul && $command 2>&1"
}

Set-Alias -Name g -Value Git-UTF8
```

**步驟 4：重新載入 Profile**
```powershell
. $PROFILE
```

**使用方式**：
```powershell
# 使用 g 別名（自動使用 UTF-8）
g status
g add .
g commit -m "更新"
g push
```

## 完整的 Git 操作流程

### 使用 git-commit-push.ps1（最簡單）

```powershell
# 一鍵完成 add、commit、push
.\git-commit-push.ps1 "feat: 完成最小化功能實現"
```

### 手動執行（使用 git-helper.ps1）

```powershell
# 1. 查看狀態
.\git-helper.ps1 git status

# 2. 新增所有變更
.\git-helper.ps1 git add .

# 3. 查看即將 commit 的內容
.\git-helper.ps1 git status

# 4. Commit
.\git-helper.ps1 git commit -m "feat: 完成最小化功能實現

- 實現智能聯動最小化
- 修正設定狀態保持問題
- 新增開發日誌文件"

# 5. Push 到遠端
.\git-helper.ps1 git push

# 6. 查看 log
.\git-helper.ps1 git log --oneline -5
```

## Git Commit 訊息規範

### Conventional Commits 格式

```
<類型>(<範圍>): <簡短描述>

<詳細描述>

<footer>
```

### 常用類型

| 類型 | 說明 | 範例 |
|------|------|------|
| `feat` | 新增功能 | `feat: 新增最小化到托盤功能` |
| `fix` | 修正問題 | `fix: 修正從托盤恢復時選項被重置` |
| `docs` | 文件變更 | `docs: 更新 README 和開發日誌` |
| `style` | 程式碼格式（不影響功能） | `style: 統一程式碼縮排` |
| `refactor` | 重構（不是新功能也不是修正） | `refactor: 簡化最小化邏輯` |
| `perf` | 效能改善 | `perf: 優化排程檢查效率` |
| `test` | 新增或修正測試 | `test: 新增排程器單元測試` |
| `chore` | 建置流程或輔助工具變更 | `chore: 更新 .gitignore` |

### 範例

**簡單訊息**：
```
feat: 新增系統托盤功能
```

**詳細訊息**：
```
feat: 新增系統托盤功能

- 實現托盤圖示和右鍵選單
- 支援雙擊恢復視窗
- 顯示氣泡通知
- 更新托盤提示文字

Closes #123
```

**多個變更**：
```
feat: 完成最小化和托盤功能整合

- feat: 智能聯動最小化機制
- fix: 修正從托盤恢復時選項狀態
- docs: 新增功能實現說明文件
- refactor: 簡化輸入欄位狀態管理
```

## 當前專案的 Commit 範例

```powershell
# 本次更新的 commit 訊息
.\git-commit-push.ps1 "feat: 完成最小化功能和開發文件

新增功能：
- 實現智能聯動最小化機制
- 啟動排程器時根據選項自動最小化
- 勾選選項時根據排程器狀態最小化
- 從托盤恢復時保持選項狀態

文件更新：
- 新增 Develop_Dairy.md 開發日誌
- 更新 README.md 專案說明
- 新增多個功能實現說明文件

問題修正：
- 修正最小化不自動啟動排程器
- 修正啟動排程器不根據選項最小化
- 修正從托盤恢復時選項被重置

技術改進：
- 新增 git-helper.ps1 處理中文編碼
- 新增 git-commit-push.ps1 快捷腳本"
```

## 常見問題

### Q1: 為什麼需要使用 `cmd /c chcp 65001`？

**A**: Windows PowerShell 預設使用 Big5 或其他編碼，而 Git 使用 UTF-8。使用 `chcp 65001` 切換到 UTF-8 代碼頁，確保中文正確顯示。

### Q2: git-helper.ps1 無法執行？

**A**: PowerShell 執行政策可能阻止腳本執行。執行以下指令：

```powershell
# 查看當前政策
Get-ExecutionPolicy

# 設定允許執行腳本（需要管理員權限）
Set-ExecutionPolicy RemoteSigned -Scope CurrentUser
```

### Q3: 還是顯示亂碼？

**A**: 確認以下設定：

1. PowerShell 視窗屬性 → 字型 → 選擇支援 Unicode 的字型（如 Consolas）
2. 重新開啟 PowerShell 視窗
3. 使用 git-helper.ps1 執行指令

### Q4: 如何查看 Git 當前編碼設定？

```powershell
.\git-helper.ps1 git config --global --list | Select-String "encoding|quotepath"
```

## 檔案清單

本專案包含以下輔助腳本：

| 檔案 | 用途 |
|------|------|
| `git-helper.ps1` | 通用 Git 指令執行器（處理編碼） |
| `git-commit-push.ps1` | 快捷 commit 和 push 腳本 |
| `Git中文編碼處理指南.md` | 本文件 |

## 快速參考

```powershell
# === 方法 1：使用 git-commit-push.ps1（最快） ===
.\git-commit-push.ps1 "commit 訊息"

# === 方法 2：使用 git-helper.ps1（逐步） ===
.\git-helper.ps1 git status
.\git-helper.ps1 git add .
.\git-helper.ps1 git commit -m "訊息"
.\git-helper.ps1 git push

# === 方法 3：手動設定 + cmd ===
$OutputEncoding = [System.Text.Encoding]::UTF8
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
cmd /c "chcp 65001 >nul && git status"
```

---

**建議**：將 `git-helper.ps1` 和 `git-commit-push.ps1` 加入專案根目錄，每次執行 Git 操作時使用這些腳本，確保中文正確顯示。
