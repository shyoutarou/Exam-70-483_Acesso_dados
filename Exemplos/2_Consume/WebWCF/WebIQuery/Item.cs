using System.Data.Services.Common;

namespace WebIQuery
{
[DataServiceKeyAttribute("Product")]
public class Item
{
    public string Product { get; set; }
    public int Quantity { get; set; }
}
}