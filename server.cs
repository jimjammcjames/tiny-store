// deploy on kubernetes
using System;
using System.Collections.Generic;

static class Server {   
    static Database database = new Database();
    private static Random random = new Random();
    public static void Main()
    {
        populate();
        // createUser("james");
        // database.printInfo();
        // List<KeyValuePair<string, Item>> results = database.searchCatalogue("shoe");
        
        var endpoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any ,8080);
        var listener = new System.Net.Sockets.TcpListener(endpoint);
        listener.Start();
        Console.WriteLine("listening on stream");
        var client = listener.AcceptTcpClient();
        var stream = client.GetStream();
        listenOnStream(stream);
    }
    static void listenOnStream(System.Net.Sockets.NetworkStream stream){
        while (true){
            System.Byte[] bytes = new System.Byte[2048];
            int size = stream.Read(bytes, 0, bytes.Length);
            var askii = new System.Text.ASCIIEncoding();
            string message = askii.GetString(bytes, 0, size-2);
            Console.WriteLine(message);
            parseCommand(message);

            var writer = new System.IO.StreamWriter(stream);
            writer.WriteLine("successful");
            writer.Flush();
                  
        }
    }
    static void parseCommand(string command){
        if (command == "sysInfo"){
            database.printInfo();
        }
        if ( command.Split(' ')[0] == "search" ){
            database.searchCatalogue(command.Split(' ')[1]);
        }
    }
    static void populate()
    {
        Console.WriteLine("populating database");
        createItem("banana", 2.50);
        createItem("running shoes", 32.50);
        createItem("basketball shoes", 32.50);
        createItem("painting", 50.00);
        createItem("basketball", 25.00);
        createItem("blender", 20.50);
        createItem("glasses", 50.50);
        createItem("keyboard", 72.00);
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
        database.accounts.Add(id, newUser);
        return newUser;
    }
    static Item createItem(string name, double price)
    {
        string id = generateKey();
        Item newItem = new Item(id, name, price);
        database.catalogue.Add(id, newItem);
        return newItem;
    }
}

class Database
{
    public Dictionary<string, Item> catalogue;
    public Dictionary<string, User> accounts;
    public Database()
    {
        catalogue = new Dictionary<string, Item>();
        accounts = new Dictionary<string, User>();
    }

    public void printInfo(){
        Console.WriteLine("---accounts in database---");
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

    public List<KeyValuePair<string, Item>> searchCatalogue(string input)
    {
        List<KeyValuePair<string, Item>> results = new List<KeyValuePair<string, Item>>();
        foreach (KeyValuePair<string, Item> kvp in this.catalogue)
        {
            if (kvp.Value.name.Contains(input)){
                results.Add(kvp);
            }
        }

        Console.WriteLine("------ results for {0}: -----", input);
        foreach (var kvp in results)
        {
            string key = kvp.Key;
            Item result = kvp.Value;
            Console.WriteLine(result.name);
        }

        return results;
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

