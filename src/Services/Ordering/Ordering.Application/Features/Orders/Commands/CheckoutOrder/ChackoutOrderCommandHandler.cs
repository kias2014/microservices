using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class ChackoutOrderCommandHandler : IRequestHandler<ChackoutOrderCommand,int>
    {
        private readonly IOrderRepository _orderRepositoy;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<ChackoutOrderCommandHandler> _logger;

        public ChackoutOrderCommandHandler(IOrderRepository orderRepositoy, IMapper mapper, IEmailService emailService, ILogger<ChackoutOrderCommandHandler> logger)
        {
            _orderRepositoy = orderRepositoy ?? throw new ArgumentNullException(nameof(orderRepositoy));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(ChackoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var newOrder = await _orderRepositoy.AddAsync(orderEntity);

            _logger.LogInformation($"Order {newOrder.Id} is successfully created.");

            await SendMail(newOrder);

            return newOrder.Id;
        }

        private async Task SendMail(Order order)
        {
            var email = new Email() { To = "ezozkme@gmail.com", Body = $"Order was created.", Subject = "Order was created" };

            try
            {
                await _emailService.SenEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
