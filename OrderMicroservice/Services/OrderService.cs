using AutoMapper;
using CSharpFunctionalExtensions;
using OrderMicroservice.Models;
using OrderMicroservice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OrderMicroservice.ModelViews.Orders;
using OrderMicroservice.ModelViews.Deliveries;
using Newtonsoft.Json;
using System.IO;
using OrderMicroservice.ModelViews;

namespace OrderMicroservice.Services
{
    public class OrderService : DefaultService, IOrderService
    {
        static readonly HttpClient client = new HttpClient();
        public OrderService(ClientOrderContext clientOrderContext) : base(clientOrderContext)
        {
        }

        public Result<OrderView> AddOrder(AddOrderView data)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri("http://store-microservice:5001/api/Product/RemoveFromStore"));
            request.Content = JsonContent.Create(data.OrderDetails.Select(Mapper.Map<ModifyOrderDetailView, ProductIdAndAmount>));
            var response = client.Send(request);
            if (!response.IsSuccessStatusCode)
                return Result.Failure<OrderView>($"Not enough products.");

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
            if(ClientOrderContext.SaveChanges() == 0)
                return Result.Failure<OrderView>($"Adding order failed.");
            else
                return GetOrderById(orderToAdd.OrderId);
        }

        public Result<bool> DeleteOrder(int id)
        {
          var orderToCancel = ClientOrderContext.Orders.FirstOrDefault(x => x.OrderId == id);
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
            var orderToEdit = ClientOrderContext.Orders.FirstOrDefault(x => x.OrderId == id);
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
