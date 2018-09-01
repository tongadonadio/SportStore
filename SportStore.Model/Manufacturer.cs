using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model
{
    public class Manufacturer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
