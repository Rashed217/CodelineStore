using CodelineStore.Data.Model;

namespace CodelineStore.Data.Repositories
{
    public interface ISellerRepository
    {
        Seller AddSeller(Seller seller);
        void DeletSeller(Seller seller);
        bool EmailExists(string email);
        IEnumerable<Seller> GetAllSellers();
        Seller GetSellerById(int id);
        Seller GetSellerByName(string sellerName);
        bool IsValidRole(string roleName);
        Seller UpdateSeller(Seller seller);
    }
}