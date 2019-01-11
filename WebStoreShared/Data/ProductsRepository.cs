using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreShared.Models;

namespace WebStoreShared.Data
{
    public static class ProductsRepository
    {
        public static Product GetProduct(int productId)
        {
            using (Context context = new Context())
            {
                return context.Products.Where(p => p.Id == productId).FirstOrDefault();
            }
        }

        public static List<Product> GetProducts()
        {

            using (Context context = new Context())
            {
                return context.Products.ToList();
            }

        }

        public static void ChangeStateToCarted(int productId)
        {
            using(Context context = new Context())
            {
                foreach (var product in context.Products)
                {
                    if (product.Id == productId)
                    {
                        product.State = 2;
                        context.Products.Attach(product);
                        context.Entry(product).State = EntityState.Modified;
                        break;
                    }
                }
                context.SaveChanges();
            }
        }

        public static void Sell(Product product)
        {
            using(Context context = new Context())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public static void Delete(int productId)
        {
            using(Context context = new Context())
            {
                foreach (var product in context.Products)
                {
                    if (product.Id == productId)
                    {
                        product.State = 1;
                        product.UserId = null;
                        context.Products.Attach(product);
                        context.Entry(product).State = EntityState.Modified;
                        break;
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
