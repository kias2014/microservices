﻿using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrderList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrdersVm>().ReverseMap();
            CreateMap<Order, ChackoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
