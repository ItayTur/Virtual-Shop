using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStoreShared.Models;

namespace MVCProject.Models
{
    public class LoginSignModel
    {
        public LoginUser LoginUser { get; set; }
        public SignInUser SignInUser { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public Product Product { get; set; }
        public SellingProduct SellingProduct { get; set; }

        public LoginSignModel()
        {
            Products = new List<Product>();
        }
    }
}