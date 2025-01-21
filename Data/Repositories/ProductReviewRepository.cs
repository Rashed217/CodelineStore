using CodelineStore.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CodelineStore.Data.Repositories
{
    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductReview>> GetAllReviewsAsync()
        {
            return await _context.ProductReviews
                .Include(r => r.Product)
                .Include(r => r.Client)
                .ToListAsync();
        }

        public async Task<ProductReview> GetReviewByIdAsync(int id)
        {
            return await _context.ProductReviews
                .Include(r => r.Product)
                .Include(r => r.Client)
                .FirstOrDefaultAsync(r => r.RId == id);
        }

        public async Task<ProductReview> CreateReviewAsync(ProductReview review)
        {
            _context.ProductReviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<ProductReview> UpdateReviewAsync(ProductReview review)
        {
            _context.ProductReviews.Update(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var review = await _context.ProductReviews.FindAsync(id);
            if (review == null) return false;

            _context.ProductReviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
