/*
Collections = c#  içerisinde dizi benzeri bir veri yapısını ifade eder. Bir kaç türü vardır.

1) IEnumerable: En temel collection yapılarından biridir. ve şu özelliklere sahiptir.
    * Iteration sağlar(döngü kullanabilmeyi sağlar)
    * Verilerin sadece okunması gereken durumlarda tercih edilir.
    * Bu durumda ekleme silme gibi operasyonlara izin vermez.
    * Özellikle foreach döngüsü ile kullanmaya oldukça uygundur.
    * Verileri belleğe almadan işlem yapar.

*/

// List<string> names = new List<string>{"Ali","Veli","Murat","Sezen"};

// IEnumerable<string> enumerableNames = names;

// foreach(var item in enumerableNames)
// {
//     System.Console.WriteLine(item);
// }

/*

2) ICollection : IEnumerable'den türetilmiştir.
    Ek olarakk şu özelliklere sahiptir.
        * Veri ekleme ve silme opreasyonlarını desktekleri.
        *  Eleman sayısını öğrenme imkanı sağlar.
        *  Veri üzerinde iteration kullanırken manipilasyon yapma ihtiyacı olan durumlarda tercih edilir.
         
*/
// ICollection<string> names = new List<string>{"Ali","Veli","Murat","Sezen"};
// System.Console.WriteLine(names.Count);

/*
   3) Ilist : ICollection'dan türetilmiştir. Ek olarak sahip olduğu özellikler şunlardır:
        * Indexleme imkanı sunar  y sadece collection elemanlarına bir indez numarası ile erişebilir.
        * sıralı veri yapılarında tercih edilir.
    */

// IList<string> names = new List<string> { "Ali", "Veli", "Murat", "Sezen" };
// System.Console.WriteLine(names[0]);

/*
    4) IQuerable: bu collection yapısı biraz daha özel amaçlara hizmet eder:
        * Daha çok veri tabanı sorguları oluşturmak için tercih edilir.
        * Sorguyu veri tabanına göndermeden önce optimize eder.
        * Büyük veri setlerinde çok ciddi bir performans sağlar.
        * Filtreleme operasyonlarını direkt olarak veri tabanı üzerinde yapark hız kazandırır.


*/