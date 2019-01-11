using MVCProject.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class SellingProduct
    {
        [Required, StringLength(50)]
        public string Title { get; set; }
        [Required, StringLength(500)]
        public string ShortDescription { get; set; }
        [Required, StringLength(4000)]
        public string LongDescription { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required, RegularExpression(@"^\d+\d{0,18}$", ErrorMessage = "Price cant have more the 18 decimal numbers")]
        [ValidPrice]
        public double Price { get; set; }
        public byte[] Picture1 { get; set; }
        public byte[] Picture2 { get; set; }
        public byte[] Picture3 { get; set; }
    }
}