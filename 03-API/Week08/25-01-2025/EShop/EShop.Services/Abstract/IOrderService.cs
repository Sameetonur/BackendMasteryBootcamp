using System;
using EShop.Shared.ComplexTypes;
using EShop.Shared.Dtos;
using EShop.Shared.Dtos.ResponseDtos;

namespace EShop.Services.Abstract;

public interface IOrderService
{
    // Siparişi ID'ye göre getirir
    Task<ResponseDto<OrderDto>> GetAsync(int id);

    // Tüm siparişleri getirir
    Task<ResponseDto<IEnumerable<OrderDto>>> GetAllAsync();

    // Sipariş ekler
    Task<ResponseDto<OrderDto>> AddAsync(OrderCreateDto orderDto);


    // Siparişi siler (Soft delete)
    Task<ResponseDto<NoContent>> SoftDeleteAsync(int id);

    // Siparişi kalıcı olarak siler (Hard delete)
    Task<ResponseDto<NoContent>> HardDeleteAsync(int id);

    // Sipariş sayısını getirir
    Task<ResponseDto<int>> CountAsync();

    // Sipariş sayısını filtreye göre getirir (Örn: aktif, pasif)
    Task<ResponseDto<IEnumerable<OrderDto>>> GetByStatusAsync(OrderStatusType? status);

    // Siparişin durumunu günceller (Örn: gönderildi, onaylandı vb.)
    Task<ResponseDto<OrderDto>> UpdateOrderStatusAsync(int id, OrderStatusType status);

    // Siparişi kullanıcıya göre getirir
    Task<ResponseDto<IEnumerable<OrderDto>>> GetUserOrdersAsync(string userId);


}
