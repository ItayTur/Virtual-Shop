using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreShared.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required ,Display(Name ="First Name"), StringLength(50)]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name"), StringLength(50)]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required, StringLength(50)]
        public string Email { get; set; }
        [Required, Display(Name = "User Name"), StringLength(50)]
        public string UserName { get; set; }
        [Required, StringLength(50)]
        public string Password { get; set; }

        [ForeignKey("OwnerId")]
        public ICollection<Product> Products { get; set; }
        [ForeignKey("UserId")]
        public ICollection<Product> Cart { get; set; }

        public User()
        {
            Products = new List<Product>();
            Cart = new List<Product>();
        }
    }
}
