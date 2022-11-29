using Microsoft.AspNetCore.Identity;
using System.Data;
using TreinamentoMarinho.Entities;
using TreinamentoMarinho.Utils;
namespace TreinamentoMarinho.Repository
{
	public class UserRepository : BaseRepository
	{

        public List<UserEntity> GetAll()
        {
            try
            {
                string query = $@"SELECT * FROM USERS WHERE DT_DELETE IS NULL";

                DataTable result = ExecQuery(query);

				List<UserEntity> users = new List<UserEntity>();

                foreach (DataRow row in result.Rows)
                {
                    users.Add(new UserEntity()
                    {
                        Cd_usuario = Convert.ToInt32(row["CD_USUARIO"]),
                        St_role = Convert.ToString(row["ST_ROLE"]),
                        St_name = Convert.ToString(row["ST_NAME"]),
                        St_email = Convert.ToString(row["ST_EMAIL"]),
                        St_login = Convert.ToString(row["ST_LOGIN"]),
                        St_password = Convert.ToString(row["ST_PASSWORD"])
                    });
                }
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserEntity GetById(int id)
		{
			try
			{
				string query = $@"SELECT * FROM USERS WHERE CD_USUARIO = {id} AND DT_DELETE IS NULL";
				UserEntity user = new UserEntity();
                DataTable result = ExecQuery(query);
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
                if (result.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    return user;
                }
                
            }
			catch (Exception ex)
			{
				throw ex;
			}
		}
        public void Save(UserEntity entity)
		{
			try
			{
				var passHash = new Utilities().GenerateHash(entity.St_login, entity.St_password);

				string query = $@"INSERT INTO USERS VALUES (
									'{entity.St_name}',
									'{entity.Cd_usuario}',
									'{entity.St_role}',
									'{entity.St_email}',
									'{entity.St_login}',
									'{passHash}',
									null,
									null)";
				ExecCommand(query);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void Update(UserEntity entity)
		{
			try
			{
                var passHash = new Utilities().GenerateHash(entity.St_login, entity.St_password);
                string query = $@"UPDATE USERS SET 
							ST_NAME = '{entity.St_name}',
							ST_ROLE = '{entity.St_role}',
							ST_EMAIL = '{entity.St_email}',
							ST_LOGIN = '{entity.St_login}',
							ST_PASSWORD = '{passHash}'
								where CD_USUARIO = '{entity.Cd_user}'";
                ExecCommand(query);
            }
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public void Delete(int id, int cd_user)
		{
			 try
            {
                string query = $@"UPDATE USERS SET 
								CD_USER_DELETE = '{cd_user}',
								DT_DELETE = GETDATE()
							where CD_USUARIO = '{id}'";
                ExecCommand(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
		}
	}
}
