using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class Contactusmodel
    {
        public string? FirstName { get; set; }
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }
    }
}
