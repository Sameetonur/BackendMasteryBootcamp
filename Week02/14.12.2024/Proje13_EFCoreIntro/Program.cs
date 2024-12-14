/*
    ORM -> Object-Relational Mapping
    Entity Framework Core: .Net dünyasında kullanılan en yaygın ve başarılı ORM paketi.

    ORM HANGİ SORUNLARA ÇÖZÜM BULUYOR
    * Nesne-Tablo ilişki uyumsuzluğunu giderme.
    * Soyutlama konusunda da çok büyük imkanlar getiriyor.
    * Veritabanı bağımsızlığı sağlıyor.

    EF Core'nin temel bileşenleri
    1) DBContext : Veri tabanı işlemleri için ana sınıfımız. EF Corenin kalbi mekrezi.
    2) DBSet : Tabloları temsil eden collectionlar.
    3) Entities : Veri tabanındaki tabloların c#  nesne karşılıkları.
*/

    /*
        Projeye ef core uygulamak için adımlar 
        1) Micrasoft.EntityFramework.SqlServer paketini kur(versiyonuna dikkat et)
        2) DbContext sınıfından miras alan context sınıfını oluştur. (Genelde AppDbContexti ApplicationDbContext gibi isimler verilir.)
        3) Entity sınıfını oluştur. (Categoryi Producti Project)
        4)Context içerisinde her bir entity için Dbset tanımlamalarını yap.
        5)Context içinde OnConfiguring metodunu override ederek ConnectionString bilgisini base class(DbContext)a gönder.
        6) dotnet ef migrations add MigrationName komutu ile migration oluştır.
        7) dotnet ef database update ile ilgili migrationdaki işlemleri veritabanına yansıt.
    */


    System.Console.WriteLine();
