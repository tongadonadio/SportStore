using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using SportStore.Model.Notifications;

namespace SportStore.Model
{
    public class Session
    {
        [Key]
        public string Token { get; set; }
        public User User { get; set; }
        [NotMapped]
        public List<Notification> Notifications { get; set; }

        public Session()
        {
        }

        public Session(User user)
        {
            this.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            this.User = user;
        }
    }
}
