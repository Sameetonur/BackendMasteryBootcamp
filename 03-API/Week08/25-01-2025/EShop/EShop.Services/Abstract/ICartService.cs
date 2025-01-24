using System;
using EShop.Shared.Dtos;
using EShop.Shared.Dtos.ResponseDtos;

namespace EShop.Services.Abstract;

public interface ICartService
{
    Task<ResponseDto<CartDto>> GetCartByUserIdAsync(string userId); // Sepeti kullanıcıya göre getirir

    // Sepete ürün ekler
    Task<ResponseDto<CartDto>> AddToCartAsync(string userId, CartItemCreateDto cartItemCreateDto);

    // Sepetten ürün çıkarır
    Task<ResponseDto<NoContent>> RemoveFromCartAsync(int cartItemId);

    // Sepetteki bir ürünün miktarını günceller
    Task<ResponseDto<CartDto>> UpdateCartAsync(CartUpdateDto cartUpdateDto);

    // Kullanıcının sepetini tamamen temizler
    Task<ResponseDto<NoContent>> ClearCartAsync(string userId);

    // Sepetin toplam tutarını hesaplar
    Task<ResponseDto<decimal>> GetCartTotalAsync(string userId);

    // Sepeti siparişe dönüştürür (Checkout)
    Task<ResponseDto<OrderDto>> CheckoutAsync(string userId);

    // Sepetteki ürün sayısını getirir
    Task<ResponseDto<int>> GetCartItemCountAsync(string userId);

    // Sepetteki ürünün varlığını kontrol eder
    Task<ResponseDto<bool>> IsProductInCartAsync(string userId, int productId);


}
