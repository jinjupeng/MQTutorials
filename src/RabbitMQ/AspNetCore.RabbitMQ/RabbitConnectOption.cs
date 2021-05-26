using RabbitMQ.Client;

namespace AspNetCore.RabbitMQ
{
    /// <summary>
    /// Rabbit 强类型配置
    /// </summary>
    public class RabbitConnectOption
    {
        /// <summary>
        /// Gets or sets the name of the host.
        /// </summary>
        /// <value>
        /// The name of the host.
        /// </value>
        public string HostName { get; set; }
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the virtual host.
        /// </summary>
        /// <value>
        /// The virtual host.
        /// </value>
        public string VirtualHost { get; set; }
        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public IConnection Connection { get; set; }
        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public IModel Channel { get; set; }
    }
}
