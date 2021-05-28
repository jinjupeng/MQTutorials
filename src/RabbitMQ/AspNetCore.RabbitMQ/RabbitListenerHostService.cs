using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCore.RabbitMQ
{
    /// <summary>
    /// RabbitMQ 监听服务
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Hosting.IHostedService" />
    public abstract class RabbitListenerHostService : IHostedService
    {
        /// <summary>
        /// 
        /// </summary>
        protected IConnection _connection;
        /// <summary>
        /// The channel
        /// </summary>
        protected IModel _channel;
        /// <summary>
        /// The rabbit connect options
        /// </summary>
        private readonly RabbitConnectOption _rabbitConnectOptions;
        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitListenerHostService" /> class.
        /// </summary>
        /// <param name="messageQueueOption"></param>
        /// <param name="logger">The logger.</param>
        public RabbitListenerHostService(IOptions<MessageQueueOption> messageQueueOption, ILogger logger)
        {
            _rabbitConnectOptions = messageQueueOption.Value?.RabbitConnect;
            _logger = logger;
        }
        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (_rabbitConnectOptions == null) return Task.CompletedTask;
                var factory = new ConnectionFactory()
                {
                    HostName = _rabbitConnectOptions.HostName,
                    Port = _rabbitConnectOptions.Port,
                    UserName = _rabbitConnectOptions.UserName,
                    Password = _rabbitConnectOptions.Password,
                };
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _rabbitConnectOptions.Connection = _connection;
                _rabbitConnectOptions.Channel = _channel;
                Process();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rabbit连接出现异常");
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_connection != null)
                this._connection.Close();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        protected abstract void Process();

        //protected abstract Task ExecuteAsync(CancellationToken stoppingToken);
    }
}
