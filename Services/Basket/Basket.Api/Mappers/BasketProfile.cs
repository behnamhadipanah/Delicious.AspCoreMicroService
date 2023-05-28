using AutoMapper;
using Basket.Api.Entities;
using EventBus.Messages.Events;

namespace Basket.Api.Mappers;

public class BasketProfile:Profile
{
    public BasketProfile()
    {
        CreateMap<BasketCheckOut, BasketCheckOutEvent>().ReverseMap();
    }
}