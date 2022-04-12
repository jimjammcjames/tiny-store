// Hello World! program
using System;
using System.Collections.Generic;

static class Server {   
    static DataBase dataBase = new DataBase();
    private static Random random = new Random();
    public static void Main()
    {
        User james = createUser("james");
        Item banana = createItem("banana", 2.50);
        james.addToCart(banana);
        // james.getCart();
        dataBase.printInfo();
    }
    static void populate()
    {
        
    }
    static string generateKey()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[8];

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new String(stringChars);
    }
    static User createUser(string name)
    {
        string id = generateKey();
        User newUser = new User(id, name);
        dataBase.accounts.Add(id, newUser);
        return newUser;
    }
    static Item createItem(string name, double price)
    {
        string id = generateKey();
        Item newItem = new Item(id, name, price);
        dataBase.catalogue.Add(id, newItem);
        return newItem;
    }
}

class DataBase
{
    public Dictionary<string, Item> catalogue;
    public Dictionary<string, User> accounts;
    public DataBase()
    {
        catalogue = new Dictionary<string, Item>();
        accounts = new Dictionary<string, User>();
    }

    public void printInfo(){
        Console.WriteLine("---accounts in dataBase---");
        foreach (var account in this.accounts)
        {
            Console.WriteLine(account.Key + " " + account.Value.name);
        }
        Console.WriteLine("---catalogue---");
        foreach (var item in this.catalogue)
        {
            Console.WriteLine(item.Key + " " + item.Value.name);
        }
    }
}
class User
{
    public string ID;
    public string name;
    List<Item> cart;
    
    public User(string ID, string name)
    {
        this.name = name;
        this.ID = ID;  
        this.cart = new List<Item>();  
        Console.WriteLine("Created "+ name);
    }

    public void addToCart(Item item)
    {
        this.cart.Add(item);
        Console.WriteLine("added " + item.name);
    }

    public void removeFromCart(Item item)
    {
        this.cart.Remove(item);
        Console.WriteLine("removed" + item.name);
    }

    public List<Item> getCart()
    {
        Console.WriteLine("-----items in cart: -----");
        foreach (var item in this.cart)
        {
           Console.WriteLine(item.name); 
        }
        Console.WriteLine("---------------");
        return this.cart;
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

