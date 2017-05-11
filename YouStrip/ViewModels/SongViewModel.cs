using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace YouStrip.ViewModels
{
    public class SongViewModel
    {
        [Required]
        [Display(Name = "Song")]
        public ICollection<IFormFile> files { get; set; }
    }
}
