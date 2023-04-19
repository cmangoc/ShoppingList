using System.Collections.Generic;
namespace ShoppingList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool goOn = true;
            decimal total = 0;
            List<string> cart = new List<string>();
            Dictionary<string, decimal> fruitPrice= new Dictionary<string, decimal>();
            fruitPrice.Add("apple", 0.99m);
            fruitPrice.Add("banana", 0.59m);
            fruitPrice.Add("cantaloupe", 1.59m);
            fruitPrice.Add("dragonfruit", 2.19m);
            fruitPrice.Add("elderberry", 1.79m);
            fruitPrice.Add("figs", 2.09m);
            fruitPrice.Add("grapefruit", 1.99m);
            fruitPrice.Add("honeydew", 3.49m);
            
            //adds user inputs to the cart
            do
            {
                DisplayItems(fruitPrice);
                AddItemToCart(fruitPrice, cart);
                goOn = Continue();
            } while (goOn);
            
            //old way of sorting, used nested loops instead of linq (yuck)
            /*List<decimal> prices = new List<decimal>();
            foreach (string item in cart)
            {
                prices.Add(fruitPrice[item]);
            }
            List<decimal> ordered = new List<decimal>();
            for(int i = 0; i < items; i++)
            {
                ordered.Add(prices.Max());
                prices.Remove(prices.Max());
            }
            string[] orderedCart = new string[items];
            for(int i = 0; i < items; i++)
            {
                for(int j = 0; j <items; j++)
                if (ordered[j] == fruitPrice[cart[i]])
                {
                    orderedCart[j] = cart[i];
                }
            }*/

            //sorts by values
            int items = cart.Count;
            List<string> ordered = cart.OrderByDescending(item => fruitPrice[item]).ToList();

            Console.WriteLine("Thanks for your order!");
            Console.WriteLine("Here's what you got: ");
            for(int i= 0; i < ordered.Count; i++)
            {
                Console.WriteLine($"{ordered[i]}    \t${fruitPrice[ordered[i]]}");
                total += fruitPrice[ordered[i]];
            }
            Console.WriteLine($"Your total is ${total}");
            Console.WriteLine($"Your most expensive item was: {ordered[0]} and cost {fruitPrice[ordered[0]]}");
            Console.WriteLine($"Your least expensive item was: {ordered[items-1]} and cost {fruitPrice[ordered[items-1]]}");
        }

        static void DisplayItems(Dictionary<string, decimal > dict)
        {
            Console.WriteLine("Welcome to Chirpus Market!");
            Console.WriteLine();
            Console.WriteLine("Item\t\tPrice");
            Console.WriteLine("==============================");
            foreach(KeyValuePair<string, decimal> kvp in dict)
            {
                Console.WriteLine($"{kvp.Key}    \t${kvp.Value}");
            }
            Console.WriteLine();
        }
        static void AddItemToCart(Dictionary<string, decimal> dict, List<string> cart)
        {
            Console.WriteLine("What item would you like to order?");
            string input = Console.ReadLine().Trim().ToLower();
            foreach (KeyValuePair<string, decimal> kvp in dict)
            {
                
                if(input == kvp.Key)
                {
                    Console.WriteLine($"Adding {kvp.Key} to cart at ${kvp.Value}");
                    cart.Add(kvp.Key);
                    return;
                }
            }
            Console.WriteLine("Sorry, we don't have those. Please try again.");
            AddItemToCart(dict, cart);
        }
        static bool Continue()
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to continue? y/n");
            string input = Console.ReadLine().ToLower().Trim();
            if (input == "y")
            {
                return true;
            }
            else if (input == "n")
            {
                return false;
            }
            else
            {
                return Continue();
            }
        }
    }
}