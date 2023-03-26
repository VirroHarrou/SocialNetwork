using Messanger_API.Core.Context;

namespace SocialNetwork.Core
{
    public class DbInitializer
    {
        public static void Initialize(SocialNetworkContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
