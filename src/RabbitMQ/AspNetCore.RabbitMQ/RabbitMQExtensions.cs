using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.RabbitMQ
{
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
