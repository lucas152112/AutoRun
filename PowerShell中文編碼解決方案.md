# PowerShell 中文編碼解決方案

## 問題說明

在 PowerShell 中執行 git 或其他指令時，中文字元可能會顯示為亂碼或問號，這是因為 PowerShell 的預設編碼設定與 UTF-8 不一致。

## 解決方法

### 方法 1: 每次執行前設定（臨時）

在執行 git 指令前，先執行以下指令設定編碼：

```powershell
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$env:LANG='zh_TW.UTF-8'
```

然後再執行你的 git 指令：

```powershell
git status
git log --oneline
git commit -m "你的中文訊息"
```

### 方法 2: 單行執行（推薦用於自動化）

將編碼設定與指令合併成一行：

```powershell
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; git status
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; git log --oneline -5
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; git commit -m "docs: 更新說明文件"
```

### 方法 3: 永久設定 PowerShell Profile

1. 開啟 PowerShell Profile 檔案：

```powershell
notepad $PROFILE
```

如果檔案不存在，會詢問是否建立，選擇「是」。

2. 在檔案中加入以下內容：

```powershell
# 設定輸出編碼為 UTF-8
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$OutputEncoding = [System.Text.Encoding]::UTF8

# 設定語言環境
$env:LANG = 'zh_TW.UTF-8'

# Git 相關設定
$env:LC_ALL = 'zh_TW.UTF-8'
```

3. 儲存並關閉檔案

4. 重新啟動 PowerShell 或執行：

```powershell
. $PROFILE
```

### 方法 4: 設定 Git 全域配置

執行以下指令設定 Git 的編碼：

```powershell
# 不要將非 ASCII 字元顯示為八進位表示
git config --global core.quotepath false

# 設定提交訊息編碼
git config --global i18n.commitencoding utf-8

# 設定日誌輸出編碼
git config --global i18n.logoutputencoding utf-8
```

## 實際範例

### 範例 1: 查看狀態

```powershell
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; cd D:\work\csharp\AutoRun; git status
```

### 範例 2: 提交變更

```powershell
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; cd D:\work\csharp\AutoRun; git add .; git commit -m "feat: 新增排程功能"
```

### 範例 3: 查看日誌

```powershell
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; cd D:\work\csharp\AutoRun; git log --oneline -5
```

### 範例 4: 推送到遠端

```powershell
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; cd D:\work\csharp\AutoRun; git push origin main
```

## 已應用的設定

在本專案中，已經執行了以下設定：

```powershell
# 設定 Git 不將中文轉義
git config core.quotepath false

# 所有 git 指令都使用 UTF-8 編碼前綴
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; git [command]
```

## 驗證編碼設定

執行以下指令驗證編碼是否正確：

```powershell
# 查看當前編碼
[Console]::OutputEncoding

# 應該顯示: BodyName        : utf-8

# 查看 Git 設定
git config --get core.quotepath

# 應該顯示: false
```

## 常見問題

### Q1: 為什麼每次都要設定？
A: 因為 PowerShell 的編碼設定是會話級別的，關閉後就會重置。使用 Profile 可以自動載入。

### Q2: VSCode 終端機也需要設定嗎？
A: 是的，VSCode 的整合終端機也是 PowerShell，需要相同的設定。

### Q3: 設定後還是有亂碼怎麼辦？
A: 
1. 確認檔案本身是 UTF-8 編碼儲存的
2. 檢查終端機字型是否支援中文
3. 重啟 PowerShell 或 VSCode

## 建議的工作流程

1. 設定永久 Profile（方法 3）
2. 設定 Git 全域配置（方法 4）
3. 重啟 PowerShell
4. 驗證設定是否生效

這樣以後就不需要每次都手動設定編碼了。

## 參考資料

- [PowerShell 文件 - about_Character_Encoding](https://docs.microsoft.com/powershell/module/microsoft.powershell.core/about/about_character_encoding)
- [Git 文件 - core.quotepath](https://git-scm.com/docs/git-config#Documentation/git-config.txt-corequotePath)
