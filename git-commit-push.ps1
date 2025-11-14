# Git Commit 並 Push 到遠端
# 使用方式: .\git-commit-push.ps1 "commit 訊息"

param(
    [Parameter(Mandatory=$false)]
    [string]$Message = "Update: 功能更新和問題修正"
)

# 設定 UTF-8 編碼
$OutputEncoding = [System.Text.Encoding]::UTF8
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$PSDefaultParameterValues['*:Encoding'] = 'utf8'

# 設定 Git 使用 UTF-8
git config --global core.quotepath false
git config --global gui.encoding utf-8
git config --global i18n.commit.encoding utf-8
git config --global i18n.logoutputencoding utf-8

Write-Host "=== Git Commit and Push ===" -ForegroundColor Green
Write-Host ""

# 1. 顯示狀態
Write-Host ">>> git status" -ForegroundColor Cyan
cmd /c "chcp 65001 >nul && git status 2>&1"
Write-Host ""

# 2. 新增所有變更
Write-Host ">>> git add ." -ForegroundColor Cyan
cmd /c "chcp 65001 >nul && git add . 2>&1"
Write-Host ""

# 3. Commit
Write-Host ">>> git commit -m `"$Message`"" -ForegroundColor Cyan
cmd /c "chcp 65001 >nul && git commit -m `"$Message`" 2>&1"
Write-Host ""

# 4. Push
Write-Host ">>> git push" -ForegroundColor Cyan
cmd /c "chcp 65001 >nul && git push 2>&1"
Write-Host ""

Write-Host "=== 完成 ===" -ForegroundColor Green
