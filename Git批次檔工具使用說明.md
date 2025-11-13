# Git 批次檔工具使用說明

## 目的

解決在 Windows CMD/PowerShell 中執行 Git 指令時的中文亂碼問題，提供簡單易用的批次檔工具。

## 批次檔列表

### 1. `run-utf8.bat` - 通用 UTF-8 執行器

**用途**: 執行任何 PowerShell 指令並確保 UTF-8 編碼

**使用方式**:
```cmd
run-utf8.bat <PowerShell 指令>
```

**範例**:
```cmd
run-utf8.bat git status
run-utf8.bat git log --oneline -5
run-utf8.bat git commit -m "更新文件"
```

### 2. `git-status.bat` - 查看 Git 狀態

**用途**: 查看當前 Git 倉庫狀態

**使用方式**:
```cmd
git-status.bat
```

**顯示內容**:
- 修改的檔案
- 未追蹤的檔案
- 暫存的變更
- 分支資訊

### 3. `git-log.bat` - 查看提交記錄

**用途**: 查看最近 10 筆提交記錄

**使用方式**:
```cmd
git-log.bat
```

**顯示內容**:
- Commit hash
- Commit 訊息
- 作者和日期

### 4. `git-commit.bat` - 提交變更

**用途**: 執行 `git add .` 和 `git commit`

**使用方式**:
```cmd
git-commit.bat
```

**操作流程**:
1. 執行批次檔
2. 輸入 commit 訊息
3. 自動執行 `git add .` 和 `git commit`

**注意事項**:
- 會自動 add 所有變更的檔案
- 支援中文 commit 訊息

### 5. `git-push.bat` - 推送到遠端

**用途**: 推送到 origin/main

**使用方式**:
```cmd
git-push.bat
```

**操作流程**:
1. 執行批次檔
2. 自動推送到 origin/main
3. 顯示推送結果

### 6. `git-menu.bat` - Git 操作選單（推薦）

**用途**: 互動式 Git 操作選單，整合所有常用功能

**使用方式**:
```cmd
git-menu.bat
```

**功能選單**:
```
================================================
      Git 操作工具 (UTF-8 編碼)
================================================

1. 查看狀態 (git status)
2. 查看日誌 (git log)
3. 提交變更 (git commit)
4. 推送到遠端 (git push)
5. 拉取最新 (git pull)
6. 查看差異 (git diff)
7. 自訂指令
0. 離開
```

**特色**:
- ? 清晰的選單介面
- ? 支援所有常用 Git 操作
- ? 自動處理 UTF-8 編碼
- ? 中文顯示正常
- ? 可以輸入自訂 Git 指令
- ? 操作後返回選單，無需重複執行

## 技術原理

### 編碼處理

每個批次檔都包含以下關鍵設定：

```batch
@echo off
chcp 65001 >nul
```

- `chcp 65001`: 設定 CMD 為 UTF-8 編碼（代碼頁 65001）
- `>nul`: 隱藏 chcp 指令的輸出訊息

### PowerShell 編碼設定

```batch
powershell -NoProfile -ExecutionPolicy Bypass -Command "[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; $OutputEncoding = [System.Text.Encoding]::UTF8; <git 指令>"
```

參數說明：
- `-NoProfile`: 不載入 PowerShell Profile，加快啟動速度
- `-ExecutionPolicy Bypass`: 繞過執行政策限制
- `[Console]::OutputEncoding`: 設定主控台輸出編碼為 UTF-8
- `$OutputEncoding`: 設定 PowerShell 輸出編碼為 UTF-8

## 使用場景

### 場景 1: 日常開發流程

```cmd
# 1. 查看狀態
git-status.bat

# 2. 提交變更
git-commit.bat
# 輸入: feat: 新增功能

# 3. 推送到遠端
git-push.bat

# 4. 查看日誌確認
git-log.bat
```

### 場景 2: 使用選單工具（推薦）

```cmd
# 執行選單
git-menu.bat

# 然後按照提示操作：
# 1 -> 查看狀態
# 3 -> 提交變更（輸入訊息）
# 4 -> 推送
# 2 -> 查看日誌
# 0 -> 離開
```

### 場景 3: 快速查看狀態

```cmd
# 雙擊 git-status.bat
# 或在 CMD 中執行
git-status.bat
```

### 場景 4: 執行自訂 Git 指令

```cmd
run-utf8.bat git branch -a
run-utf8.bat git remote -v
run-utf8.bat git diff HEAD~1
```

## 常見問題

### Q1: 為什麼需要這些批次檔？

**A**: Windows 的 CMD 和 PowerShell 預設編碼不是 UTF-8，導致 Git 輸出的中文會顯示為亂碼。這些批次檔自動處理編碼問題。

### Q2: 可以直接在 PowerShell 中執行嗎？

**A**: 可以，但每次都要先設定編碼：
```powershell
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
git status
```
使用批次檔可以自動化這個過程。

### Q3: 批次檔會修改我的 Git 設定嗎？

**A**: 不會。批次檔只是在執行時設定編碼，不會修改任何 Git 全域或本地設定。

### Q4: 為什麼選單操作後要按任意鍵？

**A**: 這樣可以讓您查看執行結果，確認操作是否成功，然後再返回選單。

### Q5: 可以用在其他 Git 倉庫嗎？

**A**: 可以！將這些批次檔複製到任何 Git 倉庫的根目錄即可使用。

### Q6: 支援 Git Bash 嗎？

**A**: 不需要。Git Bash 本身就支援 UTF-8，不會有中文亂碼問題。這些批次檔主要是給使用 Windows CMD 的使用者。

## 進階使用

### 自訂批次檔

您可以根據自己的需求創建自訂批次檔。基本模板：

```batch
@echo off
chcp 65001 >nul
echo 執行自訂操作...
powershell -NoProfile -ExecutionPolicy Bypass -Command "[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; $OutputEncoding = [System.Text.Encoding]::UTF8; <您的指令>"
pause
```

### 範例：建立分支並推送

```batch
@echo off
chcp 65001 >nul
set /p branch=請輸入新分支名稱: 
powershell -NoProfile -ExecutionPolicy Bypass -Command "[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; $OutputEncoding = [System.Text.Encoding]::UTF8; git checkout -b %branch%; git push -u origin %branch%"
pause
```

## 建議工作流程

### 日常開發

1. **使用 `git-menu.bat` 作為主要工具**
   - 提供友善的選單介面
   - 整合所有常用功能
   - 操作後自動返回選單

2. **快速查看使用單一批次檔**
   - 雙擊 `git-status.bat` 快速查看狀態
   - 雙擊 `git-log.bat` 快速查看歷史

3. **自訂操作使用 `run-utf8.bat`**
   - 執行任何自訂的 Git 指令
   - 靈活方便

### 提交變更的完整流程

```cmd
# 方法 1: 使用選單（推薦）
git-menu.bat
# 選擇 1 -> 查看狀態
# 選擇 3 -> 提交變更
# 選擇 4 -> 推送

# 方法 2: 使用個別批次檔
git-status.bat      # 查看狀態
git-commit.bat      # 提交變更
git-push.bat        # 推送
git-log.bat         # 確認結果
```

## 檔案清單

```
AutoRun/
├── run-utf8.bat           # 通用 UTF-8 執行器
├── git-status.bat         # 查看狀態
├── git-log.bat            # 查看日誌
├── git-commit.bat         # 提交變更
├── git-push.bat           # 推送到遠端
└── git-menu.bat           # 互動式選單（推薦使用）
```

## 更新紀錄

- **2025-01-20**: 初始版本
  - 創建基本批次檔
  - 實作 UTF-8 編碼處理
  - 新增互動式選單工具

## 授權

這些批次檔是為 AutoRun 專案開發的工具，可以自由複製和修改。

## 相關文件

- `PowerShell中文編碼解決方案.md` - PowerShell 編碼問題的詳細說明
- `PowerShell編碼問題解決總結.md` - 問題解決總結

## 技術支援

如果遇到問題，可以：
1. 檢查是否有管理員權限
2. 確認 PowerShell 執行政策
3. 檢查 Git 是否正確安裝
4. 查看相關文件獲取更多資訊
