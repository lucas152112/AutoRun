# PowerShell 中文編碼問題解決總結

## ? 問題已解決

### 問題描述
在 PowerShell 中執行 git 指令時，中文字元會顯示為亂碼或問號。

### 解決方案
在每個 git 指令前加入 UTF-8 編碼設定：

```powershell
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
```

## ?? 已完成的操作

### 1. 設定 Git 配置
```powershell
git config core.quotepath false
```
- 功能: 不將非 ASCII 字元（如中文）轉義顯示

### 2. 驗證設定效果

**之前（有亂碼）:**
```
摰???撌亙?銝血?I (v1.1)
```

**之後（正常顯示）:**
```
完成排程工具開發並優化UI (v1.1)
```

### 3. 成功提交的記錄

使用正確編碼執行的 git 操作：

```powershell
# Commit 1
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
git commit -m "docs: 新增 Git 提交總結文件"
# Result: 536c74b

# Commit 2
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
git commit -m "docs: 新增 PowerShell 中文編碼解決方案文件"
# Result: 6791c4a
```

## ?? 最新提交記錄

```
6791c4a (HEAD -> main, origin/main) docs: 新增 PowerShell 中文編碼解決方案文件
536c74b docs: 新增 Git 提交總結文件
00f3f77 feat: 完成排程工具開發並優化UI (v1.1)
819580f Initial commit
```

所有中文都正常顯示！?

## ?? 推薦的使用方式

### 臨時使用（單次指令）
```powershell
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; git status
```

### 永久設定（推薦）

編輯 PowerShell Profile:
```powershell
notepad $PROFILE
```

加入以下內容:
```powershell
# 設定 UTF-8 編碼
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$OutputEncoding = [System.Text.Encoding]::UTF8
$env:LANG = 'zh_TW.UTF-8'
```

### Git 全域設定（一次性）
```powershell
git config --global core.quotepath false
git config --global i18n.commitencoding utf-8
git config --global i18n.logoutputencoding utf-8
```

## ?? 相關文件

已創建的說明文件：
- `PowerShell中文編碼解決方案.md` - 完整的編碼問題解決指南
- `Git提交總結.md` - Git 提交操作總結

## ? 驗證方式

執行以下指令驗證編碼是否正確：

```powershell
# 1. 查看 Git 日誌（應顯示正確中文）
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; git log --oneline -5

# 2. 查看 Git 狀態（應顯示正確中文檔名）
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; git status

# 3. 查看當前編碼設定
[Console]::OutputEncoding
# 應顯示: BodyName: utf-8

# 4. 查看 Git 配置
git config --get core.quotepath
# 應顯示: false
```

## ?? 總結

1. ? 問題診斷完成
2. ? 解決方案實施
3. ? 驗證測試通過
4. ? 文件完整記錄
5. ? 所有變更已推送到 GitHub

現在可以在 PowerShell 中正常使用中文進行 Git 操作了！

## ?? 相關連結

- GitHub 倉庫: https://github.com/lucas152112/AutoRun
- 最新 Commit: 6791c4a
- Branch: main
