using CI_PLATFORM.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class AddStoryViewmodel
    {
        public long StoryMediaId { get; set; }

        public long StoryId { get; set; }

        public string Type { get; set; } = null!;

        public string Path { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public virtual Story Story { get; set; } = null!;
    }
}
