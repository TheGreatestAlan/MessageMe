using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageMe.Model
{
    public class Message
    {
        public string SenderId { get; set; }

        public string RecipientId { get; set; }

        public string MessageText { get; set; }

    }
}
