using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Pengembalian
    {
        [Required(ErrorMessage = "ID Pengembalian tidak boleh kosong!")]
        public int IdPengembalian { get; set; }

        [Required(ErrorMessage = "Tanggal Pengembalian tidak boleh kosong!")]
        public DateTime? TglPengembalian { get; set; }

        [Required(ErrorMessage = "ID Peminjaman tidak boleh kosong!")]
        public int? IdPeminjaman { get; set; }

        [Required(ErrorMessage = "ID Kondisi tidak boleh kosong!")]
        public int? IdKondisi { get; set; }

        [Required(ErrorMessage = "Denda tidak boleh kosong!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya boleh diisi dengan angka!")]
        public int? Denda { get; set; }

        public virtual KondisiKendaraan IdKondisiNavigation { get; set; }
        public virtual Peminjaman IdPeminjamanNavigation { get; set; }
    }
}
