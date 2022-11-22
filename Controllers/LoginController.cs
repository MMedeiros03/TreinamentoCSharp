using Microsoft.AspNetCore.Mvc;
using System.Data;
using TreinamentoMarinho.Entities;
using TreinamentoMarinho.Repository;

namespace TreinamentoMarinho.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
            [HttpPost]
            public ActionResult<string> Login([FromBody] UserEntityValidation body) // forma de pegar conteudo de um body;
            {
                try
                {
                    bool result = new LoginRepository().Login(body);

                    return result ? Ok("Usuário existe na base de dados") : Ok("Usuário não existe na base de dados");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erro: {ex} ");
                }
            }
    }
}