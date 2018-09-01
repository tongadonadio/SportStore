using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }
        public Cart Cart { get; set; }
        public int Dots { get; set; }
    }
}
