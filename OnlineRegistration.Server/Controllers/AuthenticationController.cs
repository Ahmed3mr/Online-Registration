using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineRegistration.Server.Configurations;
using OnlineRegistration.Shared.Models;
using OnlineRegistration.Shared.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineRegistration.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        //private readonly JwtConfig _jwtConfig;

        public AuthenticationController(
            UserManager<IdentityUser> userManager, 
            IConfiguration configuration
            //JwtConfig jwtConfig
            )
        {
            _userManager = userManager;
            _configuration = configuration;
           // _jwtConfig = jwtConfig;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
        {
            if(ModelState.IsValid)
            {

                var user_exist = await _userManager.FindByEmailAsync(requestDto.Email);

                if(user_exist != null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result= false,
                        Errors= new List<string>()
                        {
                            "Email Already Exists!!!"
                        }
                    });
                }

                var new_user = new IdentityUser()
                {
                    Email = requestDto.Email,
                    UserName = requestDto.Name
                };

                var is_created = await _userManager.CreateAsync(new_user,requestDto.Password);

                if(is_created.Succeeded) {

                    var token = GenerateToken(new_user);
                    return Ok(new AuthResult()
                    {
                        Result= true,
                        Token = token
                    });
                
                }

                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                        {
                            "Server Error"
                        }
                });

            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto loginRequest)
        {
            if (ModelState.IsValid)
            {
                var existing_user = await _userManager.FindByEmailAsync(loginRequest.Email);

                if (existing_user == null) {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invaild Payload2"
                        }
                    });

                    var iscorrect = await _userManager.CheckPasswordAsync(existing_user, loginRequest.Password);
                    if (!iscorrect)
                    {
                        return BadRequest(new AuthResult()
                        {
                            Result = false,
                            Errors = new List<string>()
                            {
                                "Invalid Credentials"
                            }
                        });


                        var jwtToken = GenerateToken(existing_user); 
                        return Ok(new AuthResult()
                        {
                            Result = true,
                            Token = jwtToken
                        });
                    }
                }

            }

            return BadRequest(new AuthResult()
            {
                Result= false,
                Errors = new List<string>()
                {
                    "Invalid Payload"
                }
            });
        }


        private string GenerateToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("ID",user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()) ,
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToUniversalTime().ToString())
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
            

        }



    }
}
