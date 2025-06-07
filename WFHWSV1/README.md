WFHWS – Windows File Housekeeping Service

ลบไฟล์ .tmp เก่ากว่า 7 วัน โดยใช้ Quartz.NET + .NET Windows Service
ตั้งเวลาได้ผ่าน Cron และแก้ได้จาก appsettings.json

# build and publish
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

# install as service
sc create WFHWS binPath= "C:\YourPath\WFHWSV1.exe"
sc start WFHWS
