using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace contactapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        // GET api/contacts
        [HttpGet("search")]
        public ActionResult<Models.ResponseContacts> Get(String term, int pageNumer, int pageSize)
        {
            string currentWorkingDir = Directory.GetCurrentDirectory();
            // D:\Projects\contactapi\data\contacts.csv;
            string fileName = currentWorkingDir + @"\data\contacts.csv";
            term = term == null ? "" : term;
            pageNumer = pageNumer == 0 ? 1 : pageNumer;
            pageSize = pageSize == 0 ? 10 : pageSize;
            
            Models.ResponseContacts responseContacts = Helpers.Helpers.search(fileName,
                term,
                pageNumer,
                pageSize);
            return responseContacts;
        }

        // POST api/contacts
        [HttpPost()]
        public void Post([FromBody] string value)
        {
        }
    }
}
