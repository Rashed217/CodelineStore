using CodelineStore.Data.Model;

namespace CodelineStore.Data.Repositories
{
    public interface IProductReviewRepository
    {
        Task<ProductReview> CreateReviewAsync(ProductReview review);
        Task<bool> DeleteReviewAsync(int id);
        Task<List<ProductReview>> GetAllReviewsAsync();
        Task<ProductReview> GetReviewByIdAsync(int id);
        Task<ProductReview> UpdateReviewAsync(ProductReview review);
    }
}