# _Pierre's Sweet and Savory Treats_

#### By _**Jessi Baker**_

#### _Pierre's Sweet and Savory Treats - Epicodus - C# and .NET - Authentication with Identity - Code Review_

## Technologies Used

* GIT
* CS
* ASP.NET MVC
* MYSQL
* Entity Framework
* LINQ

## Description

This is a web app using user authentication and a many-to-many relationship where a treat can have many flavors (such as sweet, savory, spicy, or creamy) and a flavor can have many treats. For instance, the "sweet" flavor could include chocolate croissants, cheesecake, and so on. While all users have read functionality, a logged in users can create, update and delete sweets & treats. The splash page lists all treats and flavors. Users should be able to click on an individual treat or flavor to see all the treats/flavors that belong to it.

## Setup/Installation Requirements

* Clone Repo: In your terminal navigate to your desktop or other desired location and enter `git clone https://github.com/jessb-epicodus/Pierre.git`
* Add Required Packages: Navigate to the top level of the project directory called Pierre & enter each of the following commands.
  `dotnet add package Microsoft.EntityFrameworkCore -v 5.0.0`
  `dotnet add package Pomelo.EntityFrameworkCore.MySql -v 5.0.0-alpha.2`
  `dotnet add package Microsoft.EntityFrameworkCore.Proxies -v 5.0.0`
  `dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore -v 5.0.0`
* Protect Your MySQL Password: Enter `touch .gitignore` & `touch appsettings.json` in the command line
* Add the following code to _appsettings.json_ & fill in your password where designated  
  `{  
    "ConnectionStrings": {  
      "DefaultConnection": "Server=localhost;Port=3306;database=jessi_baker;uid=root;pwd=YOUR-PASSWORD-HERE;"  
    }  
  }`  
* Add _*/appsettings.json_ to _.gitignore_
* Update Databae: Enter `dotnet ef migrations add Initial` & `dotnet ef database update` in the terminal
* Install Dependeciey: Enter `dotnet restore` in your terminal
* Try Out This Web App: Enter `dotnet run` in the command line and navigte navigate to _http://localhost:5000/_ in your browser
* _This is not yet published_
## Known Bugs

* No known issues

## License

Copyright (c) _Mar 2022_ _Jessi Baker_

## Contact

_If you run into any issues or have questions, ideas or concerns or wish to make a contribution to the code see contact information below._
* Jessi Baker <jessb.epicodus@gmail.com>