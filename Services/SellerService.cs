using CodelineStore.Data.Model;
using CodelineStore.Data.Repositories;
using CodelineStore.DTOs.SellerDTOs;
namespace CodelineStore.Services
{
    public class SellerService : ISellerService
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IUserService _userService;

        public SellerService(ISellerRepository sellerRepository, IUserService userService)
        {
            _sellerRepository = sellerRepository;
            _userService = userService;
        }

        public IEnumerable<Seller> GetAllSeller()
        {
            return _sellerRepository.GetAllSellers();
        }

        public Seller GetSellerById(int sellerId)
        {
            var seller = _sellerRepository.GetSellerById(sellerId);
            if (seller == null)
                throw new KeyNotFoundException($"seller with ID {sellerId} not found.");
            return seller;
        }

        public bool EmailExists(string email)
        {
            return _sellerRepository.EmailExists(email);
        }

        public SellerOutput GetSellerData(string? sellerName, int? sellerId)
        {
            if (string.IsNullOrWhiteSpace(sellerName) && !sellerId.HasValue)
                throw new ArgumentException("Either client name or client ID must be provided.");

            Seller seller = null;

            if (!string.IsNullOrEmpty(sellerName))
                seller = GetSellerByName(sellerName);

            if (sellerId.HasValue)
                seller = GetSellerById(sellerId.Value);

            if (seller == null)
                throw new KeyNotFoundException("seller not found.");

            return new SellerOutput
            {
                SId = seller.SId,
                SellerRating = seller.SellerRating,

            };
        }

        public void AddSeller(SellerOutput input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Client information is missing.");

            if (input.SId <= 0)
                throw new ArgumentException("Invalid client ID.");

            var user = _userService.GetUserById(input.ID);
            if (user == null)
                throw new KeyNotFoundException($"No user found with ID {input.ID}.");

            if (!user.Role.Equals("client", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("The provided user ID does not belong to a client.");

            var seller = new Seller
            {
                SId = user.Seller.SId,
                SellerRating = (int)input.SellerRating,

            };

            _sellerRepository.AddSeller(seller);
        }



        public void UpdateSeller(Seller seller)
        {
            if (seller == null)
                throw new ArgumentNullException(nameof(seller), "Client information is required.");

            var existingSeller = _sellerRepository.GetSellerById(seller.SId);
            if (existingSeller == null)
                throw new KeyNotFoundException($"Client with ID {seller.SId} not found.");

            _sellerRepository.UpdateSeller(seller);
        }

        public Seller GetSellerByName(string sellerName)
        {
            if (string.IsNullOrEmpty(sellerName))
                throw new ArgumentException("Client name cannot be null or empty.");

            var seller = _sellerRepository.GetSellerByName(sellerName);
            if (seller == null)
                throw new KeyNotFoundException($"seller with name '{sellerName}' not found.");

            return seller;
        }
    }
}
