using AspNetCore.RabbitMQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMQController : ControllerBase
    {
        public readonly MessagePublishService _publishService;

        public RabbitMQController(MessagePublishService publishService)
        {
            _publishService = publishService;
        }

        [HttpGet]
        [Route("sendMsg")]
        [AllowAnonymous]
        public async Task<IActionResult> SendMsg()
        {
            _publishService.SendMsg("测试", "test");
            return Ok(await Task.FromResult("消息已发送！"));
        }

    }
}
