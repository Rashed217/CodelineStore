using CodelineStore.Data.Model;
using CodelineStore.DTOs.SellerDTOs;

namespace CodelineStore.Services
{
    public interface ISellerService
    {
        void AddSeller(SellerOutput input);
        bool EmailExists(string email);
        IEnumerable<Seller> GetAllSeller();
        Task<Seller> GetSellerAsync(int sellerId);
        Seller GetSellerById(int sellerId);
        Seller GetSellerByName(string sellerName);
        SellerOutput GetSellerData(string? sellerName, int? sellerId);
        Task<SellerOutput> GetSellerWithProductsAsync(int sellerId);
        void UpdateSeller(Seller seller);
    }
}