﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model
{
    public class ConfigEntry
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
