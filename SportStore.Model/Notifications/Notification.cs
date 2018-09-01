using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model.Notifications
{
    public abstract class Notification
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
