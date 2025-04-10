namespace RepositoryPattern
{
    public interface IRepository<T> where T : Entity
    {
        List<T> GetShowRoomInfo();
        void Add(T t);
        void Remove(T t);
        void Update(T t);
    }

    public class Entity
    {
        public int Id { get; internal set; }
    }

    public class Car : Entity
    {
        public string? Make { get; set; }
        public int Year { get; set; }
    }

    public class InMemoryRepository<T> : IRepository<T> where T : Entity
    {
        private readonly List<T> _items = new List<T>();
        private int _nextId = 1;

        public List<T> GetShowRoomInfo()
        {
            return _items;
        }

        public void Add(T t)
        {
            t.Id = _nextId++;
            _items.Add(t);
        }

        public void Remove(T t)
        {
            _items.Remove(t);
        }

        public void Update(T t)
        {
            var index = _items.FindIndex(i => i.Id == t.Id);
            if (index != -1)
            {
                _items[index] = t;
            }
        }
    }

    public class ShowRoom
    {
        private readonly IRepository<Car> _repo;

        public ShowRoom(IRepository<Car> repo)
        {
            _repo = repo;
        }

        public void AddCar(string make, int year)
        {
            Car car = new Car
            {
                Make = make,
                Year = year
            };
            _repo.Add(car);
        }

        public void ShowCars()
        {
            foreach (var car in _repo.GetShowRoomInfo())
            {
                Console.WriteLine($"ID: {car.Id} | Make: {car.Make} | Year: {car.Year}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            var repo = new InMemoryRepository<Car>();
            var showRoom = new ShowRoom(repo);

            showRoom.AddCar("Toyota", 2020);
            showRoom.AddCar("Honda", 2022);
            showRoom.AddCar("Ford", 2019);

            Console.WriteLine("Showroom Inventory:");
            showRoom.ShowCars();
        }
    }
}