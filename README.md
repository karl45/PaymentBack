# .NET CLI:
```
cd /path_where_<.csproj>
dotnet run <PaymentBack.csproj>
```
or
```
dotnet run --project /path_where_<.csproj>
```
# Visual Studio:
# При запуске проекта убедитесь что запускаемый проект должен быть Payment.Web!!!!
<img width="395" height="345" alt="image" src="https://github.com/user-attachments/assets/8684e09a-ea57-4f93-b6ce-5d26c9cb21a1" />

# Если не он запускамый проект, то через правую кнопкой мыши кликаете на Payment.Web и выберите "Назначить автозапускаемым проектом"
# Запускаемые проекты выделяются курсивом как на скриншоте


# Migration 
Все миграции применяются автоматически. Если возникнет следующая ошибка:
<img width="1054" height="41" alt="image" src="https://github.com/user-attachments/assets/e850e923-3236-491f-ab51-e62b38d8957f" />
не переживайте, просто в начале когда база не создана, он при подключении падает, но потом создаёт базу и применяет миграции. Ничего дополнительно делать не нужно
# Change connection string by yourself in appsetings.json file
# Signature key in appsettings.json file (FrontEnd:SecretKey)









