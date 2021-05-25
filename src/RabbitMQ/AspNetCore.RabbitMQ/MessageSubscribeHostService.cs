using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.RabbitMQ
{
    /// <summary>
    /// 后台托管服务
    /// </summary>
    public class MessageSubscribeHostService : RabbitListenerHostService
    {
        /// <summary>
        /// The services
        /// </summary>
        private readonly IServiceProvider _services;
        /// <summary>
        /// The batch advance option
        /// </summary>
        private readonly QueueBaseOption _queueBaseOption;
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageSubscribeHostService" /> class.
        /// </summary>
        public MessageSubscribeHostService(IServiceProvider services, IOptions<MessageQueueOption> messageQueueOption,
            ILogger<MessageSubscribeHostService> logger) : base(messageQueueOption, logger)
        {
            _services = services;
            _queueBaseOption = messageQueueOption.Value?.QueueBase;
        }
        /// <summary>
        /// Processes this instance.
        /// </summary>
        protected override void Process()
        {
            _logger.LogInformation("调用ExecuteAsync");
            using (var scope = _services.CreateScope())
            {
                try
                {
                    //我们在消费端 从新进行一次 队列和交换机的绑定 ，防止 因为消费端在生产端 之前运行的 问题。
                    _channel.ExchangeDeclare(_queueBaseOption.Exchange, _queueBaseOption.ExchangeType, true);
                    _channel.QueueDeclare(_queueBaseOption.ClientQueue, true, false, false, null);
                    _channel.QueueBind(_queueBaseOption.ClientQueue, _queueBaseOption.Exchange, "face.log", null);
                    _logger.LogInformation("开始监听队列：" + _queueBaseOption.ClientQueue); // 监听客户端列队
                    _channel.BasicQos(0, 1, false);//设置一个消费者在同一时间只处理一个消息，这个rabbitmq 就会将消息公平分发
                    var consumer = new EventingBasicConsumer(_channel);
                    consumer.Received += (ch, ea) =>
                    {
                        try
                        {
                            var content = Encoding.UTF8.GetString(ea.Body);
                            _logger.LogInformation($"{_queueBaseOption.ClientQueue}[face.log]获取到消息：{content}");
                            // TODO: 对接业务代码
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "");
                        }
                        finally
                        {
                            _channel.BasicAck(ea.DeliveryTag, false);
                        }
                    };

                    _channel.BasicConsume(_queueBaseOption.ClientQueue, false, consumer);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "");
                }
            }
        }
    }
}
