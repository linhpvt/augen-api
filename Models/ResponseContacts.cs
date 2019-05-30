using System.Collections.Generic;

namespace contactapi.Models
{
    public class ResponseContacts
    {
        public ResponseContacts()
        {
            code = 0;
            error = null;
            data = new Data();
        }
        public int code { get; set; }
        public string error { get; set; }

        public Data data { get; set; }
    }

    public class Data
    {
        public Data()
        {
            total = 0;
            result = new List<Contact>();
        }
        public int total { get; set; }
        public List<Contact> result { get; set; }

    }
}
