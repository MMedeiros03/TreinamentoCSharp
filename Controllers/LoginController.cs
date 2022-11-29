using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TreinamentoMarinho.Entities;
using TreinamentoMarinho.Repository;
using TreinamentoMarinho.Services.Authorization;

namespace TreinamentoMarinho.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
            [HttpPost]
            [AllowAnonymous]
            public ActionResult<string> Login([FromBody] UserEntityValidation body) // forma de pegar conteudo de um body;
            {
                try
                {
                UserEntity user = new LoginRepository().Login(body);

                return user != null ? 
                    Ok(new { 
                        UserData = user,
                        Token = TokenService.GenerateToken(user)
                    }) 
                    : Ok("Usuário não existe na base de dados");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erro: {ex} ");
                }
            }
    }
}