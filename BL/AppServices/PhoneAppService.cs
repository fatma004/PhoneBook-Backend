using AutoMapper;
using BL.Bases;
using BL.Dto;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class PhoneAppService : AppServiceBase
    {
        public PhoneAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {
        }
        public IEnumerable<PhoneModel> GetAllPhones()
        {
            IEnumerable<Phone> all = TheUnitOfWork.Phone.GetPhones();
            return Mapper.Map<IEnumerable<PhoneModel>>(all);
        }
        public bool SaveNewPhone(PhoneModel phoneModel)
        {
            if (phoneModel == null)
                throw new ArgumentNullException();
            bool result = false;
            var phone = Mapper.Map<Phone>(phoneModel);
            if (TheUnitOfWork.Phone.Insert(phone))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool UpdatePhone(PhoneModel phoneModel)
        {
            var phoneFromDB = TheUnitOfWork.Phone.GetById(phoneModel.Id);
            Mapper.Map(phoneModel, phoneFromDB);
            TheUnitOfWork.Phone.Update(phoneFromDB);
            TheUnitOfWork.Commit();
            return true;
        }
        public PhoneModel GetPhoneById(int id)
        {
            return Mapper.Map<PhoneModel>(TheUnitOfWork.Phone.GetById(id));
        }

        public bool DeletePhone(int id)
        {
            bool result = false;

            TheUnitOfWork.Phone.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public int GetPhoneCount()
        {
            return TheUnitOfWork.Phone.CountEntity();
        }
        
    }
}
