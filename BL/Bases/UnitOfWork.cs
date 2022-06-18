using BL.Interfaces;
using BL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace BL.Bases
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Common Properties
        private DbContext DbContext { get; set; }
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        #endregion

        #region Constructors
        public UnitOfWork(ApplicationDbContext DbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this.DbContext = DbContext;
        }
        #endregion

        public TypeRepository type;
        public TypeRepository Type
        {
            get
            {
                if (type == null)
                    type = new TypeRepository(DbContext);
                return type;
            }
        }
        public PhoneRepository phone;
        public PhoneRepository Phone
        {
            get
            {
                if (phone == null)
                    phone = new PhoneRepository(DbContext);
                return phone;
            }
        }
        public ContactRepository contact;
        public ContactRepository Contact
        {
            get
            {
                if (contact == null)
                    contact = new ContactRepository(DbContext);
                return contact;
            }
        }
        #region Methods
        public int Commit()
        {
            return DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
        #endregion
    }
}