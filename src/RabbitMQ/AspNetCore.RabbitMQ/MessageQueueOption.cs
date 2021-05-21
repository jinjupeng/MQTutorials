using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.RabbitMQ
{
    /// <summary>
    /// 约定 强类型配置
    /// </summary>
    public class MessageQueueOption
    {
        /// <summary>
        /// rabbit 连接配置
        /// </summary>
        /// <value>
        /// The rabbit connect option.
        /// </value>
        public RabbitConnectOption RabbitConnect { get; set; }

        /// <summary>
        /// 批量推送消息到云信的队列配置信息
        /// </summary>
        /// <value>
        /// 
        /// </value>
        public QueueBaseOption QueueBase { get; set; }

    }
}
