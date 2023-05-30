using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandHandler:IRequestHandler<CheckoutOrderCommand,int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly  IEmailService _emailService;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _emailService = emailService;
        _logger = logger;
    }
    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = _mapper.Map<Order>(request);
        var order=await _orderRepository.AddAsync(orderEntity);
        _logger.LogInformation($"Order {order.Id} is successfully created");
       // await SendMail(order);
        return order.Id;
    }

    private async Task SendMail(Order order)
    {
        try
        {
            //Send Email
            await _emailService.SendEmail(new Email()
            {
                To = "behnamhadipanah@gmail.com",
                Subject = "new order has created",
                Body = "this is body of email"
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Email has not hass send");
        }
    }
}