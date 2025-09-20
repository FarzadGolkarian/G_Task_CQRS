
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace G_Task.Common.Helpers

{
    public static class SerilogExtension
    {
        /// <summary>
        /// کانفیگ serilog
        /// </summary>
        /// <param name="builder"></param>
        public static void UseLogger(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(services);
            });
        }

    }
}
