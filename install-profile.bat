@echo off
chcp 65001 >nul
cls
echo ================================================
echo   PowerShell Profile 安裝工具
echo ================================================
echo.
echo 這個工具會自動設定 PowerShell Profile，
echo 讓您可以在 PowerShell 中直接使用 Git 指令，
echo 並且自動處理 UTF-8 編碼問題。
echo.
echo 安裝後，您可以使用以下別名：
echo   gs         - git status
echo   gl         - git log
echo   gc "訊息"  - git commit
echo   gp         - git push
echo   gpu        - git pull
echo   gcp "訊息" - commit + push
echo   gd         - git diff
echo   gb         - git branch
echo   gm         - 開啟選單
echo.
pause

echo.
echo 正在檢查 PowerShell Profile...
powershell -NoProfile -ExecutionPolicy Bypass -Command "if (!(Test-Path $PROFILE)) { Write-Host 'Profile 不存在，將建立新檔案' -ForegroundColor Yellow } else { Write-Host 'Profile 已存在，將備份現有檔案' -ForegroundColor Yellow; Copy-Item $PROFILE ($PROFILE + '.backup') -Force; Write-Host '已備份至: ' + $PROFILE + '.backup' -ForegroundColor Green }"

echo.
echo 正在複製 Profile 內容...
powershell -NoProfile -ExecutionPolicy Bypass -Command "New-Item -ItemType Directory -Force -Path (Split-Path $PROFILE) | Out-Null; Copy-Item 'Microsoft.PowerShell_profile_example.ps1' $PROFILE -Force; Write-Host '? Profile 已安裝至: ' + $PROFILE -ForegroundColor Green"

echo.
echo 正在設定執行政策...
powershell -NoProfile -ExecutionPolicy Bypass -Command "Set-ExecutionPolicy RemoteSigned -Scope CurrentUser -Force; Write-Host '? 執行政策已設定' -ForegroundColor Green"

echo.
echo ================================================
echo   安裝完成！
echo ================================================
echo.
echo 請執行以下其中一個動作：
echo   1. 關閉並重新開啟 PowerShell
echo   2. 或執行: . $PROFILE
echo.
echo 之後就可以直接使用 Git 別名了！
echo.
pause
