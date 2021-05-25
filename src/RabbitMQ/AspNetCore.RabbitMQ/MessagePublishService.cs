using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace AspNetCore.RabbitMQ
{
    /// <summary>
    /// 消息发布服务
    /// </summary>
    public class MessagePublishService : ITransientDependency
    {
        /// <summary>
        /// The channel
        /// </summary>
        protected IModel _channel;
        /// <summary>
        /// The batch advance option
        /// </summary>
        private readonly QueueBaseOption _queueBaseOption;

        public MessagePublishService(IOptions<MessageQueueOption> messageQueueOption)
        {
            _queueBaseOption = messageQueueOption.Value?.QueueBase;
            _channel = messageQueueOption.Value?.RabbitConnect.Channel;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public void SendMsg<T>(T msg, string routeKey)
        {
            _channel.ExchangeDeclare(_queueBaseOption.Exchange, _queueBaseOption.ExchangeType, true);
            _channel.QueueDeclare(_queueBaseOption.Queue, true, false, false, null);
            _channel.QueueBind(_queueBaseOption.Queue, _queueBaseOption.Exchange, routeKey, null);
            var basicProperties = _channel.CreateBasicProperties();
            //1：非持久化 2：可持久化
            basicProperties.DeliveryMode = 2;
            var json = JsonConvert.SerializeObject(msg);
            var payload = Encoding.UTF8.GetBytes(json);
            var address = new PublicationAddress(ExchangeType.Direct, _queueBaseOption.Exchange, routeKey);
            _channel.BasicPublish(address, basicProperties, payload);
        }
    }
}
