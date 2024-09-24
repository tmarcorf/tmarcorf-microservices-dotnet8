using GeekShopping.MessageBus;
using GeekShopping.PaymentAPI.Messages;
using Microsoft.AspNetCore.Components;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShopping.PaymentAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        private const string _exchangeName = "DirectPaymentUpdateExchange";
        private const string _PaymentEmailUpdateQueueName = "PaymentEmailUpdateQueueName";
        private const string _PaymentOrderUpdateQueueName = "PaymentOrderUpdateQueueName";

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }

        public void SendMessage(BaseMessage message)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct, durable: false);

                channel.QueueDeclare(_PaymentEmailUpdateQueueName, false, false, false, null);
                channel.QueueDeclare(_PaymentOrderUpdateQueueName, false, false, false, null);

                channel.QueueBind(_PaymentEmailUpdateQueueName, _exchangeName, "PaymentEmail");
                channel.QueueBind(_PaymentOrderUpdateQueueName, _exchangeName, "PaymentOrder");

                byte[] body = GetMessageAsByteArray(message);

                channel.BasicPublish(exchange: _exchangeName, "PaymentEmail", basicProperties: null, body: body);
                channel.BasicPublish(exchange: _exchangeName, "PaymentOrder", basicProperties: null, body: body);

                throw new NotImplementedException();
            }
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize<UpdatePaymentResultMessage>((UpdatePaymentResultMessage)message, options);

            return Encoding.UTF8.GetBytes(json);
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                // log exception
                throw;
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null) return true;

            CreateConnection();

            return _connection != null;
        }
    }
}
