﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Permission:BaseModel
    {
        public string Name { get; set; }
        public List<Group>? Groups { get; set; }

    }
}
