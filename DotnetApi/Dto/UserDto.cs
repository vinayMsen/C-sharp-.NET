

namespace DotnetApi.Dto;

public partial class UserDto
{
   /*In .NET Core, a DTO (Data Transfer Object) is a simple class used to 
   carry data between layers of an application without exposing the internal
    domain or database models. DTOs typically contain only properties—no 
    business logic—and are designed to improve security, performance, and maintainability.
    example userid,passwordlength

Key advantages include:
Security: Prevents exposing sensitive fields like passwords or tokens.
Data shaping: Sends only the required fields to clients.
Performance: Reduces payload size for faster network transfer.
Decoupling: Keeps API contracts independent from database schema changes.
Validation: Allows applying input validation before data reaches the database.
*/
//Here we donot have created property for UserId because it is auto incrementing
    public string FirstName {get; set;} = "";
    public string LastName {get; set;} = "";
    public string Email {get; set;} = "";
    public string Gender {get; set;} = "";
    public bool Active {get; set;}
}