using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RentalKendaraan.Models.Upload
{
    public class FileInputModel
    {
        public IFormFile FileToUpload { get; set; }
    }
}
