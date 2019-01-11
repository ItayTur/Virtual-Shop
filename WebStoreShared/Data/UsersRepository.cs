using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreShared.Models;

namespace WebStoreShared.Data
{
    public static class UsersRepository
    {
        //Return user by user name and password 
        public static User GetUser(string userName, string userPassword)
        {
            User userToReturn = null;

            using (Context context = new Context())
            {
                foreach (var user in context.Users)
                {
                    if (user.UserName == userName && user.Password == userPassword)
                    {
                        userToReturn = user;
                        break;
                    }
                }

                return userToReturn;
            }
        }

        public static bool IsUserNameValid(string userName)
        {
            using(Context context = new Context())
            {
                return context.Users.Any(user => user.UserName == userName);
            }
        }

        //Return user by id
        public static User GetUser(int userId)
        {
            User userToReturn = null;
            using(Context context = new Context())
            {
                foreach (var user in context.Users)
                {
                    if (user.Id == userId)
                    {
                        userToReturn = user;
                    }
                }
            }
            return userToReturn;
        }
        //update user data
        public static void Update(User user)
        {
            using(Context context = new Context())
            {
                context.Users.Attach(user);
                context.Entry(user).State = EntityState.Modified;

                context.SaveChanges();
            }
        }

        public static void Add(string firstName, string lastName, DateTime birthday, string email, string userName, string password, out User userToAdd)
        {
            
            userToAdd = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthday,
                Email = email,
                UserName = userName,
                Password = password
            };

            using (Context context = new Context())
            {
                userToAdd = context.Users.Add(userToAdd);
                context.SaveChanges();
                //userToAdd = (from u in context.Users
                //             where u.UserName == userName && u.Password == password
                //             select u).ToList().SingleOrDefault();
            }
        }

        public static void AddToCart(int userId,int productId)
        {
            using(Context context = new Context())
            {
                Product productToAdd=null;
                User userToAdd = null;
                foreach (var product in context.Products)
                {
                    if (product.Id == productId)
                    {
                        productToAdd = product;
                        break;
                    }
                }
                foreach (var user in context.Users)
                {
                    if(user.Id == userId)
                    {
                        userToAdd = user;
                        break;
                    }
                }

                userToAdd.Cart.Add(productToAdd);

                context.SaveChanges();
            }
        }

        public static List<Product> GetCart(int userId)
        {
            List<Product> cart = new List<Product>();
            using(Context context = new Context())
            {
                cart = context.Products.Where(p => p.UserId == userId).ToList();
            }
            return cart;
        }

        public static void EmptySessionCart()
        {
            using(Context context = new Context())
            {
                foreach (var product in context.Products)
                {
                    if (product.UserId == null && product.State == 2)
                    {
                        product.State = 1;
                        context.Products.Attach(product);
                        context.Entry(product).State = EntityState.Modified;
                    }
                }

                context.SaveChanges();
            }
        }

        public static void Buy(List<Product>products)
        {
            using(Context context = new Context())
            {
                foreach (var product1 in context.Products)
                {
                    foreach (var product2 in products)
                    {
                        if (product1.Id == product2.Id)
                        {
                            product1.State = 3;
                            context.Products.Attach(product1);
                            context.Entry(product1).State = EntityState.Modified;
                        }
                    }
                    
                }
                context.SaveChanges();
            }
        }

        public static void Buy(int userId)
        {
            using(Context context = new Context())
            {
                var SoldProducts = context.Products.Where(p => p.UserId == userId).ToList();

                foreach (var product in SoldProducts)
                {
                    product.State = 3;
                    context.Products.Attach(product);
                    context.Entry(product).State = EntityState.Modified;
                }

                context.SaveChanges();
            }
        }


    }
}
