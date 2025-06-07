# 🧹 WFHWS – Windows File Housekeeping Service

A Windows Background Service built with **.NET 8 + Quartz.NET** for scheduled file cleanup jobs.

---

## 📌 Features

- ✅ **Runs as Windows Service**
- 🕒 **Scheduled job with Quartz.NET**
- 🗑️ Auto-delete `.tmp` files older than N days
- ⚙️ Configurable via `appsettings.json`
- 📄 Minimal, production-ready `.exe`



# build and publish
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

# install as service
sc create WFHWS binPath= "C:\YourPath\WFHWSV1.exe"
sc start WFHWS
