using System;
using TestLibrary;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // This is the library object with the Save and Find methdods.
            Compact compact = new Compact();
            // This is just a simple example to try our library methods if they are working as expected.
            User user = new User() { Age = 20, Name = "Ahmad" };
            User user2 = new User() { Age = 25, Name = "Husam" };
            long id = compact.Save(user);
            long id2 = compact.Save(user2);
            User foundUser = (User)compact.Find(id);

            Console.WriteLine(foundUser.Name);
        }
    }
    // This is an example class to try our library.
    [Serializable]
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
