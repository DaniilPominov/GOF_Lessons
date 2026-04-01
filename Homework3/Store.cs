namespace Homework3
{
    public abstract class Store
    {
        public virtual decimal GetPrice()
        {
            return 100; // базовая цена
        }

        public virtual string GetProductInfo()
        {
            return "Стандартный продукт";
        }
    }
}
