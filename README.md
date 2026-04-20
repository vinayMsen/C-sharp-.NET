***DotnetApi*** 
This folder contains my first webapi dotnet project. Where i created a random forecast generator and created controller user and weather by inheriting the controllerbase.


----
**TO launch specific profile in the launchsettings.json**
dotnet watch run --launch-profile https


**Frequently Used Command**
git status
git add Dotnetapief/
git status
git commit -m "Only Dotnetapief changes"
git push

**18/03/2026**
I created UserRepository and  IUserRepository for the abstraction .
Their Job:-
    Act as a middle layer
    Hide direct interaction with Entity Framework (DbContext)
    Provide reusable DB operations

**22/03/2026**
Create DotNetCourse.Auth Table with email,passwordsalt(varbinary(max)) and
    passwordHas(varbinary(max)). 