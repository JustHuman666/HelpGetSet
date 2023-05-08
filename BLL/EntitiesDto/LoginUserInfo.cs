namespace BLL.EntitiesDto
{
    /// <summary>
    /// Class that represents anformation about logged user
    /// </summary>
    public class LoginUserInfo
    {
        public string Token { get; }
        public int UserId { get; }

        public LoginUserInfo(string token, int id)
        {
            Token = token;
            UserId = id;
        }
    }
}
