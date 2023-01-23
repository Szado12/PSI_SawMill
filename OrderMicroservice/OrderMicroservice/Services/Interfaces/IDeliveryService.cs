using CSharpFunctionalExtensions;
using OrderMicroservice.Models;
using OrderMicroservice.ModelViews.Deliveries;

namespace OrderMicroservice.Services.Interfaces
{
    public interface IDeliveryService
    {
        Result<List<DeliveryView>> GetDeliveries(); 
        Result<DeliveryView> GetDeliveryById(int id);
        Result<DeliveryView> GetDeliveryByOrderId(int id);
        Result<List<DeliveryState>> GetDeliveryStates();
        Result<DeliveryView> UpdateDelivery(int id, int? stateId, int? delivererId);

    }
}
