using System.Collections.Generic;

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
