# ===========================================
# AutoRun 專案 - PowerShell Profile
# UTF-8 編碼自動設定
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

# ===========================================
# Git 輔助函數
# ===========================================

function Git-Status {
    <#
    .SYNOPSIS
    查看 Git 倉庫狀態
    .EXAMPLE
    Git-Status
    gs
    #>
    git status
}

function Git-Log {
    <#
    .SYNOPSIS
    查看 Git 提交歷史（圖形化，最近 10 筆）
    .EXAMPLE
    Git-Log
    gl
    #>
    param(
        [int]$Count = 10
    )
    git log --oneline -$Count --graph --decorate
}

function Git-Commit {
    <#
    .SYNOPSIS
    暫存所有變更並提交
    .PARAMETER Message
    提交訊息
    .EXAMPLE
    Git-Commit "feat: 新增功能"
    gc "feat: 新增功能"
    #>
    param(
        [Parameter(Mandatory=$true)]
        [string]$Message
    )
    
    Write-Host "執行 git add ..." -ForegroundColor Yellow
    git add .
    
    Write-Host "執行 git commit ..." -ForegroundColor Yellow
    git commit -m $Message
    
    Write-Host "? 提交完成" -ForegroundColor Green
}

function Git-Push {
    <#
    .SYNOPSIS
    推送到遠端 origin/main
    .EXAMPLE
    Git-Push
    gp
    #>
    Write-Host "執行 git push origin main ..." -ForegroundColor Yellow
    git push origin main
    Write-Host "? 推送完成" -ForegroundColor Green
}

function Git-Pull {
    <#
    .SYNOPSIS
    從遠端 origin/main 拉取
    .EXAMPLE
    Git-Pull
    gpu
    #>
    Write-Host "執行 git pull origin main ..." -ForegroundColor Yellow
    git pull origin main
    Write-Host "? 拉取完成" -ForegroundColor Green
}

function Git-CommitAndPush {
    <#
    .SYNOPSIS
    提交並推送（一次完成）
    .PARAMETER Message
    提交訊息
    .EXAMPLE
    Git-CommitAndPush "feat: 新增功能"
    gcp "feat: 新增功能"
    #>
    param(
        [Parameter(Mandatory=$true)]
        [string]$Message
    )
    
    Git-Commit $Message
    if ($LASTEXITCODE -eq 0) {
        Git-Push
    } else {
        Write-Host "? 提交失敗，未執行推送" -ForegroundColor Red
    }
}

function Git-Diff {
    <#
    .SYNOPSIS
    查看變更差異
    .EXAMPLE
    Git-Diff
    gd
    #>
    git diff
}

function Git-Branch {
    <#
    .SYNOPSIS
    查看所有分支
    .EXAMPLE
    Git-Branch
    gb
    #>
    git branch -a
}

function Git-Menu {
    <#
    .SYNOPSIS
    開啟 Git 互動式選單
    .EXAMPLE
    Git-Menu
    gm
    #>
    & "$PSScriptRoot\git-menu.bat"
}

# ===========================================
# 設定別名
# ===========================================

Set-Alias -Name gs -Value Git-Status
Set-Alias -Name gl -Value Git-Log
Set-Alias -Name gc -Value Git-Commit
Set-Alias -Name gp -Value Git-Push
Set-Alias -Name gpu -Value Git-Pull
Set-Alias -Name gcp -Value Git-CommitAndPush
Set-Alias -Name gd -Value Git-Diff
Set-Alias -Name gb -Value Git-Branch
Set-Alias -Name gm -Value Git-Menu

Write-Host "? Git 別名已設定" -ForegroundColor Green
Write-Host ""
Write-Host "可用的 Git 別名:" -ForegroundColor Cyan
Write-Host "  gs         - git status" -ForegroundColor Gray
Write-Host "  gl         - git log (圖形化)" -ForegroundColor Gray
Write-Host "  gc '訊息'  - git add . && git commit -m '訊息'" -ForegroundColor Gray
Write-Host "  gp         - git push origin main" -ForegroundColor Gray
Write-Host "  gpu        - git pull origin main" -ForegroundColor Gray
Write-Host "  gcp '訊息' - git commit + push (一次完成)" -ForegroundColor Gray
Write-Host "  gd         - git diff" -ForegroundColor Gray
Write-Host "  gb         - git branch -a" -ForegroundColor Gray
Write-Host "  gm         - 開啟互動式選單" -ForegroundColor Gray
Write-Host ""

# ===========================================
# 專案特定設定（可選）
# ===========================================

# 自動切換到專案目錄（可選）
# Set-Location "D:\work\csharp\AutoRun"

# 顯示當前 Git 狀態（可選）
# if (Test-Path .git) {
#     Write-Host "Git 倉庫狀態:" -ForegroundColor Cyan
#     git status -s
# }
