using CodelineStore.Data.Model;

namespace CodelineStore.Services
{
    public interface IProductReviewService
    {
        Task<ProductReview> CreateReviewAsync(ProductReview review);
        Task<bool> DeleteReviewAsync(int id);
        Task<List<ProductReview>> GetAllReviewsAsync();
        Task<ProductReview> GetReviewByIdAsync(int id);
        Task<ProductReview> UpdateReviewAsync(ProductReview review);
    }
}