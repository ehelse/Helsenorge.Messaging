using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using Helsenorge.Messaging.Abstractions;
using Helsenorge.Messaging.ServiceBus.Senders;
using Helsenorge.Registries.Abstractions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Helsenorge.Messaging.Tests, PublicKey=00240000048" + 
                              "0000094000000060200000024000052534131000400000100" +
                              "0100773d7b513076df908a05edadb53a26effc169310d844f" +
                              "f47ed842bebef258bcfda39e2c62e4d6ceabb04037a24bcd2" +
                              "85efd882ee7623170664411d5f5107cd2756221ba572b4903" +
                              "bd45153c192cae7c544ea23b88f814c49a4709218355eac93" +
                              "98524c353f4a5678ea9a2755bb012707d2de25019af5fd511" +
                              "b1f48aa5a6eadd4")]
[assembly: InternalsVisibleTo("Helsenorge.Messaging.IntegrationTests, PublicKey=00240000048" +
                              "0000094000000060200000024000052534131000400000100" +
                              "0100773d7b513076df908a05edadb53a26effc169310d844f" +
                              "f47ed842bebef258bcfda39e2c62e4d6ceabb04037a24bcd2" +
                              "85efd882ee7623170664411d5f5107cd2756221ba572b4903" +
                              "bd45153c192cae7c544ea23b88f814c49a4709218355eac93" +
                              "98524c353f4a5678ea9a2755bb012707d2de25019af5fd511" +
                              "b1f48aa5a6eadd4")]
namespace Helsenorge.Messaging
{
    /// <summary>
    /// Default implementation of <see cref="IMessagingClient"/>. This must act as a singleton, otherwise syncronous messaging will not work
    /// </summary>
    public sealed class MessagingClient : MessagingCore, IMessagingClient
    {
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly AsynchronousSender _asynchronousServiceBusSender;
        private readonly SynchronousSender _synchronousServiceBusSender;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Set of options to use</param>
        /// <param name="collaborationProtocolRegistry">Reference to the collaboration protocol registry</param>
        /// <param name="addressRegistry">Reference to the address registry</param>
        [Obsolete("This constructor is replaced by ctor(MessagingSettings, ILoggerFactory, ICollaborationProtocolRegistry, IAddressRegistry) and will be removed in a future version")]
        public MessagingClient(
            MessagingSettings settings,
            ICollaborationProtocolRegistry collaborationProtocolRegistry,
            IAddressRegistry addressRegistry) : base(settings, collaborationProtocolRegistry, addressRegistry)
        {
            _asynchronousServiceBusSender = new AsynchronousSender(ServiceBus);
            _synchronousServiceBusSender = new SynchronousSender(ServiceBus);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Set of options to use</param>
        /// <param name="collaborationProtocolRegistry">Reference to the collaboration protocol registry</param>
        /// <param name="addressRegistry">Reference to the address registry</param>
        /// <param name="certificateStore">Reference to an implementation of <see cref="ICertificateStore"/>.</param>
        [Obsolete("This constructor is replaced by ctor(MessagingSettings, ILoggerFactory, ICollaborationProtocolRegistry, IAddressRegistry, ICertificateStore) and will be removed in a future version")]
        public MessagingClient(
            MessagingSettings settings,
            ICollaborationProtocolRegistry collaborationProtocolRegistry,
            IAddressRegistry addressRegistry,
            ICertificateStore certificateStore) : base(settings, collaborationProtocolRegistry, addressRegistry, certificateStore)
        {
            _asynchronousServiceBusSender = new AsynchronousSender(ServiceBus);
            _synchronousServiceBusSender = new SynchronousSender(ServiceBus);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Set of options to use</param>
        /// <param name="collaborationProtocolRegistry">Reference to the collaboration protocol registry</param>
        /// <param name="addressRegistry">Reference to the address registry</param>
        /// <param name="certificateStore">Reference to an implementation of <see cref="ICertificateStore"/>.</param>
        /// <param name="certificateValidator">Reference to an implementation of <see cref="ICertificateValidator"/>.</param>
        /// <param name="messageProtection">Reference to an implementation of <see cref="IMessageProtection"/>.</param>
        [Obsolete("This constructor is replaced by ctor(MessagingSettings, ILoggerFactory, ICollaborationProtocolRegistry, IAddressRegistry, ICertificateStore, ICertificateValidator, IMessageProtection) and will be removed in a future version")]
        public MessagingClient(
            MessagingSettings settings,
            ICollaborationProtocolRegistry collaborationProtocolRegistry,
            IAddressRegistry addressRegistry,
            ICertificateStore certificateStore,
            ICertificateValidator certificateValidator,
            IMessageProtection messageProtection) : base(settings, collaborationProtocolRegistry, addressRegistry, certificateStore, certificateValidator, messageProtection)
        {
            _asynchronousServiceBusSender = new AsynchronousSender(ServiceBus);
            _synchronousServiceBusSender = new SynchronousSender(ServiceBus);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Set of options to use</param>
        /// <param name="loggerFactory"></param>
        /// <param name="collaborationProtocolRegistry">Reference to the collaboration protocol registry</param>
        /// <param name="addressRegistry">Reference to the address registry</param>
        public MessagingClient(
            MessagingSettings settings,
            ILoggerFactory loggerFactory,
            ICollaborationProtocolRegistry collaborationProtocolRegistry,
            IAddressRegistry addressRegistry) : base(settings, collaborationProtocolRegistry, addressRegistry)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _logger = _loggerFactory.CreateLogger(nameof(MessagingClient));
            _asynchronousServiceBusSender = new AsynchronousSender(ServiceBus);
            _synchronousServiceBusSender = new SynchronousSender(ServiceBus);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Set of options to use</param>
        /// <param name="loggerFactory"></param>
        /// <param name="collaborationProtocolRegistry">Reference to the collaboration protocol registry</param>
        /// <param name="addressRegistry">Reference to the address registry</param>
        /// <param name="certificateStore">Reference to an implementation of <see cref="ICertificateStore"/>.</param>
        public MessagingClient(
            MessagingSettings settings,
            ILoggerFactory loggerFactory,
            ICollaborationProtocolRegistry collaborationProtocolRegistry,
            IAddressRegistry addressRegistry,
            ICertificateStore certificateStore) : base(settings, collaborationProtocolRegistry, addressRegistry, certificateStore)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _logger = _loggerFactory.CreateLogger(nameof(MessagingClient));
            _asynchronousServiceBusSender = new AsynchronousSender(ServiceBus);
            _synchronousServiceBusSender = new SynchronousSender(ServiceBus);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Set of options to use</param>
        /// <param name="loggerFactory"></param>
        /// <param name="collaborationProtocolRegistry">Reference to the collaboration protocol registry</param>
        /// <param name="addressRegistry">Reference to the address registry</param>
        /// <param name="certificateStore">Reference to an implementation of <see cref="ICertificateStore"/>.</param>
        /// <param name="certificateValidator">Reference to an implementation of <see cref="ICertificateValidator"/>.</param>
        /// <param name="messageProtection">Reference to an implementation of <see cref="IMessageProtection"/>.</param>
        public MessagingClient(
            MessagingSettings settings,
            ILoggerFactory loggerFactory,
            ICollaborationProtocolRegistry collaborationProtocolRegistry,
            IAddressRegistry addressRegistry,
            ICertificateStore certificateStore,
            ICertificateValidator certificateValidator,
            IMessageProtection messageProtection) : base(settings, collaborationProtocolRegistry, addressRegistry, certificateStore, certificateValidator, messageProtection)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _logger = _loggerFactory.CreateLogger(nameof(MessagingClient));
            _asynchronousServiceBusSender = new AsynchronousSender(ServiceBus);
            _synchronousServiceBusSender = new SynchronousSender(ServiceBus);
        }

        /// <summary>
        /// Sends a message and allows the calling code to continue its work
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message">Information about the message being sent</param>
        /// <returns></returns>
        [Obsolete("This method is replaced by SendAndContinueAsync(OutgoingMessage) and will be removed in a future version")]
        public async Task SendAndContinueAsync(ILogger logger, OutgoingMessage message)
        {
            var collaborationProtocolMessage = await PreCheck(logger, message).ConfigureAwait(false);

            switch (collaborationProtocolMessage.DeliveryProtocol)
            {
                case DeliveryProtocol.Amqp:
                    await _asynchronousServiceBusSender.SendAsync(logger, message).ConfigureAwait(false);
                    return;
                case DeliveryProtocol.Unknown:
                default:
                    throw new MessagingException("Invalid delivery protocol: " + message.MessageFunction)
                    {
                        EventId = EventIds.InvalidMessageFunction
                    };
            }
        }

        /// <summary>
        /// Sends a message and blocks the calling code until we have an answer
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <returns>The received XML</returns>
        [Obsolete("This method is replaced by SendAndWaitAsync(OutgoingMessage) and will be removed in a future version")]
        public async Task<XDocument> SendAndWaitAsync(ILogger logger, OutgoingMessage message)
        {
            var collaborationProtocolMessage = await PreCheck(logger, message).ConfigureAwait(false);
           
            switch (collaborationProtocolMessage.DeliveryProtocol)
            {
                case DeliveryProtocol.Amqp:
                    return await _synchronousServiceBusSender.SendAsync(logger, message).ConfigureAwait(false);
                case DeliveryProtocol.Unknown:
                default:
                    throw new MessagingException("Invalid delivery protocol: " + message.MessageFunction)
                    {
                        EventId = EventIds.InvalidMessageFunction
                    };
            }
        }

        /// <summary>
        /// Sends a message and allows the calling code to continue its work
        /// </summary>
        /// <param name="message">Information about the message being sent</param>
        /// <returns></returns>
        public async Task SendAndContinueAsync(OutgoingMessage message)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            await SendAndContinueAsync(_logger, message);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Sends a message and blocks the calling code until we have an answer
        /// </summary>
        /// <param name="message"></param>
        /// <returns>The received XML</returns>
        public async Task<XDocument> SendAndWaitAsync(OutgoingMessage message)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return await SendAndWaitAsync(_logger, message);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Registers a delegate that should be called when we receive a synchronous reply message. This is where the main reply message processing is done.
        /// </summary>
        /// <param name="action">The delegate that should be called</param>
        public void RegisterSynchronousReplyMessageReceivedCallback(Action<IncomingMessage> action) => _synchronousServiceBusSender.OnSynchronousReplyMessageReceived = action;

        private async Task<CollaborationProtocolMessage> PreCheck(ILogger logger, OutgoingMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrEmpty(message.MessageFunction)) throw new ArgumentNullException(nameof(message.MessageFunction));
            if (message.ToHerId <= 0) throw new ArgumentOutOfRangeException(nameof(message.ToHerId));

            var messageFunction = string.IsNullOrEmpty(message.ReceiptForMessageFunction)
                ? message.MessageFunction
                : message.ReceiptForMessageFunction;

            var profile = await FindProfile(logger, message).ConfigureAwait(false);
            var collaborationProtocolMessage = profile?.FindMessageForReceiver(messageFunction);

            if ((profile != null && profile.Name == Registries.CollaborationProtocolRegistry.DummyPartyName)
                || (collaborationProtocolMessage == null && messageFunction.ToUpper().Contains("DIALOG_INNBYGGER_TIMERESERVASJON")))
            {
                collaborationProtocolMessage = new CollaborationProtocolMessage
                {
                    Name = messageFunction,
                    DeliveryProtocol = DeliveryProtocol.Amqp,
                    Parts = new List<CollaborationProtocolMessagePart>
                    {
                        new CollaborationProtocolMessagePart
                        {
                            MaxOccurrence = 1,
                            MinOccurrence = 0,
                            XmlNamespace = "http://www.kith.no/xmlstds/msghead/2006-05-24",
                            XmlSchema = "MsgHead-v1_2.xsd"
                        },
                        new CollaborationProtocolMessagePart
                        {
                            MaxOccurrence = 1,
                            MinOccurrence = 0,
                            XmlNamespace = "http://www.kith.no/xmlstds/dialog/2013-01-23",
                            XmlSchema = "dialogmelding-1.1.xsd"
                        }
                    }
                };
            }
            else if (collaborationProtocolMessage == null)
            {
                throw new MessagingException("Invalid delivery protocol: " + message.MessageFunction)
                {
                    EventId = EventIds.InvalidMessageFunction
                };
            }

            return collaborationProtocolMessage;
        }

        private async Task<CollaborationProtocolProfile> FindProfile(ILogger logger, OutgoingMessage message)
        {
            var profile = 
                await CollaborationProtocolRegistry.FindAgreementForCounterpartyAsync(logger, message.ToHerId).ConfigureAwait(false) ??
                await CollaborationProtocolRegistry.FindProtocolForCounterpartyAsync(logger, message.ToHerId).ConfigureAwait(false);
            return profile;
        }

    }
}
