using CodelineStore.Data.Model;
using CodelineStore.Data.Repositories;

namespace CodelineStore.Services
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly IProductReviewRepository _reviewRepository;

        public ProductReviewService(IProductReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<List<ProductReview>> GetAllReviewsAsync()
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            if (reviews == null || !reviews.Any())
            {
                throw new InvalidOperationException("No reviews found.");
            }

            return reviews;
        }

        public async Task<ProductReview> GetReviewByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Review ID must be greater than zero.");
            }

            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
            {
                throw new KeyNotFoundException($"Review with ID {id} not found.");
            }

            return review;
        }

        public async Task<ProductReview> CreateReviewAsync(ProductReview review)
        {
            ValidateReview(review);

            // Prevent duplicate reviews by the same client for the same product
            var existingReviews = await _reviewRepository.GetAllReviewsAsync();
            if (existingReviews.Any(r => r.ClientId == review.ClientId && r.ProductId == review.ProductId))
            {
                throw new InvalidOperationException("A review from this client for the specified product already exists.");
            }

            return await _reviewRepository.CreateReviewAsync(review);
        }

        public async Task<ProductReview> UpdateReviewAsync(ProductReview review)
        {
            if (review.RId <= 0)
            {
                throw new ArgumentException("Review ID must be greater than zero.");
            }

            var existingReview = await _reviewRepository.GetReviewByIdAsync(review.RId);
            if (existingReview == null)
            {
                throw new KeyNotFoundException($"Review with ID {review.RId} not found.");
            }

            ValidateReview(review);

            return await _reviewRepository.UpdateReviewAsync(review);
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Review ID must be greater than zero.");
            }

            var success = await _reviewRepository.DeleteReviewAsync(id);
            if (!success)
            {
                throw new KeyNotFoundException($"Review with ID {id} not found.");
            }

            return success;
        }

        /// <summary>
        /// Validates the product review data.
        /// </summary>
        /// <param name="review">The product review to validate.</param>
        private void ValidateReview(ProductReview review)
        {
            if (review == null)
            {
                throw new ArgumentNullException(nameof(review), "Review cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(review.ReviewContent))
            {
                throw new ArgumentException("Review comment cannot be empty.");
            }

            if (review.ReviewContent.Length > 500)
            {
                throw new ArgumentException("Review comment cannot exceed 500 characters.");
            }

            if (review.Rating < 0 || review.Rating > 5)
            {
                throw new ArgumentException("Review rate must be between 0 and 5.");
            }

            if (review.ClientId <= 0)
            {
                throw new ArgumentException("Client ID must be greater than zero.");
            }

            if (review.ProductId <= 0)
            {
                throw new ArgumentException("Product ID must be greater than zero.");
            }
        }
    }
}
