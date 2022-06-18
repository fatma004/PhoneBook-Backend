using BL.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = DAL.Models.Type;

namespace BL.Repositories
{
    public class TypeRepository : BaseRepository<Type>
    {
        private DbContext dbContext;

        public TypeRepository(DbContext DbContext) : base(DbContext)
        {
            this.dbContext = DbContext;
        }

        public bool InsertType(Type type)
        {
            return Insert(type);
        }

        public IEnumerable<Type> GetTypes()
        {
            return GetAll();
        }

    }
}
