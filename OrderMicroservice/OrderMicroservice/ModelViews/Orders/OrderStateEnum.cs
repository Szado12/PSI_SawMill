namespace OrderMicroservice.ModelViews.Orders
{
    public enum OrderStateEnum
    {
        Created = 1,
        Cancelled = 2,
        Accepted = 3,
        Ready = 4,
        Send = 5,
        Shipped = 6
    }
}
