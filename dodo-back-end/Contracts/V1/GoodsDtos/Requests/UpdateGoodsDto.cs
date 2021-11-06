
namespace DodoApp.Contracts.V1.Requests
{
    public class UpdateGoodsDto
    {
        public int Id { get; set; }
        public string GoodsName { get; set; }
        public string GoodsCode { get; set; }
        public string CarType { get; set; }
        public string PartNumber { get; set; }
        public int PurchasePrice { get; set; }
        public int StockAvailable { get; set; }
        public int MinimalAvailable { get; set; }
    }
}