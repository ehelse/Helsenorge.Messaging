﻿using Helsenorge.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Helsenorge.Messaging.IntegrationTests.ServiceBus
{
    public class ServiceBusReceiverTests : IDisposable
    {
        public string QueueName => nameof(ServiceBusReceiverTests);

        private readonly ServiceBusFixture _fixture;
        private readonly ServiceBusReceiver _receiver;

        public ServiceBusReceiverTests(ITestOutputHelper output)
        {
            _fixture = new ServiceBusFixture(output);
            _receiver = _fixture.CreateReceiver(QueueName);
            _fixture.PurgeQueueAsync(QueueName).Wait();
        }

        public void Dispose()
        {
            _receiver.Close();
            _fixture.Dispose();
        }

        [Fact]
        public async Task Should_Not_Recreate_Receiver_If_No_Underlying_Object_Is_Closed()
        {
            Assert.False(_receiver.IsClosed);
            await ReceiveTestMessageAsync();
            Assert.False(_receiver.IsClosed);
        }

        [Fact]
        public async Task Should_Recreate_Link_When_Underlying_Connection_Is_Closed()
        {
            Assert.False(_receiver.IsClosed);
            await _fixture.Connection.Connection.CloseAsync();
            await ReceiveTestMessageAsync();
            Assert.False(_receiver.IsClosed);
        }

        [Fact]
        public async Task Should_Not_Allow_To_Receive_Message_When_Connection_Is_Closed()
        {
            var connection = _fixture.Connection;
            Assert.False(_receiver.IsClosed);
            await connection.CloseAsync();
            await Assert.ThrowsAsync<ObjectDisposedException>(async () => await _receiver.ReceiveAsync(TimeSpan.Zero));
        }

        [Fact]
        public async Task Should_Not_Allow_To_Receive_Message_When_Receiver_Is_Closed()
        {
            Assert.False(_receiver.IsClosed);
            _receiver.Close();
            Assert.True(_receiver.IsClosed);
            await Assert.ThrowsAsync<ObjectDisposedException>(async () => await _receiver.ReceiveAsync(TimeSpan.Zero));
        }

        private async Task ReceiveTestMessageAsync()
        {
            var messageText = await _fixture.SendTestMessageAsync(QueueName);
            var message = await _receiver.ReceiveAsync(ServiceBusTestingConstants.DefaultReadTimeout);
            Assert.NotNull(message);
            await message.CompleteAsync();
            Assert.Equal(messageText, await message.GetBodyAsStingAsync());
        }
    }
}
