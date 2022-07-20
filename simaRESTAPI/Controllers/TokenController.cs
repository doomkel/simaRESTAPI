using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using simaRESTAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Text;

namespace simaRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly simaServiceContext _context;
        public TokenController(IConfiguration configuration, simaServiceContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Usuarios _userData)
        {
            if (_userData != null && _userData.Usuario != null && _userData.Clave != null)
            {
                var user = await GetUsuarios(_userData.Usuario, _userData.Clave);
                
                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Cod_usuario", user.CodUsuario.ToString()),
                        new Claim("Usuario", user.Usuario.ToString()),
                        new Claim("Perfil", user.Perfil.ToString())

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Credenciales incorrectas");
                }

            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Usuarios> GetUsuarios(string _usuario, string _password)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Usuario == _usuario && u.Clave == _password);
        }


    }
}
