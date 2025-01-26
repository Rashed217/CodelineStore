using CodelineStore.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CodelineStore.Data.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly ApplicationDbContext _context;
        public SellerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Seller> GetAllSellers()
        {
            return _context.Sellers;
        }

        public Seller GetSellerById(int id)
        {
            return _context.Sellers.Find(id);
        }

        public async Task<Seller> GetSellerWithProductsAsync(int sellerId)
        {
            // Retrieve the seller along with related products and product images
            return await _context.Sellers
                .Include(s => s.Products)
                    .ThenInclude(p => p.ProductImages)
                .Include(s => s.User) // Include the user for the seller's name
                .FirstOrDefaultAsync(s => s.SId == sellerId);
        }

        public Seller AddSeller(Seller seller)
        {
            _context.Sellers.Add(seller);
            _context.SaveChanges();
            return seller;
        }

        public Seller UpdateSeller(Seller seller)
        {
            _context.Sellers.Update(seller);
            _context.SaveChanges();
            return seller;
        }

        public void DeletSeller(Seller seller)
        {
            _context.Sellers.Remove(seller);
            _context.SaveChanges();
        }


        public bool IsValidRole(string roleName)
        {
            var validRoles = new List<string> { "Client", "Seller" };
            return validRoles.Contains(roleName);
        }


        public bool EmailExists(string email)
        {
            try
            {
                return _context.Users.Any(u => u.Email == email);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }
        public Seller GetSellerByName(string sellerName)
        {
            if (string.IsNullOrEmpty(sellerName))
            {
                throw new ArgumentException("client name cannot be null or empty.", nameof(sellerName));
            }

            try
            {
                // Use Include to join User with Doctor and filter by UserName
                var seller = _context.Sellers
                    .Include(d => d.User) // Join with User table
                    .FirstOrDefault(d => d.User.Username == sellerName);

                return seller;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while accessing the database.", ex);
            }
        }
    }
}
