﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Model.EntityModel
{
    public class PollChoice
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public string Choice { get; set; }

        public PollChoice()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
