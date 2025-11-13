@echo off
chcp 65001 >nul
cls
echo ================================================
echo       Git 操作工具 (UTF-8 編碼)
echo ================================================
echo.
echo 1. 查看狀態 (git status)
echo 2. 查看日誌 (git log)
echo 3. 提交變更 (git commit)
echo 4. 推送到遠端 (git push)
echo 5. 拉取最新 (git pull)
echo 6. 查看差異 (git diff)
echo 7. 自訂指令
echo 0. 離開
echo.
set /p choice=請選擇 (0-7): 

if "%choice%"=="1" goto status
if "%choice%"=="2" goto log
if "%choice%"=="3" goto commit
if "%choice%"=="4" goto push
if "%choice%"=="5" goto pull
if "%choice%"=="6" goto diff
if "%choice%"=="7" goto custom
if "%choice%"=="0" goto end

echo 無效的選擇！
pause
goto menu

:status
cls
echo [執行] git status
echo.
powershell -NoProfile -ExecutionPolicy Bypass -Command "[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; $OutputEncoding = [System.Text.Encoding]::UTF8; git status"
echo.
pause
goto menu

:log
cls
echo [執行] git log --oneline -10
echo.
powershell -NoProfile -ExecutionPolicy Bypass -Command "[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; $OutputEncoding = [System.Text.Encoding]::UTF8; git log --oneline -10 --graph --decorate"
echo.
pause
goto menu

:commit
cls
echo [執行] git add . 和 git commit
echo.
set /p msg=請輸入 commit 訊息: 
if "%msg%"=="" (
    echo 訊息不能為空！
    pause
    goto menu
)
echo.
powershell -NoProfile -ExecutionPolicy Bypass -Command "[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; $OutputEncoding = [System.Text.Encoding]::UTF8; git add .; git commit -m '%msg%'"
echo.
pause
goto menu

:push
cls
echo [執行] git push origin main
echo.
powershell -NoProfile -ExecutionPolicy Bypass -Command "[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; $OutputEncoding = [System.Text.Encoding]::UTF8; git push origin main"
echo.
pause
goto menu

:pull
cls
echo [執行] git pull origin main
echo.
powershell -NoProfile -ExecutionPolicy Bypass -Command "[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; $OutputEncoding = [System.Text.Encoding]::UTF8; git pull origin main"
echo.
pause
goto menu

:diff
cls
echo [執行] git diff
echo.
powershell -NoProfile -ExecutionPolicy Bypass -Command "[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; $OutputEncoding = [System.Text.Encoding]::UTF8; git diff"
echo.
pause
goto menu

:custom
cls
echo [自訂 Git 指令]
echo.
set /p cmd=請輸入 git 指令（不含 git 前綴）: 
if "%cmd%"=="" (
    echo 指令不能為空！
    pause
    goto menu
)
echo.
powershell -NoProfile -ExecutionPolicy Bypass -Command "[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; $OutputEncoding = [System.Text.Encoding]::UTF8; git %cmd%"
echo.
pause
goto menu

:menu
cls
echo ================================================
echo       Git 操作工具 (UTF-8 編碼)
echo ================================================
echo.
echo 1. 查看狀態 (git status)
echo 2. 查看日誌 (git log)
echo 3. 提交變更 (git commit)
echo 4. 推送到遠端 (git push)
echo 5. 拉取最新 (git pull)
echo 6. 查看差異 (git diff)
echo 7. 自訂指令
echo 0. 離開
echo.
set /p choice=請選擇 (0-7): 
goto %choice% 2>nul

:end
echo.
echo 感謝使用！
timeout /t 2 >nul
exit
