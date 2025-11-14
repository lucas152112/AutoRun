# Git 操作輔助腳本 - 處理中文編碼問題
# 使用方式: .\git-helper.ps1 <command> [args]

# 設定 UTF-8 編碼
$OutputEncoding = [System.Text.Encoding]::UTF8
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$PSDefaultParameterValues['*:Encoding'] = 'utf8'

# 設定 Git 使用 UTF-8
git config --global core.quotepath false
git config --global gui.encoding utf-8
git config --global i18n.commit.encoding utf-8
git config --global i18n.logoutputencoding utf-8

# 顯示當前設定
Write-Host "=== Git 編碼設定已套用 ===" -ForegroundColor Green
Write-Host "PowerShell 輸出編碼: UTF-8" -ForegroundColor Cyan
Write-Host "Git core.quotepath: false" -ForegroundColor Cyan
Write-Host "Git gui.encoding: utf-8" -ForegroundColor Cyan
Write-Host ""

# 執行傳入的 git 指令
if ($args.Count -gt 0) {
    $command = $args -join " "
    Write-Host "執行指令: $command" -ForegroundColor Yellow
    Write-Host ""
    
    # 使用 cmd 執行 git 指令以確保正確處理編碼
    $result = cmd /c "chcp 65001 >nul && $command 2>&1"
    
    # 顯示結果
    $result | ForEach-Object { Write-Host $_ }
    
    # 返回結果
    return $result
} else {
    Write-Host "使用方式: .\git-helper.ps1 <git-command> [args]" -ForegroundColor Yellow
    Write-Host "範例: .\git-helper.ps1 git status" -ForegroundColor Yellow
    Write-Host "範例: .\git-helper.ps1 git add ." -ForegroundColor Yellow
    Write-Host "範例: .\git-helper.ps1 git commit -m '訊息'" -ForegroundColor Yellow
}
