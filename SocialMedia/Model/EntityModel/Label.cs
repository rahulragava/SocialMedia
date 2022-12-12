using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Model.EntityModel
{
    public class Label
    {
        public string Id { get; set; }
        public string Name { get; set; }    
        public string PostId { get; set; }


        public Label()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

   
}
