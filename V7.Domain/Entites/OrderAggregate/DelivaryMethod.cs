namespace V7.Domain.Entites.OrderAggregate
{
    public class DelivaryMethod : BaseEntity
    {
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Cost { get; set; }
    }
}
