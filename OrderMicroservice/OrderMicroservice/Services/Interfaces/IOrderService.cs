using CSharpFunctionalExtensions;
using OrderMicroservice.Models;
using OrderMicroservice.ModelViews.Orders;

namespace OrderMicroservice.Services.Interfaces
{
    public interface IOrderService
    {
        Result<List<OrderView>> GetOrders();
        Result<List<OrderView>> GetOrdersByClient(int clientId);
        Result<OrderView> GetOrderById(int id);
        Result<List<OrderStateView>> GetOrderStates();
        Result<OrderView> AddOrder(AddOrderView data);
        Result<OrderView> UpdateOrder(int id, int orderState);
        Result<bool> DeleteOrder(int id);
    }
}
