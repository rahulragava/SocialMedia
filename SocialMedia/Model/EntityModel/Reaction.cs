﻿using SocialMedia.Model.EntityModel.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Model.EntityModel
{
    public class Reaction
    {
        public int Id { get;set; }
        public int ReactedBy { get; set; }
        public int ReactionOnId { get; set; }
        public ReactionType reactionType { get; set; }  
        public ReactedOnType ReactionOnType { get; set; }
    }
}