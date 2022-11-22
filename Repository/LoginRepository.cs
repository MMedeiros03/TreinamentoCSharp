using System.Data;
using TreinamentoMarinho.Entities;

namespace TreinamentoMarinho.Repository
{
    public class LoginRepository:BaseRepository
    {
        public bool Login(UserEntityValidation entity)
        {
            try
            {
                string query = $@"SELECT * FROM USERS WHERE ST_LOGIN = '{entity.St_login}'
                                AND ST_PASSWORD = '{entity.St_password}' AND DT_DELETE IS NULL";

                DataTable result = ExecQuery(query);
                UserEntityValidation user = new UserEntityValidation();
                foreach (DataRow row in result.Rows)
                {
                    user = new UserEntityValidation()
                    {
                        St_login = (string)row["ST_LOGIN"],
                        St_password = (string)row["ST_PASSWORD"]
                    };
                }
                if (result.Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
