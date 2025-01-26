using CodelineStore.DTOs.ProductDTO;

namespace CodelineStore.DTOs.SellerDTOs
{
    public class SellerOutput
    {
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public string SellerProfileImage { get; set; }
        public int SellerRating { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
