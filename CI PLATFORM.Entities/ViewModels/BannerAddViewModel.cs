using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class BannerAddViewModel

    {
        public long BannerId { get; set; }

        public IFormFile Image { get; set; } = null!;
        [Required(ErrorMessage = "Enter Description")]
        public string? Text { get; set; }
        [Required(ErrorMessage = "Enter sort order")]
        public int? SortOrder { get; set; }
        [Required(ErrorMessage = "Enter Title")]
        public string? Title { get; set; }
        public string? Status { get; set; }
    }
}
