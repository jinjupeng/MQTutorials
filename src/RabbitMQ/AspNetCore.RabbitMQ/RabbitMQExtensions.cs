using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCore.RabbitMQ
{
    /// <summary>
    /// https://blog.csdn.net/dz1822802785/article/details/105426636
    /// https://www.codeplus.vip/Net_Core%E9%9B%86%E6%88%90RabbitMQ%E8%AE%A2%E9%98%85%E4%B8%8E%E5%8F%91%E9%80%81.html
    /// https://www.programmersought.com/article/59116121832/
    /// https://www.cnblogs.com/shanfeng1000/p/12133181.html
    /// https://www.cnblogs.com/sang-bit/p/14790482.html
    /// </summary>
    public static class RabbitMQExtensions
    {
        /// <summary>
        /// Adds the register queue sub.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="option">The option.</param>
        /// <returns></returns>
        public static IServiceCollection AddMessageQueueOption(this IServiceCollection services, Action<MessageQueueOption> option)
        {
            services.Configure(option);
            return services;
        }

        /// <summary>
        /// Adds the register queue sub.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The option.</param>
        /// <returns></returns>
        public static IServiceCollection AddMessageQueueOption(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new MessageQueueOption();
            configuration.GetSection("MessageQueue").Bind(config);
            services.AddTransient<MessagePublishService>();
            return services;
        }


        /// <summary>
        /// 服务自注册，实现自管理
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddMessageSubscribeHostService(this IServiceCollection services)
        {
            services.AddHostedService<MessageSubscribeHostService>();
            return services;
        }
    }
}
