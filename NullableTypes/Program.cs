using System;
using System.Collections.Generic;

namespace NullableTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Null Conditional, Null Coalesing Operators & Nullable types

            /// Nullable types
            /// Any value type can be cast as a nullable type as such. T? where T is the underlying value type
            /// Additional documentation can be found here https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types

            bool? nullableBool = null;
            nullableBool = true;
            nullableBool = null;

            int? nullableInt = 3;
            nullableInt = null;


            int?[] array = new int?[10];
            
            /// When using nullable values, it is important to check if the nullable type is actually null before attempting to access it (thus resulting in a NullReferenceException)
            /// This can be accomplished in a few different ways
            /// 

            if (nullableInt == null)
            {
                Console.WriteLine("nullableInt is null!");
            }

            if (nullableInt.HasValue)
            {
                Console.WriteLine(nullableInt.Value);
            }

            // If you just have to have a value except for null there are a few different ways to define this

            int regularInt;
            if (!nullableInt.HasValue)
            {
                // Whatever you want to put in
                regularInt = 0;

            }
            else
            {
                regularInt = nullableInt.Value;
            }

            Console.WriteLine($"{regularInt}");

            // Returns the value or if null the default value (depending on the underlying type). For type int the default value is 0. Check with documentation for others.
            // GetValueOrDefault returns the underlying type
            regularInt = nullableInt.GetValueOrDefault();

            Console.WriteLine($"nullableInt.GetValueOrDefault(): {regularInt}");


            // Use a null coalesing operator instead
            // Null coalesing operators work like a ternary operator "?:" where the if the operand to the left of the ?? is not null that value will be returned. 
            // If the operator left of the operand IS null, the value to the right will be returned
            nullableInt = null;
            Console.WriteLine($"{nullableInt ?? 4}");

            // Multiple null coaleseing operators can be used in the same expression
            // The are right associative

            int? a = null;
            int? b = null;
            int? c = null;


            // These evaluate the same
            _ = a ?? b ?? c;
            _ = a ?? (b ?? c);

            // Only available in C# 8 is the null coalescing assigment operator ??=. This works similarly to the null coalesing operator but it will assign the value to the left operand
            // The rules on multiple operators are the same for ??
            nullableInt ??= 4;

            // In C# 7 you can do these instead
            nullableInt = nullableInt ?? 4;

            if (!nullableInt.HasValue)
            {
                nullableInt = 4;
            }



            /// Null conditional operators
            ///
            /// Developers can use null conditional operators as a sort of null check to run before accessing data on an object
            /// More information on this subject here https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-

            List<int?> list = null;
            
            // Since the list has not been instanciated yet, these expressions will not evaluate

            // Using a ? in front of the assessor "." this expression will not take place
            list?.Add(3);
            
            // Index accessors such as [] in lists, arrays, ect also can use ? as a null check before
            int? value = list?[0];



            Console.WriteLine("Hello World!");
        }
    }
}
