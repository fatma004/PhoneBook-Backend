using BL.Bases;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
    public class PhoneRepository : BaseRepository<Phone>
    {
        private DbContext dbContext;

        public PhoneRepository(DbContext DbContext) : base(DbContext)
        {
            this.dbContext = DbContext;
        }

        public bool InsertPhone(Phone phone)
        {
            return Insert(phone);
        }

        public IEnumerable<Phone> GetPhones()
        {
            return GetAll();
        }

        public Phone GetPhoneById(int id)
        {
            return GetFirstOrDefault(ph => ph.Id == id);
        }
        public bool CheckPhoneExists(int id)
        {
            return GetAny(ph => ph.Id == id);
        }
        public void UpdatePhone(Phone phone)
        {
            Update(phone);
        }
        public void DeletePhone(int Id)
        {
            Delete(Id);
        }
    }

}
