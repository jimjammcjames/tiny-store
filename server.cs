// Hello World! program
using System;
using System.Collections.Generic;

class Server {   

    static void Main(string[] args)
    {
        DataBase.User james = new DataBase.User("123");

        DataBase.Item banana = new DataBase.Item("123", "banana", 2.25);
        james.addToCart(banana);
    }
}

namespace DataBase
{
    class User
    {
    class ShoppingCart
    {
        List<Item> item_list;

        public ShoppingCart()
        {
            item_list = new List<Item>();
        }
        public void addItem(Item item)
        {
            this.item_list.Add(item);
        }
    }   
        public string ID;
        ShoppingCart cart;
        
        public User(string ID)
        {
            this.ID = ID;  
            this.cart = new ShoppingCart();  
            Console.WriteLine("Created "+ ID);
        }

        public void addToCart(Item item)
        {
            this.cart.addItem(item);
            Console.WriteLine("added " + item.name);
        }

        

    }

    

    class Item
    {
        public string ID;
        public string name;
        public double price;

        public Item(string ID, string name, double price)
        {
            this.ID = ID;
            this.name = name;
            this.price = price;
        }
        
    }

}