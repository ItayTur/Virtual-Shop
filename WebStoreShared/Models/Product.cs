using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WebStoreShared.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Owner ID")]
        public int OwnerId { get; set; }
        [Display(Name = "User ID")]
        public int? UserId { get; set; }
        [Required, StringLength(50)]
        public string Title { get; set; }
        [Required, Display(Name ="Short Description"), StringLength(500)]
        public string ShortDescription { get; set; }
        [Required, Display(Name = "Long Description"), StringLength(4000)]
        public string LongDescription { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required, RegularExpression(@"^\d+\d{0,18}$",ErrorMessage ="Price cant hae more the 18 decimal numbers")]
        public double Price { get; set; }
        public byte[] Picture1 { get; set; }
        public byte[] Picture2 { get; set; }
        public byte[] Picture3 { get; set; }
        public int State { get; set; }

        
        public User UserOwnerNavigation { get; set; }
        public User UserBuierNavigation { get; set; }
    }
}
