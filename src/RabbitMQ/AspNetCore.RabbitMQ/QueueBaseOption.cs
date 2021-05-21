using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.RabbitMQ
{
    /// <summary>
    /// 队列配置基类
    /// </summary>
    public class QueueBaseOption
    {
        /// <summary>
        /// 交换机名称
        /// </summary>
        /// <value>
        /// The exchange.
        /// </value>
        public string Exchange { get; set; }
        /// <summary>
        /// 队列名称
        /// </summary>
        /// <value>
        /// The queue.
        /// </value>
        public string Queue { get; set; }
        /// <summary>
        /// 交换机类型 direct、fanout、headers、topic 必须小写
        /// </summary>
        /// <value>
        /// The type of the exchange.
        /// </value>
        public string ExchangeType { get; set; }
        /// <summary>
        /// 路由
        /// </summary>
        /// <value>
        /// The route key.
        /// </value>
        //public string RouteKey { get; set; }

        /// <summary>
        /// 客户端队列名称
        /// </summary>
        /// <value>
        /// The client queue
        public string ClientQueue { get; set; }
    }
}
