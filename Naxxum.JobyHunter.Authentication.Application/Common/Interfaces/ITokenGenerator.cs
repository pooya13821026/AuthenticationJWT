namespace Authentication.Application.Common.Interfaces
{
    public interface ITokenGenerator
    {
        public string GenerateJwtoken((string userId, string userName) userDetails);
    }
}