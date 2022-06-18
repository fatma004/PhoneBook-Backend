using BL.Bases;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
 
        public class ContactRepository : BaseRepository<Contact>
        {
            private DbContext dbContext;

            public ContactRepository(DbContext DbContext) : base(DbContext)
            {
                this.dbContext = DbContext;
            }

            public bool InsertContact(Contact contact)
            {
                return Insert(contact);
            }

            public IEnumerable<Contact> GetContacts()
            {
                return GetAll();
            }

            public Contact GetContactById(int id)
            {
                return GetFirstOrDefault(c => c.Id == id);
            }
            public bool CheckContactExists(int id)
            {
                return GetAny(c => c.Id == id);
            }
            public void UpdateContact(Contact contact)
            {
                Update(contact);
            }
            public void DeleteContact(int Id)
            {
                Delete(Id);
            }
        }

    }
