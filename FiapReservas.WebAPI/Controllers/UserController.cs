using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.Domain.Utils.Cryptography;
using FiapReservas.WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FiapReservas.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]    
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("listaTodos")]
        public async Task<IEnumerable<User>> Listar()
        {
            return await _service.List(x => true);
        }

        [AllowAnonymous]
        [HttpPost("registrar")]
        [ProducesResponseType(typeof(User), 201)]
        [ProducesResponseType(typeof(void), 401)]
        public async Task<IActionResult> Inserir(UserInsertDTO dto)
        {
            var user = new User()
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Password = PasswordCryptography.Encrypt(dto.Password)
            };

            await _service.Insert(user);
            return Ok(user);
        }

        [HttpPut("alterar")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(void), 404)]
        [ProducesResponseType(typeof(void), 401)]
        public async Task<IActionResult> Update(UserUpdateDTO dto)
        {
            var user = await _service.Get(dto.Id);

            if (user == null)
            {
                return NotFound();
            }

            user.Nome = dto.Nome;
            user.Email = dto.Email;
            user.Password = PasswordCryptography.Encrypt(dto.Password) ?? user.Password;

            await _service.Update(user);
            return Ok(user);
        }

        [HttpDelete("apagar/{id:Guid}")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(void), 401)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _service.Get(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(void), 401)]
        public async Task<ActionResult> Login(UserLoginDTO dto)
        {
            //if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var user = await _service.GetByEmail(dto.Email);
            if (user != null)
            {
                if (user.ValidatePassword(dto.Password))
                {
                    return Ok(GerarJwt());
                }
            }

            return Unauthorized("Usuário ou senha incorretos");
        }

        private string GerarJwt()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JwtSecret"));

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("JwtIssuer"),
                Audience = Environment.GetEnvironmentVariable("JwtAudience"),
                Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(Environment.GetEnvironmentVariable("JwtExpiracaoHoras") ?? "1")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }
    }
}
