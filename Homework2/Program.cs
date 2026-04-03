namespace Homework2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dogFactory = new DogFactory();
            var catFactory = new CatFactory();

            var dogClient = new Client(dogFactory);
            var catClient = new Client(catFactory);

            catClient.Action();
            dogClient.Action();

            Console.WriteLine($"{catClient.GetType().Equals(dogClient.GetType())}");
        }
    }

    //пример абстрактной фабрики
    public interface IAnimal
    {
        void Speak();
    }
    public class Dog : IAnimal
    {
        public void Speak()
        {
            Console.WriteLine("Woof!");
        }
    }
    public class Cat : IAnimal
    {
        public void Speak()
        {
            Console.WriteLine("Meow!");
        }
    }
    public interface IAnimalFactory
    {
        IAnimal CreateAnimal();
    }
    public class DogFactory : IAnimalFactory
    {
        public IAnimal CreateAnimal()
        {
            return new Dog();
        }
    }
    public class CatFactory : IAnimalFactory
    {
        public IAnimal CreateAnimal()
        {
            return new Cat();
        }
    }

    public class Client
    {
        private readonly IAnimalFactory _animalFactory;
        public Client(IAnimalFactory animalFactory)
        {
            _animalFactory = animalFactory;
        }
        public void Action()
        {
            var animal = _animalFactory.CreateAnimal();
            animal.Speak();
        }
    }



    //пример прототипа
    public interface IPrototype

    {
        public int Id { get; set; }
        public string Name { get; set; }

        IPrototype Clone();
    }
    public class ConcretePrototype : IPrototype
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IPrototype Clone()
        {
            return new ConcretePrototype
            {
                Id = this.Id,
                Name = this.Name
            };
        }
    }
    public class PrototypeClient
    {
        public void Run()
        {
            var original = new ConcretePrototype { Id = 1, Name = "Original" };
            var clone = original.Clone();
            Console.WriteLine($"Original: Id={original.Id}, Name={original.Name}");
            Console.WriteLine($"Clone: Id={clone.Id}, Name={clone.Name}");
        }
    }
}
