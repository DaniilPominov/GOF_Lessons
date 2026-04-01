using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework3
{
    // пример паттерна Композит - товары и наборы товаров
    public abstract class Product
    {
        public abstract decimal GetPrice();
        public abstract string GetName();
    }

    public class SimpleProduct : Product
    {
        private string _name;
        private decimal _price;

        public SimpleProduct(string name, decimal price)
        {
            _name = name;
            _price = price;
        }

        public override decimal GetPrice() => _price;
        public override string GetName() => _name;
    }

    // Композит
    public class ProductBundle : Product
    {
        private string _name;
        private List<Product> _products = new();

        public ProductBundle(string name)
        {
            _name = name;
        }

        public void AddProduct(Product product) => _products.Add(product);
        public void RemoveProduct(Product product) => _products.Remove(product);

        public override decimal GetPrice() => _products.Sum(p => p.GetPrice());
        public override string GetName() => _name;

        public void PrintContents()
        {
            Console.WriteLine($"Набор: {_name}");
            foreach (var product in _products)
            {
                Console.WriteLine($"  - {product.GetName()}: {product.GetPrice()}");
            }
        }
    }

    // Адаптер для интеграции Композита с системой скидок Декоратора
    public class ProductStore : Store
    {
        private Product _product;

        public ProductStore(Product product)
        {
            _product = product;
        }

        public override decimal GetPrice() => _product.GetPrice();
        public override string GetProductInfo() => _product.GetName();
    }
}
