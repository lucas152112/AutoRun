@echo off
chcp 65001 >nul
echo 設定 UTF-8 編碼...
echo.
echo 請輸入 commit 訊息（不含引號）:
set /p msg=
echo.
echo 執行 git add . 和 git commit...
powershell -NoProfile -ExecutionPolicy Bypass -Command "[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; $OutputEncoding = [System.Text.Encoding]::UTF8; git add .; git commit -m '%msg%'"
echo.
pause
