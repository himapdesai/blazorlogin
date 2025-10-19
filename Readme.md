# BlazorLogin

Hello! This is my first Blazor app. I am new developer and I am learning C# and Blazor. I hope you like my project.

## What this app do

- **User Registration:** You can make new account with username and password.
- **User Login:** You can login with your username and password.
- **Weather Forecast Page:** After login, you can see weather data. This data is from SQLite database.

## How to run

1. You need [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download) on your computer.
2. Open terminal and go to `BlazorLogin` folder.
3. To populate data run following command:
    ```
    dotnet ef migrations add Init
    ```
    ```
    dotnet ef  database update  
    ```
4. Run this command:
   ```
   dotnet run
   ```

5. Open browser and go to [http://localhost:5000](http://localhost:5000)

## How it works

- When you register, your password is saved as hash in database (SQLite).
- When you login, app check your username and password.
- If login is ok, you can see weather page.
- Weather data is in database and show in table.

## What I learn

- How to use Blazor Server
- How to use Entity Framework Core with SQLite
- How to make login and register page
- How to protect page with login

## What were the challenges

- I was not been able to find a way to use HttpContext in the razor code. Which is why I had to use cshtml.cs code to implement login process.

Thank you for looking my project! 😊