using MVCProject.Infrastructure.Filters;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreShared.Data;


namespace MVCProject.Controllers
{
    [SessionTimeout]
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var product = ProductsRepository.GetProduct(id.Value);
            var user = UsersRepository.GetUser(product.OwnerId);
            var signInUser = new SignInUser() { FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };
            LoginSignModel loginSignModel = new LoginSignModel() { Product = product, SignInUser= signInUser };
            return View(loginSignModel);
        }
    }
}