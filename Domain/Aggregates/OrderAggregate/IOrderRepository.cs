using System;
using System.Threading.Tasks;
using Domain.SeedWork;

namespace Domain.Aggregates.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
	{
        Order Add(Order order);

        void Update(Order order);

        Task<Order> GetAsync(Guid orderId);

        Task<Order> ConfirmOrder(Order order);
	}
}