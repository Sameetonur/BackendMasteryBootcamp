using System;
using System.Text.Json.Serialization;

namespace EShop.Shared.Dtos.ResponseDtos;
// bu sınıf içinde;
// 1* geri döndürülecek datayı
// 2* geri döndürülecek hata mesajını
// 3* geri döndürülecek hata kodu
// 4* geri döndürülecek işlemin başarılı olup olmadığını tutacağız.
public class ResponseDto<T>
{
    public T? Data { get; set; }
    public string? Error {get; set;}
    
    [JsonIgnore]
    public int StatusCode { get; set; }
    public bool IsSuccessful { get; set; }

    //Başarılı durumlarda kullanılacak  metot

    public static ResponseDto<T> Success(T data, int statusCode)
    {
        return new ResponseDto<T>
        {
            Data =data,
            StatusCode= statusCode,
            IsSuccessful=true
        };
    }
    // başarılı ama geriye data döndürülmeyecek durumlarda kullanılacak metot

    public static ResponseDto<T> Success(int statusCode)
    {
        return new ResponseDto<T>
        {
            Data = default,
            StatusCode= statusCode,
            IsSuccessful=true,
        };
    }

    // Hata durumunda kullanılacak metot
    public static ResponseDto<T> Fail(string error,int statusCode) // factorydesignpattert kendinden oluşan veriyi üreten pattern.
    {
        return new ResponseDto<T>
        {
            Error = error,
            StatusCode = statusCode,
            IsSuccessful = true,
        };
    }
}
