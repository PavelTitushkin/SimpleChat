using SimpleChat.SubscribeTableDependencies.Contracts;

namespace SimpleChat.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSqlTableDependency<T>(this IApplicationBuilder applicationBuilder, string connectionString)
            where T : ISubscribeTableDependency
        {
            var serviceProvider = applicationBuilder.ApplicationServices;
            var service = serviceProvider.GetService<T>();
            service?.SubscribeTable(connectionString);
        }
    }
}
