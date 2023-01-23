using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OrderMicroservice.Models;
using OrderMicroservice.ModelViews.Deliveries;
using OrderMicroservice.ModelViews.Orders;
using OrderMicroservice.Services.Interfaces;

namespace OrderMicroservice.Services
{
    public class DeliveryService : DefaultService, IDeliveryService
    {
        public DeliveryService(ClientOrderContext clientOrderContext) : base(clientOrderContext)
        {
        }

        public Result<List<DeliveryView>> GetDeliveries()
        {
            var result = ClientOrderContext.Deliveries
                .Include(x => x.DeliveryState)
                .Include(x => x.Orders)
                .ThenInclude(x => x.Client)
                .ThenInclude(x => x.Address)
                .Select(Mapper.Map<Delivery, DeliveryView>)
                .ToList();

            if (result != null)
                return Result.Success(result);
            else
                return Result.Failure<List<DeliveryView>>("Fetching deliveries failed.");
        }

        public Result<DeliveryView> GetDeliveryById(int id)
        {
            var result = ClientOrderContext.Deliveries
                .Where(x => x.DeliveryId == id)
                .Include(x => x.DeliveryState)
                .Include(x => x.Orders)
                .ThenInclude(x => x.Client)
                .ThenInclude(x => x.Address)
                .Select(Mapper.Map<Delivery, DeliveryView>)
                .FirstOrDefault();

            if (result != null)
                return Result.Success(result);
            else
                return Result.Failure<DeliveryView>($"Fetching delivery with id {id} failed.");
        }

        public Result<DeliveryView> GetDeliveryByOrderId(int id)
        {
            var result = ClientOrderContext.Deliveries
                .Where(x => x.Orders.First().OrderId == id)
                .Include(x => x.DeliveryState)
                .Include(x => x.Orders)
                .ThenInclude(x => x.Client)
                .ThenInclude(x => x.Address)
                .Select(Mapper.Map<Delivery, DeliveryView>)
                .FirstOrDefault();

            if (result != null)
                return Result.Success(result);
            else
                return Result.Failure<DeliveryView>($"Fetching delivery with order id {id} failed.");
        }

        public Result<List<DeliveryState>> GetDeliveryStates()
        {
            var result = ClientOrderContext.DeliveryStates.ToList();

            if (result != null)
                return Result.Success(result);
            else
                return Result.Failure<List<DeliveryState>>($"Fetching delivery states failed.");
        }

        public Result<DeliveryView> UpdateDelivery(int id, int? stateId, int? delivererId)
        {
            var deliveryToEdit = ClientOrderContext.Deliveries.Where(x => x.DeliveryId == id).FirstOrDefault();
            if (deliveryToEdit == null)
                return Result.Failure<DeliveryView>($"Updating delivery with id {id} failed.");

            if (stateId != null)
            {
                deliveryToEdit.DeliveryStateId = stateId ?? 0;
                if(stateId == (int)DeliveryStateEnum.Send)
                    deliveryToEdit.SendDate = DateTime.Now;
            }

            if (delivererId != null)
                deliveryToEdit.DelivererId = delivererId ?? 0;

            if (ClientOrderContext.SaveChanges() > 0)
                return GetDeliveryById(deliveryToEdit.DeliveryId);
            else
                return Result.Failure<DeliveryView>($"Updating delivery with id {id} failed.");
        }
    }
}
