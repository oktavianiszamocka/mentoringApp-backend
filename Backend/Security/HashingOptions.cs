﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Security
{
    public sealed class HashingOptions
    {
        public int Iterations { get; set; } = 10000;
    }
}
