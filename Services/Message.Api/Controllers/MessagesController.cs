using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace Message.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateMessage()
        {
        
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost", 
                Port = 5672,            
                UserName = "guest",     
                Password = "guest"     
            };

            
            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                
                channel.QueueDeclare(
                    queue: "Kuyruk1",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                
                var messageContent = "Merhaba bu bir RabbitMQ kuyruk mesajıdır";
                var byteMessageContent = Encoding.UTF8.GetBytes(messageContent);

               
                channel.BasicPublish(
                    exchange: "",               
                    routingKey: "Kuyruk1",      
                    basicProperties: null,      
                    body: byteMessageContent    
                );
            }

           
            return Ok("Mesajınız alınmıştır.");
        }
    }
}
