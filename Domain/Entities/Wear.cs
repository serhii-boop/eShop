using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Wear
    {
        [HiddenInput(DisplayValue = false)]
        [Display(Name="ID")]
        public int WearId { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть назву товару")]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть матеріал товару")]
        [Display(Name = "Тканина")]
        public string Fabric { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть розмір товару")]
        [Display(Name = "Розмір")]
        public string Size { get; set; }


        [Required(ErrorMessage = "Будь ласка, введіть опис товару")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть ціну товару")]
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть категорію товару")]
        [Display(Name = "Категорія")]
        public string Category { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }


    }
}
