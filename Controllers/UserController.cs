using Microsoft.AspNetCore.Mvc;
using TreinamentoMarinho.Entities;
using TreinamentoMarinho.Repository;

namespace TreinamentoMarinho.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> Get(int id = 0)
        {
            try
            {
                if(id > 0)
                {
                    UserEntity result = new UserRepository().GetById(id);
                    return result != null ? Ok(result) : StatusCode(404, "Nenhum resultado encontrado");
                }
                else
                {
                    List<UserEntity> result = new UserRepository().GetAll();
                    return Ok(result);
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro: {ex} ");
            }
        }
        [HttpPost]
        public ActionResult<string> Save([FromBody] UserEntity body) // forma de pegar conteudo de um body;
        {
            try
            {
                new UserRepository().Save(body);
                return Ok("save");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro: {ex} ");
            }
        }

        [HttpPut]
        public ActionResult<string> Update([FromBody] UserEntity body)
        {
            try
            {
                new UserRepository().Update(body);
                return Ok("Registro atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro: {ex} ");
            }
        }
        [HttpDelete]
        public ActionResult Delete(int id, int id_user)
        {
            try
            {
                new UserRepository().Delete(id, id_user);
                return Ok("Usuário deletado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro: {ex} ");
            }
        }
    }
}