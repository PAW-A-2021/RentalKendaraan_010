using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Customers = new HashSet<Customer>();
        }

        [Required(ErrorMessage = "ID Gender tidak boleh kosong!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya boleh diisi dengan angka!")]
        public int IdGender { get; set; }

        [Required(ErrorMessage = "Nama Gender tidak boleh kosong!")]
        public string NamaGender { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
