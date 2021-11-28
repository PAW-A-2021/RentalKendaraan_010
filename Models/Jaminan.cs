using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Jaminan
    {
        public Jaminan()
        {
            Peminjamen = new HashSet<Peminjaman>();
        }

        [Required(ErrorMessage = "ID Jaminan tidak boleh kosong!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya boleh diisi dengan angka!")]
        public int IdJaminan { get; set; }

        [Required(ErrorMessage = "Nama Jaminan tidak boleh kosong!")]
        public string NamaJaminan { get; set; }

        public virtual ICollection<Peminjaman> Peminjamen { get; set; }
    }
}
