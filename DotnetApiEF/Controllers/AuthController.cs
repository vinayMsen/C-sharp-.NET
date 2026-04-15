
using Microsoft.AspNetCore.Mvc;
using DotnetApiEF.Dto;
using System.Security.Cryptography;
using System.Text;
using Dapper;

using DotnetApiEF.Data;
using Microsoft.Data.SqlClient; // For SqlParameter and SqlConnection
using System.Data;             // For SqlDbType
using Microsoft.AspNetCore.Cryptography.KeyDerivation; // For PBKDF2 hashing


namespace DotnetApiEF.Controllers
{
    
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

        [HttpPost("Register")]
            public IActionResult Register(UserForRegistrationDto userForRegistration)
            {
                if (userForRegistration.Password == userForRegistration.PasswordConfirmation)
                {
                    string sqlCheckUserExist = "SELECT Email FROM Auth WHERE Email = '" + userForRegistration.Email + "'";

                    // Fixed variable name consistency (existingUsers)
                    IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheckUserExist);
                    
                    if (existingUsers.Count() == 0)
                    {   
                        byte[] passwordSalt = new byte[128 / 8];
                        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                        {
                            rng.GetNonZeroBytes(passwordSalt);
                        }

                        // Fixed: passwordSalt variable name passed correctly
                        byte[] passwordHash = GetPasswordHash(userForRegistration.Password, passwordSalt);

                        string sqlAddAuth = @"INSERT INTO Auth (Email, PasswordHash, PasswordSalt)
                                            VALUES ('" + userForRegistration.Email + "', @PasswordHash, @PasswordSalt)";

                        // Fixed: Correct spelling (SqlParameter) and list name consistency
                        List<SqlParameter> sqlParameters = new List<SqlParameter>();

                        SqlParameter passwordSaltParameter = new SqlParameter("@PasswordSalt", SqlDbType.VarBinary);
                        passwordSaltParameter.Value = passwordSalt; // Use the byte array here

                        SqlParameter passwordHashParameter = new SqlParameter("@PasswordHash", SqlDbType.VarBinary);
                        passwordHashParameter.Value = passwordHash; // Use the byte array here

                        sqlParameters.Add(passwordSaltParameter);
                        sqlParameters.Add(passwordHashParameter);

                        // Ensure your DataContextDapper has this method name exactly
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
            return Ok();
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
