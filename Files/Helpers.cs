using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using contactapi.Models;

namespace contactapi.Helpers
{
    public class Helpers
    {
        /**
         * The function reads csv data from a given fileName
         * Return a null value if any unexpected operations occured, otherwise, return contact list 
        */
        public static List<Contact> readFile(string fileName)
        {
            List<Contact> contacts = null;
            try
            {
                contacts = new List<Contact>();
                string[] lines = File.ReadAllLines(fileName);
                foreach(string line in lines)
                {
                    // Console.WriteLine(line);
                    Contact contact = buildOneContact(line);
                    if (contact != null)
                    {
                        contacts.Add(contact);
                    }
                }

            } catch(Exception e)
            {
                Console.Write(e.Message);
            }
            return contacts;
        }

        /**
         * The function recieve an csv row data and return a Contact object
        */
        private static Contact buildOneContact(string line)
        {
            string[] items = line.Split(",");
            /*[
              firstName = '',
              lastName = '',
              companyName,
              address,
              city,
              county,
              state,
              zip,
              phone1,
              phone = '',
              email = ''
             ] = items;*/
            Contact contact = new Contact();
            contact.firstName = items[0];
            contact.lastName = items[1];
            contact.phone = items[9];
            contact.email = items[10];
            return contact;
        }

        /**
         * The function search a page of data and return client with specific format
         * { code, error, data: { result, total } }
        */
        public static ResponseContacts search(string fileName, string term, int pageNumber, int pageSize)
        {
            ResponseContacts responseContacts = new ResponseContacts();

            // File does not exist
            if (!File.Exists(fileName))
            {
                responseContacts.code = 100;
                responseContacts.error = String.Concat(fileName, " Not existed");
                return responseContacts;
            }

            List<Contact> contacts = Helpers.readFile(fileName);
            
            // search not Ok
            if (contacts == null)
            {
                responseContacts.code = 101;
                responseContacts.error = String.Concat("Error occured while reading ", fileName);
                return responseContacts;
            }

            // filter data with specific page number
            term = term.ToLower();
            var query = contacts.Where(contact => (
                ((contact.firstName != null && contact.firstName.ToLower().IndexOf(term) >= 0) || term == String.Empty) ||
                ((contact.lastName != null && contact.lastName.ToLower().IndexOf(term) >= 0) || term == String.Empty)||
                ((contact.phone != null && contact.phone.ToLower().IndexOf(term) >= 0) || term == String.Empty)||
                ((contact.email != null && contact.email.ToLower().IndexOf(term) >= 0) || term == String.Empty)
            ));

            contacts = query.ToList();
            int total = contacts.Count;
            responseContacts.data.total = total;
            
            // extract data with specific page
            int startIndex = (pageNumber - 1) * pageSize;
            if (total > startIndex)
            {
                // canculate the range we will respond client based on pageSize and pageNumber
                int count = total > (startIndex + pageSize) ? pageSize : total - ((pageNumber - 1) * pageSize);
                contacts = contacts.GetRange(startIndex, count);
                responseContacts.data.result = contacts;
            }

            return responseContacts;
        }
    }
}
