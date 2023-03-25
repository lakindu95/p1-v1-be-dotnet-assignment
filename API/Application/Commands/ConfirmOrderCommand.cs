using System;
using Domain.Aggregates.OrderAggregate;
using MediatR;

namespace API.Application.Commands
{
    public class ConfirmOrderCommand : IRequest<Order>
    {
        public Guid Id { get; set; }
        
        public ConfirmOrderCommand(Guid id)
        {
            Id = id;
        }
    }
}