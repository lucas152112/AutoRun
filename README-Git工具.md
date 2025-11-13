# ?? 快速開始 - Git 中文編碼解決方案

## ?? 問題說明

在 Windows 的 CMD 或 PowerShell 中執行 Git 指令時，中文會顯示為亂碼。

## ? 解決方案（三選一）

### ?? 方案 1: 使用互動式選單（最簡單，推薦新手）

**步驟**:
1. 雙擊 `git-menu.bat`
2. 按照選單提示操作

**優點**:
- ? 最簡單，不需要記指令
- ? 清晰的選單介面
- ? 所有功能都有
- ? 完全不會有亂碼

**使用範例**:
```
雙擊 git-menu.bat
-> 選擇 1 (查看狀態)
-> 選擇 3 (提交變更)
-> 選擇 4 (推送)
-> 選擇 0 (離開)
```

---

### ?? 方案 2: 使用批次檔（簡單，適合快速操作）

**步驟**:
直接雙擊或在 CMD 中執行對應的批次檔

**可用的批次檔**:
- `git-status.bat` - 查看狀態
- `git-log.bat` - 查看日誌
- `git-commit.bat` - 提交變更
- `git-push.bat` - 推送
- `run-utf8.bat <指令>` - 執行任何指令

**優點**:
- ? 雙擊就能用
- ? 不需要設定
- ? 100% 可靠

**使用範例**:
```cmd
git-status.bat
git-commit.bat
git-push.bat
```

---

### ?? 方案 3: 安裝 PowerShell Profile（進階，適合常用指令行）

**步驟**:
1. 雙擊執行 `install-profile.bat`
2. 重新開啟 PowerShell

**優點**:
- ? 一次設定，永久有效
- ? 可以直接使用簡短別名
- ? 最接近原生 Git 體驗

**使用範例**:
```powershell
gs              # git status
gl              # git log
gc "更新文件"   # git add . && git commit
gp              # git push
gpu             # git pull
```

**可用的別名**:

| 別名 | 等同於 | 說明 |
|------|--------|------|
| `gs` | `git status` | 查看狀態 |
| `gl` | `git log --oneline -10 --graph` | 查看日誌 |
| `gc "訊息"` | `git add . && git commit -m "訊息"` | 提交變更 |
| `gp` | `git push origin main` | 推送 |
| `gpu` | `git pull origin main` | 拉取 |
| `gcp "訊息"` | commit + push | 提交並推送 |
| `gd` | `git diff` | 查看差異 |
| `gb` | `git branch -a` | 查看分支 |
| `gm` | 開啟 git-menu.bat | 互動式選單 |

---

## ?? 詳細文件

- ?? [Git批次檔工具使用說明.md](./Git批次檔工具使用說明.md) - 完整的批次檔使用說明
- ?? [Git操作規則與編碼設定.md](./Git操作規則與編碼設定.md) - 操作規則和設定說明
- ?? [PowerShell中文編碼解決方案.md](./PowerShell中文編碼解決方案.md) - 編碼問題詳細說明

---

## ?? 推薦的使用方式

### 給新手
?? 使用 **git-menu.bat** 互動式選單

### 給經常操作的使用者
?? 使用 **專用批次檔**（雙擊即可）

### 給指令行愛好者
?? 安裝 **PowerShell Profile**（使用簡短別名）

---

## ?? 常用操作快速參考

### 查看狀態
```
方案 1: git-menu.bat -> 選擇 1
方案 2: git-status.bat
方案 3: gs
```

### 提交變更
```
方案 1: git-menu.bat -> 選擇 3
方案 2: git-commit.bat
方案 3: gc "更新訊息"
```

### 推送到遠端
```
方案 1: git-menu.bat -> 選擇 4
方案 2: git-push.bat
方案 3: gp
```

### 查看歷史
```
方案 1: git-menu.bat -> 選擇 2
方案 2: git-log.bat
方案 3: gl
```

---

## ?? 疑難排解

### Q: 批次檔執行後仍有部分亂碼？

**A**: 這是正常的，只要主要內容（如檔名、commit 訊息）顯示正確即可。標題列的亂碼可以忽略。

### Q: PowerShell Profile 無法執行？

**A**: 執行 `install-profile.bat` 會自動設定執行政策。如果仍有問題，以管理員身分執行 PowerShell：
```powershell
Set-ExecutionPolicy RemoteSigned -Scope CurrentUser
```

### Q: 如何卸載 PowerShell Profile？

**A**: 
```powershell
Remove-Item $PROFILE
```

如果有備份：
```powershell
Move-Item ($PROFILE + '.backup') $PROFILE -Force
```

### Q: 批次檔可以用在其他專案嗎？

**A**: 可以！將所有 `.bat` 檔案複製到其他 Git 專案目錄即可使用。

---

## ?? 檔案清單

### 批次檔工具
- `git-menu.bat` - ? 互動式選單（推薦）
- `git-status.bat` - 查看狀態
- `git-log.bat` - 查看日誌
- `git-commit.bat` - 提交變更
- `git-push.bat` - 推送到遠端
- `run-utf8.bat` - 通用執行器

### PowerShell Profile
- `Microsoft.PowerShell_profile_example.ps1` - Profile 範例檔
- `install-profile.bat` - 自動安裝工具

### 說明文件
- `README-Git工具.md` - 本檔案（快速開始）
- `Git批次檔工具使用說明.md` - 詳細使用說明
- `Git操作規則與編碼設定.md` - 操作規則
- `PowerShell中文編碼解決方案.md` - 編碼問題說明

---

## ?? 開始使用

### 第一次使用

1. **選擇一個方案**（建議從方案 1 開始）
2. **測試一下**
   ```
   雙擊 git-menu.bat
   選擇 1 查看狀態
   ```
3. **如果正常顯示中文，就可以開始使用了！**

### 日常使用

根據您選擇的方案：
- **方案 1**: 每次雙擊 `git-menu.bat`
- **方案 2**: 雙擊對應的批次檔
- **方案 3**: 在 PowerShell 中使用別名

---

## ?? 需要幫助？

查看詳細文件：
- [Git批次檔工具使用說明.md](./Git批次檔工具使用說明.md)
- [Git操作規則與編碼設定.md](./Git操作規則與編碼設定.md)

---

**祝您使用愉快！不再為中文亂碼煩惱！** ??
