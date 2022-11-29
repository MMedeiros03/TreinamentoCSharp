namespace TreinamentoMarinho.Entities
{
    public class UserEntity : BaseEntity
    { 
        public string? St_name { get; set; }

        public int Cd_usuario { get; set; }

        public string? St_email { get; set; }

        public string? St_login { get; set; }

        public string? St_password { get; set; }

        public string? St_role { get; set; }
    }

    public class UserEntityValidation
    {
        public string? St_login { get; set; }
        public string? St_password { get; set; }
    }
}
