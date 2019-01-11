using MVCProject.Infrastructure.Filters;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreShared.Data;
using WebStoreShared.Models;

namespace MVCProject.Controllers
{
    [SessionTimeout]
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index(string order)
        {

            List<Product> products = ProductsRepository.GetProducts();
            if (order == "name")
            {
                products = (from p in products
                            orderby p.Title
                            select p).ToList();

            }
            else if (order == "date")
            {
                products = (from p in products
                            orderby p.Date
                            select p).ToList();
            }

            var LoginSignModel = new LoginSignModel() { Products = products };
            return View(LoginSignModel);
        }
        

        [HttpPost]
        public ActionResult Index(LoginSignModel loginSignModel)
        {
            if (ModelState.IsValid)
            {
                User user = UsersRepository.GetUser(loginSignModel.LoginUser.Name, loginSignModel.LoginUser.Password);
                if (user != null)
                {
                    HttpCookie cookie = new HttpCookie("SignInCookie");
                    cookie.Value = user.Id.ToString() + "," + user.FirstName;
                    cookie.Expires = DateTime.Now.AddMinutes(10);
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }
                else { ViewBag.Error = "Username or password is incorrect"; }
            }
            loginSignModel.Products = ProductsRepository.GetProducts();
            return View(loginSignModel);
        }
        [HttpGet]
        public ActionResult SignIn()
        {
 
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(LoginSignModel loginSignModel)
        {
            User userToAdd = null;
            if (ModelState.IsValid)
            {
                UsersRepository.Add(loginSignModel.SignInUser.FirstName, loginSignModel.SignInUser.LastName, loginSignModel.SignInUser.BirthDate, loginSignModel.SignInUser.Email, loginSignModel.SignInUser.UserName, loginSignModel.SignInUser.Password, out userToAdd);
                HttpCookie cookie = new HttpCookie("SignInCookie");
                cookie.Value = userToAdd.Id.ToString()+","+userToAdd.FirstName;
                cookie.Expires = DateTime.Now.AddMinutes(10);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                return RedirectToAction("Index", loginSignModel);
            }
            return View(loginSignModel);
        }
        public ActionResult Logout()
        {
            Response.Cookies["SignInCookie"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index");
        }

        public ActionResult AddToCart( int? productId)
        {
            if (productId != null)
            {
                if (Request.Cookies["SignInCookie"] != null)
                {
                    string[] cookie = Request.Cookies["SignInCookie"].Value.Split(',');
                    int userId = int.Parse(cookie[0]);
                    UsersRepository.AddToCart(userId, productId.Value);
                    ProductsRepository.ChangeStateToCarted(productId.Value);
                }
                else
                {
                    if (Session["cart"] != null)
                    {
                        List<Product> cart = (List<Product>)Session["cart"];
                        ProductsRepository.ChangeStateToCarted(productId.Value);
                        var productToAdd = ProductsRepository.GetProduct(productId.Value);
                        cart.Add(productToAdd);
                        Session["cart"] = cart;
                        Session.Timeout = 1;
                    }
                    else
                    {
                        List<Product> cart = new List<Product>();
                        ProductsRepository.ChangeStateToCarted(productId.Value);
                        var productToAdd = ProductsRepository.GetProduct(productId.Value);
                        cart.Add(productToAdd);
                        Session["cart"] = cart;
                        Session.Timeout = 1;
                    }
                   
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Cart()
        {
            LoginSignModel loginSignModel = new LoginSignModel();
            if (Request.Cookies["SignInCookie"] != null)
            {
                string[] cookie = Request.Cookies["SignInCookie"].Value.Split(',');
                int userId = int.Parse(cookie[0]);
                loginSignModel.Products = UsersRepository.GetCart(userId);
            }
            else if (Session["cart"] != null) 
            {
                List<Product> cart = (List<Product>)Session["cart"];
                loginSignModel.Products = cart;
            }
            return View(loginSignModel);
        }
        public ActionResult Buy()
        {
            if (Request.Cookies["SignInCookie"] != null)
            {
                string[] cookie = Request.Cookies["SignInCookie"].Value.Split(',');
                int userId = int.Parse(cookie[0]);
                UsersRepository.Buy(userId);
                TempData["BuyMessage"]= "Thank you for your buy! keep on shoping!";
            }
            else if (Session["cart"] != null)
            {
                UsersRepository.Buy((List<Product>)Session["cart"]);
                TempData["BuyMessage"] = "Thank you for your buy! keep on shoping!";
                Session["cart"] = null;
            }
           
            return RedirectToAction("Index");
               
        }
        [HttpGet]
        public ActionResult Sell()
        {
            ViewBag.IsSold = false;
            return View();
        }

        [HttpPost]
        public ActionResult Sell(LoginSignModel loginSignModel, HttpPostedFileBase picture1, HttpPostedFileBase picture2, HttpPostedFileBase picture3)
        {
            ViewBag.IsSold = false;
            if (ModelState.IsValid)
            {
                string[] cookie = Request.Cookies["SignInCookie"].Value.Split(',');
                int userId = int.Parse(cookie[0]);
                Product productToAdd = new Product()
                {
                    OwnerId = userId,
                    Title = loginSignModel.SellingProduct.Title,
                    ShortDescription = loginSignModel.SellingProduct.ShortDescription,
                    LongDescription = loginSignModel.SellingProduct.LongDescription,
                    Date = loginSignModel.SellingProduct.Date,
                    Price = loginSignModel.SellingProduct.Price,
                    Picture1 = ConvertToByteArry(picture1),
                    Picture2 = ConvertToByteArry(picture2),
                    Picture3 = ConvertToByteArry(picture3),
                    State=1
                };

                ProductsRepository.Sell(productToAdd);
                ViewBag.IsSold = true;
            }
            
            return View();
        }
        private byte[] ConvertToByteArry(HttpPostedFileBase file)
        {
            if (file != null)
            {
                MemoryStream stream = new MemoryStream();
                file.InputStream.CopyTo(stream);
                return stream.ToArray();
            }
            else
            {
                return null;
            }
        }

        public ActionResult Delete(int productId)
        {
                ProductsRepository.Delete(productId);
            
            if (Session["cart"] != null)
            {
                List<Product> cart = (List<Product>)Session["cart"];
                foreach (var product in cart)
                {
                    if (product.Id == productId)
                    {
                        cart.Remove(product);
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Cart");
        }

        [HttpGet]
        public ActionResult NewProfile()
        {
            if (Request.Cookies["SignInCookie"] != null)
            {
                string[] cookie = Request.Cookies["SignInCookie"].Value.Split(',');
                int userId = int.Parse(cookie[0]);
                User user = UsersRepository.GetUser(userId);
                SignInUser signInUser = new SignInUser()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    Email = user.Email,
                    UserName = user.UserName,
                    Password = user.Password,
                    VerificationPassword = user.Password
                };
                LoginSignModel loginSignModel = new LoginSignModel() { SignInUser = signInUser };
                return View(loginSignModel);
            }
            return RedirectToAction("SignIn");

        }

        [HttpPost]
        public ActionResult NewProfile(LoginSignModel loginSignModel)
        {
            ModelState.Remove(loginSignModel.SignInUser.UserName);
            if (ModelState.IsValid)
            {
                string[] cookie = Request.Cookies["SignInCookie"].Value.Split(',');
                int userId = int.Parse(cookie[0]);
                User userToUpdate = new User()
                {
                    Id= userId,
                    FirstName = loginSignModel.SignInUser.FirstName,
                    LastName = loginSignModel.SignInUser.LastName,
                    BirthDate = loginSignModel.SignInUser.BirthDate,
                    Email = loginSignModel.SignInUser.Email,
                    UserName = loginSignModel.SignInUser.UserName,
                    Password = loginSignModel.SignInUser.Password
                };
                
                UsersRepository.Update(userToUpdate);
                Response.Cookies["SignInCookie"].Value = userId.ToString() + "," + userToUpdate.FirstName;
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}