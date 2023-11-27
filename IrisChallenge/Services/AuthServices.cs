using AutoMapper;
using Iris.Commons.Settings;
using Iris.Data;
using Iris.Shared.CommonsModels;
using Iris.Shared.InputModels;
using IrisChallenge.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IrisChallenge.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IUserServices _userServices;
        private readonly string _key;

        public AuthServices(IUserServices userServices, IOptions<GeneralSettings> options)
        {
            _userServices = userServices;
            _key = options.Value.SecretKey;
        }

        public async Task<List<User>> Authentication(UserInputModel userInputModel)
        {
            var users = await _userServices.SearchUserbyEmailAndPassword(userInputModel);
            return users;
        }

        public string createJWT(UserAuthModel user)
        {
            var jwtTokenHandlre = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Rol),
                new Claim(ClaimTypes.Name, user.name),
                new Claim("UserId", user.userId)
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials,
            };

            var token = jwtTokenHandlre.CreateToken(tokenDescriptor);
            return jwtTokenHandlre.WriteToken(token);
        }

        public bool ValidateJWT(string token)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
            };

            try
            {
                ClaimsPrincipal claimsPrincipal = jwtTokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void GetTokenInfo(string token)
        {
            ClaimsPrincipal claimsPrincipal = DecodeJWT(token);
            if (claimsPrincipal != null)
            {
                // Aquí puedes acceder a los claims que guardaste en el token.
                string userName = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                string userRole = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                // Ahora puedes hacer algo con esta información.
                Console.WriteLine($"User name: {userName}");
                Console.WriteLine($"User role: {userRole}");
            }
            else
            {
                // Aquí puedes manejar la situación en que el token no es válido.
                Console.WriteLine("Invalid token.");
            }
        }

        private ClaimsPrincipal DecodeJWT(string token)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
            };
            try
            {
                ClaimsPrincipal claimsPrincipal = jwtTokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return claimsPrincipal;
            }
            catch
            {
                // Si el token no es válido, puedes manejar la excepción aquí y, por ejemplo, devolver null.
                return null;
            }
        }

        public (bool, string) getUserIdfromToken(HttpContext httpContext)
        {
            (bool, string) resp = new();
            string? userId = null;
            if (httpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                // Aquí puedes utilizar el valor del encabezado de autorización como lo necesites
                string authorizationValue = authorizationHeader.ToString();
                string token = authorizationValue.Replace("Bearer ", "");
                if (ValidateJWT(token))
                {
                    ClaimsPrincipal claimsPrincipal = DecodeJWT(token);
                    resp.Item2 = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

                    resp.Item1 = true;

                }
                else
                {
                    resp.Item1 = false;
                    resp.Item2 = "";
                }

            }
            return resp;
        }
    }
}
