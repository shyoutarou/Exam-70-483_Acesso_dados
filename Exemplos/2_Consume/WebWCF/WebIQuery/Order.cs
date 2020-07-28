using System.Collections.Generic;
using System.Data.Services.Common;

namespace WebIQuery
{
[DataServiceKeyAttribute("OrderId")]
public class Order
{
    public int OrderId { get; set; }
    public string Customer { get; set; }
    public IList<Item> Items { get; set; }
}
}