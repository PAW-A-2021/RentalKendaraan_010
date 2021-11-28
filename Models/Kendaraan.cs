using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Kendaraan
    {
        public Kendaraan()
        {
            Peminjamen = new HashSet<Peminjaman>();
        }

        [Required(ErrorMessage = "ID Kendaraan tidak boleh kosong!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya boleh diisi dengan angka!")]
        public int IdKendaraan { get; set; }

        [Required(ErrorMessage = "Nama Kendaraan tidak boleh kosong!")]
        public string NamaKendaraan { get; set; }

        [Required(ErrorMessage = "No Polisi tidak boleh kosong!")]
        public string NoPolisi { get; set; }

        [Required(ErrorMessage = "No STNK tidak boleh kosong!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya boleh diisi dengan angka!")]
        public string NoStnk { get; set; }
        public int? IdJenisKendaraan { get; set; }

        [Required(ErrorMessage = "Ketersediaan tidak boleh kosong!")]
        public string Ketersediaan { get; set; }

        public virtual JenisKendaraan IdJenisKendaraanNavigation { get; set; }
        public virtual ICollection<Peminjaman> Peminjamen { get; set; }
    }
}
