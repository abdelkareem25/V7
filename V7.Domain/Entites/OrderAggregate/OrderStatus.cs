using System.Runtime.Serialization;

namespace V7.Domain.Entites.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,//0 

        [EnumMember(Value = "PaymentReceived")]
        PaymentReceived, // 1
        
        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed // 2
    }
}
