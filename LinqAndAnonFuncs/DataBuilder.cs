using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAndAnonFuncs
{
    /// <summary>
    /// This class is responsible for creating mock datasets
    /// </summary>
    public class DataBuilder 
    {
        Random randomGenerator = new Random();

        /// <summary>
        /// Our current lineup of cars
        /// </summary>
        public List<Car> Cars { get; set; } = new List<Car>();

        /// <summary>
        /// People who are looking for a car
        /// </summary>
        public List<Buyer> Buyers { get; set; } = new List<Buyer>
        {
            new Buyer
            {
                FirstName = "Tyler",
                LastName =  "Busse",
                HasCarInsurance = true,
                PreferredMake = "Ford"
            },
            new Buyer
            {
                FirstName = "John",
                LastName =  "Johnson",
                HasCarInsurance = false,
                PreferredMake = "Tesla"
            },
            new Buyer
            {
                FirstName = "Sigurd",
                LastName =  "Sigurdsson",
                HasCarInsurance = true,
                PreferredMake = "Saab"
            },
            new Buyer
            {
                FirstName = "Jerry",
                LastName =  "Haynes",
                HasCarInsurance = true,
                PreferredMake = "Tesla"
            },
            new Buyer
            {
                FirstName = "Fiorenza",
                LastName =  "Offredi",
                HasCarInsurance = true,
                PreferredMake = "Ferrari"
            }
        };

        /// <summary>
        /// Randomly generates different cars from <see cref="Car"/>
        /// </summary>
        /// <param name="iterations"></param>
        public void RandomlyGenerateCars(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                int category = randomGenerator.Next(0, 4);
                switch (category)
                {
                    case 0:
                        Cars.Add(new SportsCar());
                        break;
                    case 1:
                        Cars.Add(new Truck());
                        break;
                    case 2:
                        Cars.Add(new Hatchback());
                        break;
                    case 3:
                        Cars.Add(new ElectricCar());
                        break;
                    default:
                        Cars.Add(new Hatchback());
                        break;
                }
            }
        }

        /// <summary>
        /// Randomly Assigns Milage to Cars
        /// </summary>
        public void RandomlyAssignMileage()
        {
            foreach (Car car in Cars)
            {
                car.Mileage = randomGenerator.Next(0, 150000);
            }
        }

        /// <summary>
        /// Randomly Assigns Ownership to Cars
        /// </summary>
        public void RandomlyAssignOwned()
        {
            foreach (Car car in Cars)
            {
                car.Owned = Convert.ToBoolean(randomGenerator.Next(0, 2));
            }
        }

    }   

    /// <summary>
    /// Vroom Vroom go fast
    /// </summary>
    public class SportsCar : Car
    {
        // lambda operator (=>) here represents a lambda statement, not an expression
        public override string Make => "Ferrari";

        public override string Model => "SF90 Stradale";

        public override DateTimeOffset ModelRelease => new DateTime(2019,1,1);

        public override int EngineSize => 4;

        public override int Doors => 2;

        public override void Describe()
        {
            Console.WriteLine($"I am a fast {(Owned ? "owned" : "unowned")} {ModelRelease.Year} {Make} {Model} sports car with {Mileage} miles ");
        }

        /// <summary>
        /// Press the pedal twice
        /// </summary>
        public string Rev() => "VROOM VROOM";

    }

    /// <summary>
    /// Pickup Truck
    /// </summary>
    public class Truck : Car
    {
        public override string Make => "Ford";

        public override string Model => "F150";

        public override DateTimeOffset ModelRelease => new DateTime(2015, 1, 1);

        public override int EngineSize => 6;

        public override int Doors => 4;
        public override void Describe()
        {
            Console.WriteLine($"I am a big {(Owned ? "owned" : "unowned")} {ModelRelease.Year} {Make} {Model} truck with {Mileage} miles");
        }
    }

    /// <summary>
    /// 5 Door Hatchback Car
    /// </summary>
    public class Hatchback : Car
    {
        public override string Make => "Ford";

        public override string Model => "Focus";

        public override DateTimeOffset ModelRelease => new DateTime(2016, 1, 1);

        public override int EngineSize => 4;

        public override int Doors => 5;

        public override void Describe()
        {
            Console.WriteLine($"I am a slow and ordinary {(Owned ? "owned" : "unowned")} {ModelRelease.Year} {Make} {Model} sedan with {Mileage} miles");
        }
    }

    class ElectricCar : Car
    {
        public override string Make => "Tesla";

        public override string Model => "Model S";

        public override DateTimeOffset ModelRelease => new DateTime(2018, 1, 1);

        public override int Doors => 4;

        public override int EngineSize => 0;

        public override void Describe()
        {
            Console.WriteLine($"I am a quiet {(Owned ? "owned" : "unowned")} {ModelRelease.Year} {Make} {Model} electric car with {Mileage} miles");
        }
    }

    /// <summary>
    /// Base class for vehicles that are powered by an engine and have (at least) 4 wheels, roofs optional
    /// </summary>
    public abstract class Car : ICar
    {

        public abstract string Make { get; }

        public abstract string Model { get; }

        public abstract DateTimeOffset ModelRelease { get; }

        public abstract int EngineSize { get; }

        public int Mileage { get; set; }

        public bool Owned { get; set; }

        public abstract int Doors { get; }

        public Guid Guid { get; } = new Guid();

        public virtual void Describe()
        {
            Console.WriteLine($"I am an {(Owned ? "owned" : "unowned")} {ModelRelease.Year} {Make} {Model} car with {Mileage} miles");
        }
    }

    public interface ICar
    {
        /// <summary>
        /// Who Makes this car
        /// </summary>
        string Make { get; }
        /// <summary>
        /// What model type is the car
        /// </summary>
        string Model { get; }
        /// <summary>
        /// When was the car made
        /// </summary>
        DateTimeOffset ModelRelease { get; }
        /// <summary>
        /// How many cylinders does the car have
        /// </summary>
        int EngineSize { get; }
        /// <summary>
        /// What is this car's mileage
        /// </summary>
        int Mileage { get; set; }
        /// <summary>
        /// Is this car owned?
        /// </summary>
        bool Owned { get; set; }
        /// <summary>
        /// How many doors does this car have
        /// </summary>
        int Doors { get; }
        /// <summary>
        /// Unique Idenifier - VIN equivalent
        /// </summary>
        Guid Guid { get; }
        /// <summary>
        /// Describle the car to the console
        /// </summary>
        void Describe();
    }

    /// <summary>
    /// Who wants to buy a car
    /// </summary>
    public class Buyer
    {
        public string FirstName { get; set; } = "Person";
        public string LastName { get; set; } = "McMensch";
        public bool HasCarInsurance { get; set; } = true;
        public string PreferredMake { get; set; } = "Toyota";
    }
}
