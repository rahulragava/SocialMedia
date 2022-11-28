using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Model.EntityModel
{
    public class PollChoice
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string? Choice { get; set; }
    }
}
