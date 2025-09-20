namespace G_Task.Application.DTOs.Account
{
    public class GetLoginDto
    {
        private string _userName;
        private string _password;
        public string UserName
        {
            get => _userName.Trim();
            set => _userName = value;

        }

        public string Password
        {
            get => _password.Trim();
            set => _password = value;
        }

    }
}
