# To Do List

### New branches for each lesson

* _In VS Code open a new terminal_
  1. _In VS Code termainl run:  ```dotnet add package Microsoft.EntityFrameworkCore -v 6.0.0```_
  2. _In VS Code terminal run:  ```dotnet add package Pomelo.EntityFrameworkCore.MySql -v 6.0.0```_
  
* _New Commands for Migrations_
  1. _In VS Code terminal run:  ```dotnet add package Microsoft.EntityFrameworkCore.Design -v 6.0.0```_
    - Run when making a new project that needs migrations
  2. _In VS Code terminal run:  ```dotnet ef migrations add Initial```_
    - Run to add Migration folder
  3. _In VS Code terminal run:  ```dotnet ef database update```_
    - Run after making changes
  4. _In VS Code terminal run:  ```dotnet ef migrations remove```_
    - Run if needing to remove previous update (then run update command again)
