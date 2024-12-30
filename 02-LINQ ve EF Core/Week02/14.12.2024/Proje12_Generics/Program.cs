/*  

    Generic yapılar , özellikle tip güvenliği sağlama konusunda işimii görürüler fakat aynı zamanda kodun yeniden kullanılabilirliğini arttırır.
    DRY = Dont't Repeat Yourself
*/  

    //Dictionary<TKey ,TValue>
    
    Dictionary<string , int > ages = new();
        ages.Add("Samet",24);
        ages.Add("Mehmet",26);
        ages.Add("Mahmut",20);
        ages.Add("Selin",18);
            foreach (var item in ages)
            {
                System.Console.WriteLine($"{item.Key}: {item.Value}");
            }

    ages.Remove("Can");

    foreach (var item in ages)
    {
        System.Console.WriteLine($"{item.Key} : {item.Value}");
    }
    // Box<int> numberBox = new Box<int>();
    // numberBox.Add(5);
    // System.Console.WriteLine(numberBox.Get());

    // Box<string> stringBox = new Box<string>();
    // stringBox.Add("Aleyna");
    // System.Console.WriteLine(stringBox.Get());



    // class Box<T>
    // {
    //     private T content;
        
    //     public void Add(T item)
    //     {
    //         content = item;
    //     }

    //     public T Get()
    //     {
    //         return content;
    //     }

        
    // }