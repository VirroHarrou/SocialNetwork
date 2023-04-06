using SocialNetwork.Domain.Context;

namespace SocialNetwork.Core.Common
{
    public class DbInitializer
    {
        public static void Initialize(SocialNetworkContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
