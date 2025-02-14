﻿using CodelineStore.Data.Model;
using CodelineStore.Data.Repositories;
using CodelineStore.DTOs.ProductDTO;
using CodelineStore.DTOs.SellerDTOs;
using Microsoft.EntityFrameworkCore;
namespace CodelineStore.Services
{
    public class SellerService : ISellerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISellerRepository _sellerRepository;
        private readonly IUserService _userService;

        public SellerService(ISellerRepository sellerRepository, IUserService userService, ApplicationDbContext context)
        {
            _sellerRepository = sellerRepository;
            _userService = userService;
            _context = context;
        }

        public async Task<Seller> GetSellerAsync(int sellerId)
        {
            return await _context.Sellers
                .Where(s => s.SId == sellerId)
                .Select(s => new Seller
                {
                    SId = s.SId,
                    UserId = s.UserId,
                    SellerRating = s.SellerRating,
                    ProfileImagePath = s.ProfileImagePath,
                    User = s.User
                })
                .FirstOrDefaultAsync();
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

        public async Task<SellerOutput> GetSellerWithProductsAsync(int sellerId)
        {
            var seller = await _sellerRepository.GetSellerWithProductsAsync(sellerId);

            if (seller == null)
            {
                throw new InvalidOperationException($"Seller with ID {sellerId} not found.");
            }

            return new SellerOutput
            {
                SellerId = seller.SId,
                SellerName = seller.User.Username,
                SellerProfileImage = seller.ProfileImagePath ?? "https://via.placeholder.com/150",
                SellerRating = seller.SellerRating,
                Products = seller.Products.Select(p => new ProductDto
                {
                    ProductId = p.PId,
                    Name = p.Name,
                    Price = p.Price,
                    Image = !string.IsNullOrEmpty(p.Image) ? p.Image : "https://via.placeholder.com/300"
                }).ToList()
            };
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
                SellerId = seller.SId,
                SellerRating = seller.SellerRating,

            };
        }

        public void AddSeller(SellerOutput input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Client information is missing.");

            //if (input.SellerId <= 0)
            //    throw new ArgumentException("Invalid client ID.");

            var user = _userService.GetUserById(input.userId);
            if (user == null)
                throw new KeyNotFoundException($"No user found with ID {input.SellerId}.");

            if (!user.Role.Equals("Seller", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("The provided user ID does not belong to a client.");

            var seller = new Seller
            {
                User = user,
                UserId = user.UserId,
                SellerRating = (int)input.SellerRating,
                ProfileImagePath = input.SellerProfileImage
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
