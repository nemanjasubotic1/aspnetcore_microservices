using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace BasketECommerce.Web.Models.Orders;

[JsonConverter(typeof(StringEnumConverter))]
public enum OrderStatus
{
    Draft, Pending, Verified, Completed, Canceled
}