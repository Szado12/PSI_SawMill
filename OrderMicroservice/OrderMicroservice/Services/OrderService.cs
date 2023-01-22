using AutoMapper;
using CSharpFunctionalExtensions;
using OrderMicroservice.Models;
using OrderMicroservice.ModelViews;
using OrderMicroservice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OrderMicroservice.Services
{
    public class OrderService : DefaultService, IOrderService
    {
        public OrderService(ClientOrderContext clientOrderContext) : base(clientOrderContext)
        {
        }

        public Result<OrderView> AddOrder(ModifyOrderView data)
        {
            throw new NotImplementedException();
        }

        public Result<bool> DeleteOrder(int id)
        {
            var orderToCancel = ClientOrderContext.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            if (orderToCancel == null)
                return Result.Failure<bool>($"Order with id {id} not found.");
            orderToCancel.OrderStateId = ClientOrderContext.OrderStates.Where(x => x.Name == "Anulowane").FirstOrDefault().OrderStateId;

            if (ClientOrderContext.SaveChanges() > 0)
                return Result.Success(true);
            else
                return Result.Failure<bool>($"Cancelling order with id {id} failed.");
        }

        public Result<OrderView> GetOrderById(int id)
        {
            var result = ClientOrderContext.Orders
                .Where(x => x.OrderId == id)
                .Include(x => x.OrderState)
                .Include(x => x.Client)
                .Include(x => x.OrderDetails).ThenInclude(x => x.Product)
                .Include(x => x.OrderDetails).ThenInclude(x => x.Product.ProductType)
                .Include(x => x.OrderDetails).ThenInclude(x => x.Product.WoodType)
                .Select(Mapper.Map<Order, OrderView>)
                .FirstOrDefault();
            if (result == null)
                return Result.Failure<OrderView>($"Fetching order with id {id} failed.");
            return Result.Success(result);
        }

        public Result<List<OrderView>> GetOrders()
        {
            var result = ClientOrderContext.Orders
                .Include(x => x.OrderState)
                .Include(x => x.Client)
                .Include(x => x.OrderDetails).ThenInclude(x => x.Product)
                .Include(x => x.OrderDetails).ThenInclude(x => x.Product.ProductType)
                .Include(x => x.OrderDetails).ThenInclude(x => x.Product.WoodType)
                .Select(Mapper.Map<Order, OrderView>)
                .ToList();
            if(result == null)
                return Result.Failure<List<OrderView>>($"Fetching orders failed.");
            return Result.Success(result);
        }

        public Result<List<OrderView>> GetOrdersByClient(int clientId)
        {
            var result = ClientOrderContext.Orders
                .Where(x => x.ClientId == clientId)
                .Include(x => x.OrderState)
                .Include(x => x.Client)
                .Include(x => x.OrderDetails).ThenInclude(x => x.Product)
                .Include(x => x.OrderDetails).ThenInclude(x => x.Product.ProductType)
                .Include(x => x.OrderDetails).ThenInclude(x => x.Product.WoodType)
                .Select(Mapper.Map<Order, OrderView>)
                .ToList();
            if (result == null)
                return Result.Failure<List<OrderView>>($"Fetching client {clientId} orders failed.");
            return Result.Success(result);
        }

        public Result<List<OrderState>> GetOrderStates()
        {
            var result = ClientOrderContext.OrderStates.ToList();
            if (result == null)
                return Result.Failure<List<OrderState>>($"Fetching order states failed.");
            return Result.Success(result);
        }

        public Result<OrderView> UpdateOrder(ModifyOrderView data)
        {
            throw new NotImplementedException();
        }
    }
}
