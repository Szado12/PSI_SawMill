namespace OrderMicroservice.ModelViews.Orders
{
    public enum OrderStateEnum
    {
        Created = 1,
        Accepted = 2,
        Cancelled = 3,
        ToBeSend = 4,
        Send = 5,
        Shipped = 6
    }
}
