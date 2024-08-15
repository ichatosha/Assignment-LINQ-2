using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace LINQ_2
{
    class Program
    {
        static void Main()
        {


            #region Aggregate Functions

            Product ProductList = new Product();
            // 1. Get the total units in stock for each product category:
            var totalUnitsInStockByCategory = ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalUnitsInStock = g.Sum(p => p.UnitsInStock)
                });
            Console.WriteLine(totalUnitsInStockByCategory);

            // 2. Get the cheapest price among each category's products:
            var cheapestPriceByCategory = ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, CheapestPrice = g.Min(p => p.Price) });


            // 3. Get the products with the cheapest price in each category (Use Let):
            var cheapestProductsByCategory = ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    CheapestProducts = g.Where(p => p.Price == g.Min(p => p.Price))
                });

            //4. Get the most expensive price among each category's products:
            var mostExpensivePriceByCategory = productsList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, MostExpensivePrice = g.Max(p => p.Price) });

            //5. Get the products with the most expensive price in each category:
            var mostExpensiveProductsByCategory = productsList
                .GroupBy(p => p.Category)
                .Select(g => new {
                    Category = g.Key,
                    MostExpensiveProducts = g.Where(p => p.Price == g.Max(p => p.Price))
                });

            //6.Get the average price of each category's products:
            var averagePriceByCategory = productsList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, AveragePrice = g.Average(p => p.Price) });


            #endregion

            #region Set Operator
            //1.Find the unique Category names from Product List:
            var uniqueCategories = productsList.Select(p => p.Category).Distinct();

            // 2. Produce a Sequence containing the unique first letter from both product and customer names:
            var uniqueFirstLetters = productsList.Select(p => p.Name[0])
                    .Union(customers.Select(c => c.Name[0])).Distinct();

            // 3. Create one sequence that contains the common first letter from both product and customer names:
            var commonFirstLetters = productsList.Select(p => p.Name[0])
                    .Intersect(customers.Select(c => c.Name[0]));

            //4. Create one sequence that contains the first letters of product names that are not also first letters of customer names:
            var productOnlyFirstLetters = productsList.Select(p => p.Name[0])
                    .Except(customers.Select(c => c.Name[0]));

            //5. Create one sequence that contains the last Three Characters in each name of all customers and products, including any duplicates:
            var lastThreeCharacters = productsList.Select(p => p.Name.Substring(p.Name.Length - 3))
                    .Concat(customers.Select(c => c.Name.Substring(c.Name.Length - 3)));


            #endregion

            #region  Partitioning Operators

            //1. Get the first 3 orders from customers in Washington:
            var firstThreeOrders = customersList
                .Where(c => c.State == "WA")
                .SelectMany(c => c.Orders)
                .Take(3);

            //2. Get all but the first 2 orders from customers in Washington:
            var allButFirstTwoOrders = customersList
                .Where(c => c.State == "WA")
                .SelectMany(c => c.Orders)
                .Skip(2);

            //3. Return elements starting from the beginning of the array until a number is hit that is less than its position in the array:
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var elements = numbers.TakeWhile((n, i) => n >= i);


            //4. Get the elements of the array starting from the first element divisible by 3:
            var elementsDivisibleBy3 = numbers.SkipWhile(n => n % 3 != 0);

            //5. Get the elements of the array starting from the first element less than its position:
            var elementsLessThanPosition = numbers.SkipWhile((n, i) => n >= i);


            #endregion


            #region Quantifiers
            //1. Determine if any of the words in dictionary_english.txt contain the substring 'ei':

            var containsEi = dictionaryWords.Any(word => word.Contains("ei"));

            //2. Return a grouped list of products only for categories that have at least one product that is out of stock:
            var categoriesWithOutOfStockProducts = productsList
                .GroupBy(p => p.Category)
                .Where(g => g.Any(p => p.UnitsInStock == 0));

            //3. Return a grouped list of products only for categories that have all of their products in stock:

            var categoriesWithAllInStock = productsList
                .GroupBy(p => p.Category)
                .Where(g => g.All(p => p.UnitsInStock > 0));

            #endregion


            #region Grouping Operators 

            //1. Use group by to partition a list of numbers by their remainder when divided by 5:
            var groupedByRemainder = numbers.GroupBy(n => n % 5);

            //2.Use group by to partition a list of words by their first letter using dictionary_english.txt:

            var groupedByFirstLetter = dictionaryWords.GroupBy(word => word[0]);

            //3. Use Group By with a custom comparer that matches words that consist of the same characters together:
            var groupedByAnagram = Arr.GroupBy(
                    word => String.Concat(word.OrderBy(c => c)),
                    (key, words) => new { AnagramKey = key, Words = words });

            #endregion


        }
    }
}