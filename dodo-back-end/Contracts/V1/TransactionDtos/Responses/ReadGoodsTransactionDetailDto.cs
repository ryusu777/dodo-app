namespace DodoApp.Contracts.V1.Responses
{
    public class ReadGoodsTransactionDetailDto
    {
        public int Id { get; set; }
        public int GoodsTransactionHeaderId { get; set; }
        public int GoodsId { get; set; }
        public int GoodsAmount { get; set; }
        public int PricePerItem { get; set; }
        public ReadGoodsDto TheGoods { get; set; }
    }
}