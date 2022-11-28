﻿using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Model.BusinessModel
{
    public class PollPostBobj : PostBobj
    {
        public string? Question { get; set; }
        public List<PollChoiceBobj>? choices;

    }
}
