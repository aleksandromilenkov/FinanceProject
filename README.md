#FullStack .NET Application  
FinShark is a Finance App where you can Register, Login, Search, Comment and more!  
The user once loged in, can read stock data from every company.  
Even you can check their 10K Forms which are 100% accurate because they are displaying data from the archives of www.sec.gov  
The app is using data from financialmodelingprep.com's free API.  
Every user can Add stock to their own portfolio and remove it if he likes.  
Every user can leave a comment in the stock's page.  

I'm using React.js with TypeScript and TailwindCss for the UI and ASP.NET Web API for the backend with Microsoft SQl Server Database.  

In the frontend i'm using the following npm packages:  
1. Axios - for better manipulating with my API  
2. Dotenv - for secretly storing my API Key for financialmodelingprep.com API  
3. React-Hook-Form - for better experience with forms  
4. React-Icons - for beautiful icons  
5. React-Router-Dom v6 - for navigating easily in the app  
6. React-Spinners - for beautiful spinners while waiting when fetching the data   
7. React-Toastify - for beautiful messages in the top right corner (either success or warning/error messages)   
8. Yup - for validating forms in the frontend   

In the backend i'm using the following dotnet packages:  
1.Microsoft.AspNetCore.Authentication.JwtBearer - For bearer tokens when Authorizing when accessing the API  
2.Microsoft.AspNetCore.Identity.EntityFrameworkCore - For better manipulation with the Microsoft SQL Database  
3.Microsoft.AspNetCore.Mvc.NewtonsoftJson - For easily converting to JSON  
4.Microsoft.AspNetCore.OpenApi - For documenting the API like the version, securifty requirenment and defenition with schemes  
5.Microsoft.EntityFrameworkCore.SqlServer - For Microsoft SQL Server Database  
6.Microsoft.Extensions.Identity.Core - For easily creating users, loging, logout and more  

Here are some images from the App's UI:   

1.Home Page  
![finshark_home](https://github.com/aleksandromilenkov/FinanceProject/assets/64156983/4dba17f0-677f-45e2-a9ef-db4d8b83ad00)

2.Search Page  
![finshark_search](https://github.com/aleksandromilenkov/FinanceProject/assets/64156983/0f87ae67-c62e-4202-a91f-df66302b06ce)

3.Company Page  
![finshark_company-profile](https://github.com/aleksandromilenkov/FinanceProject/assets/64156983/259bc6ca-7061-4e32-863c-2abc7f4209bf)

4.Comments in the Company Page  
![finshark_comments](https://github.com/aleksandromilenkov/FinanceProject/assets/64156983/a728e977-60a9-424f-ac19-b6505df91b4f)

4.Income Statement  
![finshark_incomeStatement](https://github.com/aleksandromilenkov/FinanceProject/assets/64156983/05e0c29f-03e2-43d4-b71e-69ab4528c10f)

5.Balance sheet  
![finshark_balanceSheet](https://github.com/aleksandromilenkov/FinanceProject/assets/64156983/47741d97-c623-4238-bf43-9b3a67e74683)

6.Cashflow statement  
![finshark_cashflowStatement](https://github.com/aleksandromilenkov/FinanceProject/assets/64156983/12db87d5-8c5d-48d0-944a-265e7ebfa9f6)

7.Form 10K  
![finshark_Form10k](https://github.com/aleksandromilenkov/FinanceProject/assets/64156983/108e722d-390a-4e74-aa16-6dce245990be)

8.Login   
![finshark_login](https://github.com/aleksandromilenkov/FinanceProject/assets/64156983/a40c3a1a-4481-4f88-94d6-728e30ba4392)

9.Register   
![finshark_register](https://github.com/aleksandromilenkov/FinanceProject/assets/64156983/62280b8e-eac8-4dee-93e4-86801b91a727)
