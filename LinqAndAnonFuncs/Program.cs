using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAndAnonFuncs
{
    class Program
    {
        static void Main(string[] args)
        {
            ////// Lambda expressions
            // Essentially a form of anonymous method. Can be type casted to Actions / Funcs Delagates

            // An anonymous method is a method that does not need to be defined and is typically disposed of once it has completed execution.
            // A lambda expression is a type of anonymous method that does not require the type to be explicitly defined
            // Lambda expressions are written as follows
            // (input-parameters) => expression
            // One or more inputs may be specified
            // The lambda declaration operator (=>) is what defines an operation as lambda
            // There are also lambda statements, which you can find examples of in the DataBuilder class as a part of the project. Lambda statements are unrelated to LINQ.


            // Lambda without a return - Action Delagate
            Action action = () => Console.WriteLine("Heres a test action");
            action.Invoke();
            // Lambda without a return and has an input - Action Delagate
            Action<string> writeMyLine = line => Console.WriteLine(line);
            writeMyLine.Invoke("some line");

            // Lambda with a return type - Func Delagate
            Func<int, int> square = x => x * x;
            Console.WriteLine(square(5)); // 25

            // Lambda with multiple inputs and a return type - Func Delagate
            Func<int, string, string> squareWithString = (toSquare, preamble) => preamble + (toSquare * toSquare).ToString();
            Console.WriteLine(squareWithString(5, "The square of 5 is ")); // The square of 5 is 25


            // For the purposes of LINQ, we will use lambda expressions to help us to manipulate data returned by LINQ methods. Most LINQ methods take an Action / Func as the method parameter
            // When using LINQ queries, the lambda is done in the background for you. LINQ stands for "Language - Integrated Query"


            /////// LINQ Queries

            // A LINQ Query consists of a few parts, take the follwing example

            //   IEnumerable<T1> output = from RangeVariable<T2> in DataSource : IEnumerable<T2>
            //                            (data filtering / transformation )
            //                            select RangeVariable<T1>

            // Step 1, specify the data source. In this case, myInts is the data source.
            //         Only objects who inherit IEnumerable or IQueryable will be able to be a datasource in a LINQ query.

            // Step 2, Identify a range variable. In this case I used integer to describe the range variable
            //         This is similar to how in a foreach loop, you specify the variable to be used in each iteration (int integer in myInts) but no type delcaration needs to occur
            //         The type is inferred from the datasource (in this case List<int>)

            // Step 3, Filter / Sort / Order / Join / the range variable. This will filter and manipulate the data prior to output. This is not necessary to complete the query

            // Step 4, Produce the results of the query. Depending on the type of keyword used (select, group, ect) the result is returned based on the the type.
            //         In the example below "select integer" returns IEnumerable<int> since integer is of type int. 
            //         However, if we changed the result to "select integer.ToString()" the return type would be of IEnumerable<string>

            // Best practices using LINQ include heavy usage of "var" to implicitly set the returns. Intellisense will let you know the type of the range variable, along with any other types
            // detailed within the query with a quick hover-over. If you do choose to explicitly define the type of return, Intellisense and the compiler will alert you to any errors


            List<int> myInts = new List<int>
            {
                0,3,4,12,34,156,3,34,2,34
            };

            // Type of IEnumerable<int>
            var integers = from integer in myInts
                           select integer;

            var intList = integers.ToList();

            /////// LINQ Methods

            // You can also use many of LINQs fuctionality in a more method based approach. The following example outputs the exact same data but written in another way.
            // In this way we are defining the data source as myInts and using the Select exension to select a sequence. 
            // The Select() method takes a Func<int, TResult> as a parameter, given the data source of IEnumerable<int>
            // Using a lambda expression, the Func is defined as "integer => integer". This uses an <int> as the input variable and returns type <int>.
            // As above, this operation returns IEnumerable<int> but if we changed the lambda expression to integer => integer.ToString(), it would return IEnumerable<string>.
            
            var integersMethodBased = myInts.Select(integer => integer);

            var intMethodList = integersMethodBased.ToList();


            // Why would we ever want to use LINQ? The tried and true way of forlooping over a collection, cleaning, filtering, sorting works just fine!
            // Some of the pros:
            // LINQ syntatically is not very complex, which enhances readiblilty of the code         
            // For trivial transformations / filter / sort operations, LINQ requires far less code than tradition methods
            // LINQ can be used to query multiple data sources against each other easily
            // The compiler type checks the objects and syntax in the query (when interacting with a database a string for SQL does not)
            // IntelliSense support for all generic collections

            // Some of the downsides:
            // For really complex data manipulation, LINQ gets a little hairy
            // There is no step by step debugging LINQ Methods or Queries, which can make testing and exception hunting harder than it needs to be
            // Since all of the methods are extensions, anonymous, there is no code reuse (unless nested inside of a method).

            // LINQ is just another tool in the toolbox 
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/basic-linq-query-operations for more in depth documentation


            /////// EXAMPLES

            // Examples using Cars & Buyers found in DataBuilder
            DataBuilder dataBuilder = new DataBuilder();


            // Randomly Generate X amount of Car objects of varying types
            dataBuilder.RandomlyGenerateCars(40);

            /////// A simple selection

            // Foreach Method - Non LINQ
            List<Car> theCarsForEach = new List<Car>();
            foreach(Car car in dataBuilder.Cars)
            {
                theCarsForEach.Add(car);
            }

            // Linq Query Method
            IEnumerable<Car> theCarsQuery = from car in dataBuilder.Cars
                                             select car;

            // Linq Method Call                                        // Lambda expression
            IEnumerable<Car> theCarsMethodCall = dataBuilder.Cars.Select(car => car);

            //List Conversions
            List<Car> theCarsQueryList = theCarsQuery.ToList();
            List<Car> theCarsMethodCallList = theCarsMethodCall.ToList();





            ////// Filtering
            dataBuilder.RandomlyAssignOwned();

            // Foreach Method
            List<Car> theOwnedCarsForEach = new List<Car>();
            foreach(Car car in dataBuilder.Cars)
            {
                if (car.Owned)
                {
                    theOwnedCarsForEach.Add(car);
                }
            }

            // Query
            var theOwnedCarsQuery = from car in dataBuilder.Cars
                                    where car.Owned
                                    select car;

            // Method

            var theOwnedCarsMethod = dataBuilder.Cars.Where(car => car.Owned).Select(car => car).ToList();



            //////// Multi level filtering
            // Foreach Method
            dataBuilder.RandomlyAssignMileage();
            List<Car> theOwnedCarsAndMileageForEach = new List<Car>();
            foreach (Car car in dataBuilder.Cars)
            {
                if (car.Owned && car.Mileage > 50000)
                {
                    theOwnedCarsAndMileageForEach.Add(car);
                }
            }

            // Query
            var theOwnedCarsAndMileageQuery = from car in dataBuilder.Cars
                                              where car.Owned && car.Mileage > 50000
                                              select car;

            // Method

            var theOwnedCarsAndMileageMethod = dataBuilder.Cars.Where(car => car.Owned && car.Mileage > 50000)
                                                               .Select(car => car)
                                                               .ToList();



            //////// Ordering
            var orderedByMilageDesc = from car in dataBuilder.Cars
                                      orderby car.Mileage descending
                                      select car;

            var orderbyMilageMethod = dataBuilder.Cars.OrderByDescending(car => car.Mileage);



            //////// Grouping

            // Group by Make
            var groupByMake = from car in dataBuilder.Cars
                              group car by car.Make;

            foreach(IGrouping<string, Car> group in groupByMake)
            {
                Console.WriteLine($"There are {group.Count()} {group.Key}s");
            }


            // Method Calls
            var groupByMakeMethod = dataBuilder.Cars.GroupBy(car => car.Make);





            // Group by object type
            var groupByType = from car in dataBuilder.Cars
                              group car by car.GetType();

            // Method Calls
            var groupByTypeMethod = dataBuilder.Cars.GroupBy(car => car.GetType());

            foreach(IGrouping<Type, Car> group in groupByType)
            {
                Console.WriteLine($"This group contains {group.Count()} {group.Key}s");
            }


            // Transforming objects
            IEnumerable<SportsCar> findSportsCars = from car in dataBuilder.Cars
                                                    where car.GetType() == typeof(SportsCar)
                                                    select car as SportsCar;

            Console.WriteLine(findSportsCars.FirstOrDefault().Rev());


            // Method Calls and ForEach


            // One of the most useful ways of using LINQ is to use the ForEach exension to preform generic transformations instead of breaking things out to a new block that will be used once. 
            // Here's a few examples

            List<int> someInts = new List<int>();
            myInts.ForEach(theInt => someInts.Add(theInt));

            // Call methods on collections
            dataBuilder.Cars.ForEach(car => car.Describe());

            // Assign large amounts of data
            dataBuilder.Cars.ForEach(car => car.Owned = false);


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            // Call methods on selections of collections
            dataBuilder.Cars.Where(car => car.Doors > 2)
                            .ToList() // returns IEnumerable and in order to access .ForEach the type must be list
                            .ForEach(biggerCar => biggerCar.Describe());



            // Navigating dictionaries
            var exampleDictionary = new Dictionary<int, string>
            {
                {1, "one" },
                {2, "two" },
                {3, "three" },
                {15998946, "Fifteen million, nine hundred ninety eight thousand, nine hundred and fourty six" }
            };


            // Maths

            int sumOfLengthsGreaterThanTwo = exampleDictionary.Where(kvp => kvp.Key > 2).Sum(kvp => kvp.Value.Length);
            double averageGreaterThanOrEqualToThree = exampleDictionary.Keys.Where(intKey => intKey >= 3).Average(intKey => intKey);
            int maxNumber = exampleDictionary.Keys.Max(numbers => numbers);
            int shortestText = exampleDictionary.Min(text => text.Value.Length);


            /////// Joins
            /// Joins allow the developer to take 2 (or more) different objects and join them together into a mixed collection as long as the join occurs on an accessible type for both objects
            /// and both properties types implement IEquatable
            /// In the following examples, strings are equality compared against each other to make the join occur
            /// The return type of 'select new { Property = value, ...}' is an anonymous type. These types do not require a definition (like anonymous methods) and allow for easy
            /// One time use data wrangling when combining multiple classes 

            dataBuilder.RandomlyAssignOwned();

            /// This exmaple matches each buyer to their favorite cars
            /// Returns anonymous IEnumerable<'a(Buyer, IEnumerable<Car>)>
            var buyersToCars = from buyer in dataBuilder.Buyers
                               join car in dataBuilder.Cars on buyer.PreferredMake equals car.Make into favoriteCars
                               select new { Buyer = buyer, Cars = favoriteCars };


            /// Which of these is easier to read???
            buyersToCars.ToList().ForEach(buyerCarGroup => buyerCarGroup.Cars.ToList()
                                 .ForEach(car => Console.WriteLine($"{buyerCarGroup.Buyer.FirstName} {buyerCarGroup.Buyer.LastName} who likes {buyerCarGroup.Buyer.PreferredMake} matches with " +
                                 $"a {car.ModelRelease.Year} {car.Make} {car.Model} with {car.Mileage} miles")));


            foreach (var CarBuyerGroup in buyersToCars)
            {
                Console.WriteLine($"\nBuyer: {CarBuyerGroup.Buyer.FirstName} {CarBuyerGroup.Buyer.LastName} likes {CarBuyerGroup.Buyer.PreferredMake}");
                foreach(var car in CarBuyerGroup.Cars)
                {
                    car.Describe();
                }
            }


            //////// Sub queries with join operation, anonymous types output
            /// Lets pretend that we are a car dealership with the collection in DataBuilder containing cars we have in our database for all vehicles bought and sold.
            /// In the preceding example, we had potential buyers who didn't have car insurance and we paired them with cars that were already owned; not an ideal representation of what we could sell
            /// In this next example, we can filter out buyers to who are eligible and match them with cars they can actually own, ordered by least mileage to most, giving a more personalized marketing stragegy
            var goodBuyersToUnownedCars = from buyer in dataBuilder.Buyers
                                          where buyer.HasCarInsurance
                                          join car in dataBuilder.Cars on buyer.PreferredMake equals car.Make into favoriteCars
                                          select new { GoodBuyer = buyer ,
                                                       ProspectiveCars = from favoriteCar in favoriteCars
                                                                         where !favoriteCar.Owned
                                                                         orderby favoriteCar.Mileage ascending
                                                                         select favoriteCar
                                                      };

            foreach (var BuyerCarCombo in goodBuyersToUnownedCars)
            {
                Console.WriteLine($"\nQualified Buyer {BuyerCarCombo.GoodBuyer.FirstName} {BuyerCarCombo.GoodBuyer.LastName}");
                if(BuyerCarCombo.ProspectiveCars.ToList().Count > 0)
                {
                    foreach(var car in BuyerCarCombo.ProspectiveCars)
                    {
                        car.Describe();
                    }
                }
                else
                {
                    Console.WriteLine($"There are no {BuyerCarCombo.GoodBuyer.PreferredMake} cars for sale!");
                }
            }
            Console.ReadLine();
        }
    }
}
