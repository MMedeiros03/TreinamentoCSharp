using System.Data;
using TreinamentoMarinho.Entities;
using TreinamentoMarinho.Utils;

namespace TreinamentoMarinho.Repository
{
    public class LoginRepository:BaseRepository
    {
        public UserEntity Login(UserEntityValidation entity)
        {
            try
            {
                string hashPass = new Utilities().GenerateHash(entity.St_login, entity.St_password);

                string query = $@"SELECT * FROM USERS WHERE ST_LOGIN = '{entity.St_login}'
                                AND ST_PASSWORD = '{hashPass}' AND DT_DELETE IS NULL";

                UserEntity user = new UserEntity();
                DataTable result = ExecQuery(query);

                if (result.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    foreach (DataRow row in result.Rows)
                    {
                        user = new UserEntity()
                        {
                            Cd_usuario = Convert.ToInt32(row["CD_USUARIO"]),
                            St_role = Convert.ToString(row["ST_ROLE"]),
                            St_name = Convert.ToString(row["ST_NAME"]),
                            St_email = Convert.ToString(row["ST_EMAIL"]),
                            St_login = Convert.ToString(row["ST_LOGIN"]),
                            St_password = Convert.ToString(row["ST_PASSWORD"])
                        };
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
