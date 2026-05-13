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


**12/05/2026**
   # Added package Authentication.JwtBearer
   # https://www.jwt.io/ to decode the token

    Added Authentication with JWT (JWT (JSON Web Token) authentication is one of the most popular and secure ways to protect APIs in ASP.NET Core. It allows you to build scalable, stateless authentication systems where the server does not need to store session data.)
    

**13/05/2026**
   *** implemented JWT Validation using These symmetricsecurity key to create tokenvalidatoin parameters***
   *** Implemented RefreshToken: ***
            User logs in → Server issues:
        Access Token (short expiry)
        Refresh Token (long expiry, e.g., days or weeks)
        Client stores the refresh token securely (never in localStorage if possible — use secure cookies or encrypted storage).
        When the access token expires:
        Client sends the refresh token to the server’s token refresh endpoint.
        Server validates the refresh token.
        If valid, server issues a new access token (and optionally a new refresh token).
        If the refresh token is invalid or expired, the user must log in again.
