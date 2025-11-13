@echo off
chcp 65001 >nul
echo ³]©w UTF-8 ½s½X...
powershell -NoProfile -ExecutionPolicy Bypass -Command "[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; $OutputEncoding = [System.Text.Encoding]::UTF8; git log --oneline -10"
pause
