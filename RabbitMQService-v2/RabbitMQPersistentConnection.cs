using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;

namespace RabbitMQService_v2
{
    public class RabbitMQPersistentConnection : IDisposable
    {
        //fields
        private readonly IConnectionFactory connectionFactory;
        private readonly int retryCount;
        public IConnection connection;
        private object lock_object = new object();
        private bool _disposed;

        //constructor
        public RabbitMQPersistentConnection(IConnectionFactory connectionFactory, int retryCount = 5)
        {
            this.connectionFactory = connectionFactory;

        }
        public bool IsConnected => connection != null && connection.IsOpen;

        public IModel CreateModel()
        {
            return connection.CreateModel();
        }

        public void Dispose()
        {
            _disposed = true;
            connection.Dispose();
        }

        public bool TryConnect()
        {
            lock (lock_object)
            {
                var policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {

                    }
                    );
                policy.Execute(() =>
                {
                    connection = connectionFactory.CreateConnection();
                });

                if (IsConnected)
                {
                    connection.ConnectionShutdown += Connection_ConnectionShutdown;
                    connection.CallbackException += Connection_CallBackException;
                    connection.ConnectionBlocked += Connection_ConnectionBlocked;
                    return true;
                }
                return false;
            }
        }

        private void Connection_ConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
        {
            if (!_disposed) return;

            TryConnect();
        }
        private void Connection_CallBackException(object? sender, CallbackExceptionEventArgs e)
        {
            if (!_disposed) return;

            TryConnect();
        }
        private void Connection_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            if (!_disposed) return;

            TryConnect();
        }
    }

}
