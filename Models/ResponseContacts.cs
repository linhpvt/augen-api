using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace contactapi.Models
{
    public class ResponseContacts
    {
        public int code { get; set; }
        public string error { get; set; }

        public Data data { get; set; }
    }

    public class Data
    {
        public int total { get; set; }
        public List<Contact> result { get; set; }

    }
}
