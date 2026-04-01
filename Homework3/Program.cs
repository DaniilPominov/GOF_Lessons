namespace Homework3
{
    //магазин с системой скидок
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Декоратор ===\n");
            Store store = new BasicStore();
            store = new PromocodeDecorator(store, "PROMO10");
            Console.WriteLine($"Базовая цена с промокодом: {store.GetPrice()}\n");

            Console.WriteLine("=== Композит + Декоратор ===\n");

            // Создаём отдельные товары
            var book = new SimpleProduct("Книга", 500);
            var pen = new SimpleProduct("Ручка", 50);
            var notebook = new SimpleProduct("Тетрадь", 150);

            // Создаём набор товаров 
            var schoolBundle = new ProductBundle("Набор для школы");
            schoolBundle.AddProduct(book);
            schoolBundle.AddProduct(pen);
            schoolBundle.AddProduct(notebook);

            // Показываем содержимое набора
            schoolBundle.PrintContents();
            Console.WriteLine($"Итоговая цена набора: {schoolBundle.GetPrice()}\n");

            // Применяем систему скидок
            Store bundleStore = new ProductStore(schoolBundle);
            bundleStore = new DiscountDecorator(bundleStore, 0.1m);
            Console.WriteLine($"Цена с скидкой: {bundleStore.GetPrice()}\n");

            // Создаём вложенный набор
            var luxuryPen = new SimpleProduct("Люксовая ручка", 500);
            var premiumBundle = new ProductBundle("Премиум-набор");
            premiumBundle.AddProduct(schoolBundle);
            premiumBundle.AddProduct(luxuryPen);

            Console.WriteLine("=== Вложенный Композит ===\n");
            premiumBundle.PrintContents();
            Console.WriteLine($"Итоговая цена премиум-набора: {premiumBundle.GetPrice()}\n");

            // Применяем несколько скидок
            Store premiumStore = new ProductStore(premiumBundle);
            premiumStore = new DiscountDecorator(premiumStore, 0.15m);
            premiumStore = new SeasonalDiscountDecorator(premiumStore, 0.05m);
            Console.WriteLine("Применяем сезонную скидку+ основную");
            Console.WriteLine($"Итоговая цена: {premiumStore.GetPrice()}");
        }
    }
    public class BasicStore : Store
    {
        public override decimal GetPrice()
        {
            return base.GetPrice();
        }
    }
    public class StoreDecorator : Store
    {
        protected Store _store;
        public StoreDecorator(Store store)
        {
            _store = store;
        }
        public override decimal GetPrice()
        {
            return _store.GetPrice();
        }
    }
    public class DiscountDecorator : StoreDecorator
    {
        private decimal _discount;
        public DiscountDecorator(Store store, decimal discount) : base(store)
        {
            _discount = discount;
        }
        public override decimal GetPrice()
        {
            return base.GetPrice() * (1 - _discount);
        }
    }
    public class SeasonalDiscountDecorator : StoreDecorator
    {
        private decimal _seasonalDiscount;
        public SeasonalDiscountDecorator(Store store, decimal seasonalDiscount) : base(store)
        {
            //get dicount from season, etc...
            _seasonalDiscount = seasonalDiscount;
        }
        public override decimal GetPrice()
        {
            return base.GetPrice() * (1 - _seasonalDiscount);
        }
    }
    public class PromocodeDecorator : StoreDecorator
    {
        private decimal _promocodeDiscount;
        private string promocode;
        public PromocodeDecorator(Store store, string promocode) : base(store)
        {
            var discount = GetDiscountFromPromocode(promocode);
            _promocodeDiscount = discount;
        }
        private decimal GetDiscountFromPromocode(string promocode)
        {
            return 0.1m;
        }
        public override decimal GetPrice()
        {
            return base.GetPrice() * (1 - _promocodeDiscount);
        }
    }
}
