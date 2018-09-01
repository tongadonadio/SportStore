using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model
{
    public class Role
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }

        public Role()
        { }

        public Role(string name)
            : this()
        {
            this.Name = name;
        }

        public static readonly Role Administrator = new Role(RoleName.Administrator);
    }
}
