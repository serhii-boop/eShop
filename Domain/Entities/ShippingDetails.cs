using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Вкажіть Ваше ім'я")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Вкажіть Ваш номер телефону")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Адрес доставки")]
        [Display(Name = "Перший адрес")]
        public string Line1 { get; set; }
        [Display(Name = "Другий адрес")]
        public string Line2 { get; set; }


        [Required(ErrorMessage = "Вкажіть місто")]
        [Display(Name = "Місто")]
        public string City { get; set; }

        [Required(ErrorMessage = "Вкажіть країну")]
        [Display(Name = "Країна")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
