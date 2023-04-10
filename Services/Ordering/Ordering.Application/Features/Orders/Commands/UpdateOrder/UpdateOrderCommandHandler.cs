using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOrderCommand> _logger;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommand> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }


    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderForUpdate = await _orderRepository.GetByIdAsync(request.Id);
        if (orderForUpdate == null)
        {
            _logger.LogError("Order is not Exists");
            throw new NotFoundException(nameof(Order), request.Id);
        }

        _mapper.Map(request, orderForUpdate, typeof(UpdateOrderCommand), typeof(Order));
        await _orderRepository.UpdateAsync(orderForUpdate);
        _logger.LogInformation($"Order {orderForUpdate.Id } is successfully updated");
    }
}