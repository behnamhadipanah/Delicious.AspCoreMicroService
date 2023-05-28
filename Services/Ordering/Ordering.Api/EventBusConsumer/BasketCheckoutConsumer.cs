using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.Api.EventBusConsumer;

public class BasketCheckoutConsumer:IConsumer<BasketCheckOutEvent>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILogger<BasketCheckoutConsumer> _logger;
    public BasketCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<BasketCheckOutEvent> context)
    {

        var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
        var result=await _mediator.Send(command);
        _logger.LogInformation($"Order consumed successfully and order id is :{result}");

    }
}