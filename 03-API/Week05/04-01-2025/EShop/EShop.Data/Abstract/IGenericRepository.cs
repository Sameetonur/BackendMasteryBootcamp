using System;
using System.Linq.Expressions;

namespace EShop.Data.Abstract;

public interface IGenericRepository<TEntity> where TEntity : class
{

    Task<TEntity> GetAsync(int id);
    Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        params Func<IQueryable<TEntity>,IQueryable<TEntity>>[] includes
        ); // BİR TEKİL VERİYLE ÇALIŞARAK KOŞUL KOŞMA VE BİRDEN VAZLA TEKİL VERİ AMA BİRDEN FAZLA TABLOYA JOİN(İNCLUDE ) YAPMAMA DA İZİN VEREN METOD

    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy=null,
        params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes
    ); //BİR LİSTE GETRİCEK ŞEKİLDE BİR ÖZELLİĞE GÖRE ÇEKİP VEYA SIRALAMA  HEM DE İNCLUD(JOİN) İŞLEMİ YAPMAMI SAĞLAYAN METOD

    Task<bool> ExistsAsync(Expression<Func<TEntity,bool>> predicate); //VERİ VAR MI YOK MU YU TRU FFALSE OLARAK DÖNEN METOD
    Task<int> CountAsync(); // SAYI SAYAN SOLFT BİR ŞEKİLDE

    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate); // BİR KOŞULA GÖRE SAYMA İŞLEMİ YAPAN METOD

    void Update(TEntity entity);
    void Delete(TEntity entity);

    Task<TEntity> AddAsync(TEntity entity);


}
