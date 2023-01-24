using AutoMapper;
using CSharpFunctionalExtensions;
using OrderMicroservice.Models;
using OrderMicroservice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OrderMicroservice.ModelViews.Orders;
using OrderMicroservice.ModelViews.Deliveries;

namespace OrderMicroservice.Services
{
    public class OrderService : DefaultService, IOrderService
    {
        public OrderService(ClientOrderContext clientOrderContext) : base(clientOrderContext)
        {
        }

        public Result<OrderView> AddOrder(AddOrderView data)
        {
            var deliveryToAdd = new Delivery
            {
                DeliveryStateId = (int)DeliveryStateEnum.Created
            };

            ClientOrderContext.Deliveries.Add(deliveryToAdd);
            if (ClientOrderContext.SaveChanges() == 0)
                return Result.Failure<OrderView>($"Adding order failed.");

            var orderToAdd = Mapper.Map<AddOrderView, Order>(data);
            orderToAdd.DeliveryId = deliveryToAdd.DeliveryId;
            ClientOrderContext.Add(orderToAdd);

            if (ClientOrderContext.SaveChanges() > 0)
                return GetOrderById(orderToAdd.OrderId);
            else
                return Result.Failure<OrderView>($"Adding order failed.");
        }

        public Result<bool> DeleteOrder(int id)
        {
            var orderToCancel = ClientOrderContext.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            if (orderToCancel == null)
                return Result.Failure<bool>($"Order with id {id} not found.");
            orderToCancel.OrderStateId = (int)OrderStateEnum.Cancelled;

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

        public Result<List<OrderStateView>> GetOrderStates()
        {
            var result = ClientOrderContext.OrderStates.Select(Mapper.Map<OrderState, OrderStateView>).ToList();
            if (result == null)
                return Result.Failure<List<OrderStateView>>($"Fetching order states failed.");
            return Result.Success(result);
        }

        public Result<OrderView> UpdateOrder(int id, int orderState)
        {
            var orderToEdit = ClientOrderContext.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            if (orderToEdit == null)
                return Result.Failure<OrderView>($"Fetching order with id {id} failed.");

            orderToEdit.OrderStateId = orderState;
            if (orderState == (int)OrderStateEnum.Accepted)
                orderToEdit.AcceptanceDate = DateTime.Now;

            if (ClientOrderContext.SaveChanges() > 0)
                return GetOrderById(orderToEdit.OrderId);
            else
                return Result.Failure<OrderView>($"Updating order with id {id} failed.");
        }
    }
}
