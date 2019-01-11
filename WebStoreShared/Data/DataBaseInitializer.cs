using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreShared.Models;

namespace WebStoreShared.Data
{
    public class DataBaseInitializer: DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context)
        {
            //This is the database's seed data
            //6 users (3 of them are sellers), 3 products

            User user1 = new User()
            {
                FirstName = "Itay",
                LastName = "Tur",
                BirthDate = new DateTime(1994, 9, 14),
                Email = "ItayT@sela.co.il",
                UserName="ItayTur",
                Password="1",
                
            };

            User user2 = new User()
            {
                FirstName = "Avi",
                LastName = "Hadad",
                BirthDate = new DateTime(1990, 10, 1),
                Email = "AviH@sela.co.il",
                UserName = "AviH",
                Password = "1"
            };

            User user3 = new User()
            {
                FirstName = "Tomer",
                LastName = "Tai",
                BirthDate = new DateTime(1993, 3, 25),
                Email = "TomerT@sela.co.il",
                UserName = "TPain",
                Password = "1"
            };

            User user4 = new User()
            {
                FirstName = "Oded",
                LastName = "Harmony",
                BirthDate = new DateTime(1992, 1, 1),
                Email = "OdedH@sela.co.il",
                UserName = "OdedH",
                Password = "1"
            };

            User user5 = new User()
            {
                FirstName = "Omer",
                LastName = "Cohen",
                BirthDate = new DateTime(1995, 11, 23),
                Email = "OmerC@sela.co.il",
                UserName = "OmerC",
                Password = "1"
            };

            User user6 = new User()
            {
                FirstName = "Sasha",
                LastName = "King",
                BirthDate = new DateTime(1994, 6, 19),
                Email = "SashaK@sela.co.il",
                UserName = "SashaK",
                Password = "1"
            };

            Product product1 = new Product()
            {
                UserOwnerNavigation=user1,
                Title = "Shoes",
                ShortDescription = "Black sport shoes",
                LongDescription = "This is amazing running shoes," +
                " you can run 40 miles with them and not feel a thing!" +
                " they are black and they for sport!",
                Date = new DateTime(2018, 1, 1),
                Price = 200,
                Picture1 = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory+ "\\Images\\shoe.jpg"),
                State = 1
            };
            

            Product product2 = new Product()
            {
                UserOwnerNavigation = user2,
                Title = "Lamborgini",
                ShortDescription = "used sport car",
                LongDescription = "For the speed and edge lovers" +
                " this car shows power with style, making any ride unforgetable" +
                "buy now and you won't regret it",
                Date = new DateTime(2018, 2, 14),
                Price = 500000,
                Picture1 = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/Images/lambo.jpg"),
                State = 1
            };
            
            Product product3 = new Product()
            {
                UserOwnerNavigation = user3,
                Title = "IPhone X",
                ShortDescription = "The newest iphone out there",
                LongDescription = "Screen so immersive the device itself disappears " +
                "into the experience. Device so intelligent it can" +
                " respond to a tap, your voice, and even a glance. With iPhone X," +
                " that vision is now a reality. Say hello to the future.",
                Date = new DateTime(2017, 12, 12),
                Price = 4000,
                Picture1 = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/Images/iphone1.jpg"),
                Picture2 = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/Images/iphone2.jpg"),
                Picture3 = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/Images/iphone3.jpg"),
                State = 1
            };

            user1.Products.Add(product1);
            user2.Products.Add(product2);
            user3.Products.Add(product3);

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.Users.Add(user4);
            context.Users.Add(user5);
            context.Users.Add(user6);

            context.Products.Add(product1);
            context.Products.Add(product2);
            context.Products.Add(product3);

            context.SaveChanges();
        }
    }
}
