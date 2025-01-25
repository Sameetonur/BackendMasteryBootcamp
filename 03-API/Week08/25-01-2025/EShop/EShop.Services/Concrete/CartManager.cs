using System;
using AutoMapper;
using EShop.Data.Abstract;
using EShop.Entity.Concrete;
using EShop.Services.Abstract;
using EShop.Shared.Dtos;
using EShop.Shared.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace EShop.Services.Concrete;

public class CartManager : ICartService
{

    private readonly IUnitOfWork _unitoFWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Cart> _cartRepository;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IGenericRepository<CartItem> _cartItemRepository;

    public CartManager(IUnitOfWork unitoFWork, IMapper mapper)
    {
        _unitoFWork = unitoFWork;
        _mapper = mapper;
        _cartRepository = _unitoFWork.GetRepository<Cart>();
        _productRepository = _unitoFWork.GetRepository<Product>();
        _cartItemRepository = _unitoFWork.GetRepository<CartItem>();
    }

    public async Task<ResponseDto<CartItemDto>> AddToCartAsync(CartItemCreateDto cartItemCreateDto)
    {
        try
        {
            var product = await _productRepository.GetAsync(cartItemCreateDto.ProductId);
            if (product == null)
            {
                return ResponseDto<CartItemDto>.Fail("Ürün bulunamadı", StatusCodes.Status404NotFound);
            }
            if (!product.IsActive)
            {
                return ResponseDto<CartItemDto>.Fail("Ürün aktif değil", StatusCodes.Status400BadRequest);

            }

            var cartItem = new CartItem(
                cartItemCreateDto.CartId,
                cartItemCreateDto.ProductId,
                cartItemCreateDto.Quantity

            );
            await _cartItemRepository.AddAsync(cartItem);
            var result = await _unitoFWork.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<CartItemDto>.Fail("Ürün eklenirken bir hata oluştu", StatusCodes.Status500InternalServerError);

            }
            var cartItemDto = _mapper.Map<CartItemDto>(cartItem);
            return ResponseDto<CartItemDto>.Success(cartItemDto, StatusCodes.Status201Created);

        }
        catch (Exception ex)
        {
            return ResponseDto<CartItemDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<CartItemDto>> ChangeQuantityAsync(CartItemUpdateDto cartItemUpdateDto)
    {
        try
        {

            var cartItem = await _cartItemRepository.GetAsync(
                x => x.Id == cartItemUpdateDto.Id,
                query => query.Include(x => x.Product)


            ); //cartItemUpdateDto.Id ile cartItem'ı bul ve güncelle
            //cartItem bulunamazsa hata dön
            if (cartItem == null)
            {
                return ResponseDto<CartItemDto>.Fail("Ürün bulunamadı", StatusCodes.Status404NotFound);
            }


            cartItem.Quantity = cartItemUpdateDto.Quantity; //cartItem'ın miktarını güncelle
            _cartItemRepository.Update(cartItem); //cartItem'ı güncelle
            var result = await _unitoFWork.SaveAsync(); //değişiklikleri kaydet
            if (result < 1)
            {
                return ResponseDto<CartItemDto>.Fail("Ürün miktarı güncellenirken bir hata oluştu", StatusCodes.Status500InternalServerError);
            }
            var CartItemDto = _mapper.Map<CartItemDto>(cartItem); //cartItem'ı CartItemDto'ya dönüştür
            return ResponseDto<CartItemDto>.Success(CartItemDto, StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<CartItemDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<NoContent>> ClearCartAsync(string applicationUserId)
    {
        try
        {
            var cart = await _cartRepository.GetAsync(
                x => x.ApplicationUserId == applicationUserId,
                query => query.Include(x => x.CartItems)
            );
            if (cart == null)
            {
                return ResponseDto<NoContent>.Fail("Sepet bulunamadı", StatusCodes.Status404NotFound);

            }
            cart.CartItems?.Clear();
            _cartRepository.Update(cart);
            var result = await _unitoFWork.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<NoContent>.Fail("Sepet temizlenirken bir hata oluştu", StatusCodes.Status500InternalServerError);
            }
            return ResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
        }
        catch (Exception ex)
        {
            return ResponseDto<NoContent>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<CartDto>> CreateCartAsync(string applicationUserId)
    {
        if (string.IsNullOrEmpty(applicationUserId))//is null or empty hem null mu hem boşmuyu tekte kontrol etmemi sağlıyor.
        {
            return ResponseDto<CartDto>.Fail("Kullanıcı id'si boş olamaz", StatusCodes.Status400BadRequest);
        }
        var existscart = await _cartRepository.GetAsync(x => x.ApplicationUserId == applicationUserId);
        if (existscart != null)
        {
            var cartDto = _mapper.Map<CartDto>(existscart);
            return ResponseDto<CartDto>.Success(cartDto, StatusCodes.Status400BadRequest);
        }
        var cart = new Cart(applicationUserId);
    }

    public Task<ResponseDto<NoContent>> DeleteCartAsync(int cartItemId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<CartDto>> GetCartAsync(string applicationUserId)
    {
        throw new NotImplementedException();
    }
}
