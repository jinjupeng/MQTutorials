using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace AspNetCore.RabbitMQ
{
    /// <summary>
    /// https://blog.csdn.net/dz1822802785/article/details/105426636
    /// https://www.codeplus.vip/Net_Core%E9%9B%86%E6%88%90RabbitMQ%E8%AE%A2%E9%98%85%E4%B8%8E%E5%8F%91%E9%80%81.html
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
