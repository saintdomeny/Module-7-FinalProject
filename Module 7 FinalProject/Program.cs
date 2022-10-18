using System.Net.Cache;
using System.Xml.Linq;

namespace Module_7_FinalProject
{
    abstract class Delivery
    {
        public string Address;
        protected double Price;
        public Delivery(string address)
        {
            Address = address;
        }
        public void DisplayAddress()
        {
            Console.WriteLine(Address);
        }
        public abstract void GetOrder();//абстрактный метод.
    }

    class HomeDelivery : Delivery
    {
        public int CourierID { get; set; } //свойства
        public bool ReadyToDeliver;
        public HomeDelivery(string address, bool ReadyToDeliver) : base(address)// конструктор класса с параметрами
        {
            ReadyToDeliver = ReadyToDeliver;
        }
        public override void GetOrder()
        {
            Console.WriteLine("Это доставка на дом.");
        }
    }

    class PickPointDelivery : Delivery
    {
        public int PickPointID;
        public bool Delivered;
        public PickPointDelivery(string address,int PickPointID, bool Delivered) : base(address) // конструктор класса с параметрами
        {
            Delivered = Delivered;
            PickPointID = PickPointID;
        }
        public override void GetOrder()
        { 
            Console.WriteLine("Это доставка в пункт выдачи."); 
        }

    }

    class ShopDelivery : Delivery
    {
        public int ShopID;
        public bool Delivered;
        public ShopDelivery(string address, int ShopID, bool Delivered) : base(address) // конструктор класса с параметрами
        {
            Delivered = Delivered;
            ShopID = ShopID;
        }
        public override void GetOrder()
        { 
            Console.WriteLine("Это доставка в розничный магазин."); 
        }
    }

    class Order<TDelivery,
    TStruct> where TDelivery : Delivery
    {
        public TDelivery Delivery;

        public int Number;

        public string Description;

        public void DisplayAddress()
        {
            Console.WriteLine(Delivery.Address);
        }
        public void DisplayPrice()
        {
            Console.WriteLine(ProductType.minPrice);//использование статического элемента
        }
    }
    static class StringExtensions // метод расширения
    {
        public static char GetFirstChar(this string source)
        {
            return source[0];
        }
    }
    class MyArr
    {
        // Координаты точки на карте
        public double x, y;

        public MyArr(double x = 0.0, double y = 0.0)
        {
            this.x = x;
            this.y = y;
        }

        // Перегружаем бинарный оператор +
        public static MyArr operator +(MyArr obj1, MyArr obj2)
        {
            MyArr arr = new MyArr();
            arr.x = obj1.x + obj2.x;
            arr.y = obj1.y + obj2.y;
            return arr;
        }

        // Перегружаем бинарный оператор - 
        public static MyArr operator -(MyArr obj1, MyArr obj2)
        {
            MyArr arr = new MyArr();
            arr.x = obj1.x - obj2.x;
            arr.y = obj1.y - obj2.y;
            return arr;
        }
    }
    class Product
    {
        public string Type;
        public string Model;
        public void Display<T>(T param)//обобщенный метод
        {
            Console.WriteLine(param.ToString());
        }
    }
    class ProductCollection
    {
        private Product[] collection;   //агрегация
        public ProductCollection(Product[] collection)
        {
            this.collection = collection;
        }
        public Product this[int index]//индексатор
        {
            get
            {
                
                if (index >= 0 && index < collection.Length)
                {
                    return collection[index];
                }
                else
                {
                    return null;
                }
            }

            private set
            {
                
                if (index >= 0 && index < collection.Length)
                {
                    collection[index] = value;
                }
            }
        }
    }
    class ProductType // композиция
    {
        private Product product;
        public static double minPrice = 2.0; //статические элементы.
        public ProductType()
        {
            product = new Product();
        }
    }
    abstract class Parcel 
    {
        public string ParcelId;
        public void DisplayId()
        {
            Console.WriteLine(ParcelId);
        }
    }
    class PremiumCustomer<TParcel> : Customer<TParcel> where TParcel : Parcel //наследование обобщений
    {
        public TParcel Field;
    }
    class Customer<TParcel> where TParcel : Parcel //обобщение и наследование + инкапсуляция
    {
        public TParcel Parcel;

        private string Name;
        private string LastName;
        private int age;
        public int Age //свойства с логикой
        {
            get
            {
                return age;
            }
            set
            {
                if (value < 18)
                {
                    Console.WriteLine("Возраст должен быть не меньше 18");
                }
                else
                {
                    age = value;
                }
            }
        }
        public string GetName()
        {
            return Name;
        }
        public string GetLastName()
        {
            return LastName;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Product obj = new Product();
            obj.Display<string>("Тип Продукта:");//Использование обобщенного метода
            obj.Display<int>(365);

            Console.WriteLine("string".GetFirstChar());//использование метода расширения

            var array = new Product[]
            {
                new Product
                {
                    Type = "Chair",
                    Model = "S-550"
                },
                new Product
                {
                    Type = "Sofa",
                    Model = "F-500"
                },
            };
            ProductCollection collection = new ProductCollection(array);

            Product product = collection[1];//индексация

            MyArr Point1 = new MyArr(5.0, 2.3);// использование перегрузки операторов
            MyArr Point2 = new MyArr(0.5, -3.0);
            MyArr Point3 = Point1 + Point2;
            Console.WriteLine("Сумма координат на карте: " + Point3.x + " " + string.Format("{0:0.00}", Point3.y));
        }
    }
}