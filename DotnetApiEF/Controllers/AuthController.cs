
using Microsoft.AspNetCore.Mvc;
using DotnetApiEF.Dto;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using System.Text;
using Dapper;
using DotnetApiEF.Data;
using Microsoft.Data.SqlClient; // For SqlParameter and SqlConnection
using System.Data;             // For SqlDbType
using Microsoft.AspNetCore.Cryptography.KeyDerivation; // For PBKDF2 hashing


namespace DotnetApiEF.Controllers
{

    [Authorize] // This attribute indicates that all endpoints in this controller require authentication
    [ApiController] // send and recieve json data
    [Route("[controller]")] // route to the controller

    public class AuthController: ControllerBase // is the base class for API controllers.
    {
        private readonly DataContextDapper _dapper; 
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config) //It’s how your code reads your appsettings.json file
        {
            _dapper = new DataContextDapper(config);
            _config = config;
            /*_config = config;: You are taking the configuration the system gave you and saving it into
             your private variable so other methods (like your Login or Register functions) can use it later*/
        }

        [AllowAnonymous] // This attribute allows unauthenticated access to this specific endpoint, overriding the [Authorize] attribute at the controller level.
        [HttpPost("Register")]
            public IActionResult Register(UserForRegistrationDto userForRegistration)
            {
                if (userForRegistration.Password == userForRegistration.PasswordConfirmation)
                {
                    string sqlCheckUserExist = "SELECT Email FROM Auth WHERE Email = '" + userForRegistration.Email + "'";

                   
                    IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheckUserExist);
                    
                    if (existingUsers.Count() == 0)
                    {   
                        byte[] passwordSalt = new byte[128 / 8];
                        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                        {
                            rng.GetNonZeroBytes(passwordSalt);
                        }

                       
                        byte[] passwordHash = GetPasswordHash(userForRegistration.Password, passwordSalt);

                        string sqlAddAuth = @"INSERT INTO Auth (Email, PasswordHash, PasswordSalt)
                                            VALUES ('" + userForRegistration.Email + "', @PasswordHash, @PasswordSalt)";

                     
                        List<SqlParameter> sqlParameters = new List<SqlParameter>();

                        SqlParameter passwordSaltParameter = new SqlParameter("@PasswordSalt", SqlDbType.VarBinary);
                        passwordSaltParameter.Value = passwordSalt; // Use the byte array here

                        SqlParameter passwordHashParameter = new SqlParameter("@PasswordHash", SqlDbType.VarBinary);
                        passwordHashParameter.Value = passwordHash; // Use the byte array here

                        sqlParameters.Add(passwordSaltParameter);
                        sqlParameters.Add(passwordHashParameter);

                        
                        if (_dapper.ExecuteSqlParameter(sqlAddAuth, sqlParameters))
                         { 
                             string sqlAddUser =@"INSERT INTO Users (FirstName, LastName, Email, Gender, Active) 
                                            VALUES ("+
                                            "'"    + userForRegistration.FirstName+
                                            "', '" + userForRegistration.LastName+
                                            "', '" + userForRegistration.Email+
                                            "', '" + userForRegistration.Gender+
                                            "', 1)";

                             if(_dapper.ExecuteSql(sqlAddUser))
                                {
                                    return Ok();
                                }
                            throw new Exception("Failed to Add user");
                         }

                        throw new Exception("Failed to Register user");
                    }
                    throw new Exception("User Already Exists");
                }
                throw new Exception("Passwords Do Not Match");
            }
        
       [AllowAnonymous] 
       [HttpPost("Login")]
        public IActionResult Login(UserForLoginDto userForLogin)
        {
            string sqlForHashandSalt= @"Select PasswordHash,PasswordSalt from Auth
             Where Email= '"+userForLogin.Email+"'";

            UserForLoginConfirmationDto userForConfirmation= _dapper.
            LoadDataSingle<UserForLoginConfirmationDto>(sqlForHashandSalt); 

            byte[] passwordHash= GetPasswordHash(userForLogin.Password,userForConfirmation.PasswordSalt);

            //if(passwordHash== userForConfirmation.PasswordHash) //won't work because both are objects and different
            for(int index=0; index<passwordHash.Length; index++)
            {
                if(passwordHash[index] != userForConfirmation.PasswordHash[index])
                return StatusCode(401,"Password  incorrect"); 
            }

            string userIdSql= @"Select UserId from Users Where Email='"
            +userForLogin.Email + "'";
            int userId= _dapper.LoadDataSingle<int>(userIdSql);
            return Ok(new Dictionary<string, string>
            {
                {"token", CreateToken(userId)}
            });

        }


        /* In .NET Core (and generally in authentication systems), 
          a refresh token is a long-lived credential used to obtain a new access token without
         requiring the user to log in again.*/
        [HttpGet("RefreshToken")]
        public IActionResult RefreshToken()
        {
           int userId = int.Parse(User.FindFirst("userId")?.Value ?? "0"); // get userId from the claims in the token

            string userIdSql= "Select UserId from Users Where UserId="+userId;
            int userIdFromDb = _dapper.LoadDataSingle<int>(userIdSql);

            return Ok(new Dictionary<string, string>
            {
                {"token", CreateToken(userId)}
            });
            
        }

        private byte[] GetPasswordHash (string password, byte[] passwordSalt)
        {
            string passwordsaltplusString= _config.GetSection("AppSettings:Passwordkey").Value+
                    Convert.ToBase64String(passwordSalt);

           return  KeyDerivation.Pbkdf2(
                        password: password, // from arguments
                        salt: Encoding.ASCII.GetBytes(passwordsaltplusString),
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 1000000,
                        numBytesRequested:  256/8
                         );

        }


        // new helper method which will return a string for JWT 
       private string CreateToken(int userId)
            {
                // 1. Define the Claims (The user data inside the token)
                Claim[] claims = new Claim[]
                {
                    new Claim("userId", userId.ToString())
                };

                // 2. Create the Security Key (The "Secret" used to sign the token)
                
                string? tokenKeyString = _config.GetSection("AppSettings:TokenKey").Value;
 
                SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        tokenKeyString != null ? tokenKeyString : ""
                    )
                );

                // 3. Define the Signing Credentials (The algorithm used for the signature)
               
                SigningCredentials credentials = new SigningCredentials(
                    tokenKey,
                    SecurityAlgorithms.HmacSha512Signature);

                // 4. Describe the Token (What's in it, who signed it, when it expires)
               
                SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(claims),
                    SigningCredentials = credentials,
                    Expires = DateTime.Now.AddDays(1)
                };

                // 5. Create and Write the Token
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken token = tokenHandler.CreateToken(descriptor);

                return tokenHandler.WriteToken(token);
            }

    }
}







/*
        [HttpPost("Register")]
        public IActionResult Register(UserForRegistrationDto userForRegistration)
        {
            if(userForRegistration.Password==userForRegistration.PasswordConfirmation)
            {
                //checking if user exist with same Email
                string sqlcheckuserexist = "select Email from Auth where Email= '"+
                userForRegistration.Email+"'";

                IEnumeratble<string> existingusers=  _dapper.LoadData<string>(sqlcheckuserexist);
                if(existinguser.Count() ==0)
                {   
                    byte[] passwordsalt= new byte[128 / 8];
                    using(RandomNumberGenerator rng= RandomNumberGenerator.Create())
                    {
                        rng.GetNonZeroBytes(passwordsalt);
                    }
                    // string passwordsaltplusString= _config.GetSection("AppSettings:Passwordkey").Value+
                    // Convert.ToBase64String(passwordsalt); // no need cause GetPasswordHash() will create this

                    byte[] passwordhash = GetPasswordHash(userForRegistration.Password,passwordSalt);

                    
                    string sqladdauth= @"insert into Auth (Email, PasswordHash,PasswordSalt)
                     Values ('"+userForRegistration.Email+
                      "', @PasswordHash,  @PasswordSalt) ";


                    List<SqlParamter> sqlParamter = new List<SqlParamter>();

                    SqlParamter passwordSaltParameter= new SqlParamter("@PasswordSalt", SqlDbType.VarBinary);
                    passwordSaltParamenter.Value=  PasswordSalt;

                    sqlParamter passwordHashParameter= new SqlParamter("@PasswordHash", SqlDbType.VarBinary);
                    passwordHashParamenter.Value=  PasswordHash;

                    sqlParameter.Add(passwordSaltParamter);
                    sqlParameter.Add(passwordHashParamter);


                    if(_dapper.ExecuteSqlParameter(sqladdauth, sqlParameter))
                    return Ok();

                    throw new Exception ("Failed to Register user");
                }

                throw new Exception ("User Already Exist with this email");
            }
            throw new Exception ("Password Do Not Match");
           
        }

        [HttpPost("Login")]
        public IActionResult Login(UserForLoginDto userForLogin)
        {
            string sqlForHashandSalt= @"Select PasswordHash,PasswordSalt from Auth
             Where Email= '"+userForLogin.Email+"'";

            UserForLoginConfirmationDto userForConfirmation= _dapper.
            LoadDataSingle<UserForLoginConfirmationDto>(sqlHashandSalt); 

            byte[] passwordHash= GetPasswordHash(userForLogin.Password,userForConfirmation.PasswordSalt);

            //if(passwordHash== userForConfirmation.PasswordHash) //won't work because both are objects and different
            for(int index=0; index<passwordHash.Length; index++)
            {
                if(passwordHash != userForConfirmation.PasswordHash[index])
                return StatusCode(401,"Password  incorrect"); 
            }
            return Ok();
        } */
