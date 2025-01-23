using CodelineStore.Data.Model;
using CodelineStore.DTOs.SellerDTOs;

namespace CodelineStore.Services
{
    public interface ISellerService
    {
        void AddSeller(SellerOutput input);
        Task<SellerOutput> GetSellerWithProductsAsync(int sellerId);
        bool EmailExists(string email);
        IEnumerable<Seller> GetAllSeller();
        Seller GetSellerById(int sellerId);
        Seller GetSellerByName(string sellerName);
        SellerOutput GetSellerData(string? sellerName, int? sellerId);
        void UpdateSeller(Seller seller);
    }
}