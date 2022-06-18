using AutoMapper;
using BL.Bases;
using BL.Dto;
using BL.Interfaces;
using Type = DAL.Models.Type;
namespace BL.AppServices
{
    public class TypeAppService : AppServiceBase
    {
        public TypeAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {
        }
        public IEnumerable<TypeModel> GetAll()
        {
            IEnumerable<Type> all = TheUnitOfWork.Type.GetTypes();
            return Mapper.Map<IEnumerable<TypeModel>>(all);
        }
        public bool SaveNewType(TypeModel typeModel)
        {
            if (typeModel == null)
                throw new ArgumentNullException();
            bool result = false;
            var type = Mapper.Map<Type>(typeModel);
            if (TheUnitOfWork.Type.Insert(type))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public int GetTypeCount()
        {
            return TheUnitOfWork.Type.CountEntity();
        }
    }
}
