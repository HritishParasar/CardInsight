using CardInsight.API.Models;

namespace CardInsight.API.Helpers
{
    public interface IJwtHelper
    {
        string GenerateToken(User user);
    }
}
