﻿using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Entity
{
    public class FcmToken :BaseEntity
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}
