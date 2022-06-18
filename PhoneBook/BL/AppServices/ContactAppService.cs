using AutoMapper;
using BL.Bases;
using BL.Dto;
using BL.Helpers;
using BL.Interfaces;
using DAL.Models;
using System.Linq;

namespace BL.AppServices
{
    public class ContactAppService : AppServiceBase
    {
        public ContactAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {
        }
        public IEnumerable<ContactModel> GetAllContacts()
        {
            IEnumerable<Contact> all = TheUnitOfWork.Contact.GetContacts();
            return Mapper.Map<IEnumerable<ContactModel>>(all);
        }
        public bool SaveNewContact(ContactModel contactModel)
        {
            if (contactModel == null)
                throw new ArgumentNullException();
            bool result = false;
            var contact = Mapper.Map<Contact>(contactModel);
            if (TheUnitOfWork.Contact.Insert(contact))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }

        public void UpdatePhonesRelatedToContact(ContactModel contactModel)
        {
            List<Phone> oldPhones = TheUnitOfWork.Phone.GetWhere(ph => ph.ContactId == contactModel.Id).ToList();
            var newPhones_ = contactModel.Phones;
            List<Phone> newPhones = new List<Phone>();
            foreach(var ph in newPhones_)
            {
                newPhones.Add(Mapper.Map<Phone>(ph));
            }
            PhoneComparer phoneComparer = new PhoneComparer();
            List<Phone> deletedPhones = oldPhones.Except(newPhones, phoneComparer).ToList();
            foreach(var ph in deletedPhones)
            {
                TheUnitOfWork.Phone.DeletePhone(ph.Id);
            }
        }
        public bool UpdateContact(ContactModel contactModel)
        {
            var contactFromDB = TheUnitOfWork.Contact.GetById(contactModel.Id);
            Mapper.Map(contactModel, contactFromDB);
            TheUnitOfWork.Contact.Update(contactFromDB);
            UpdatePhonesRelatedToContact(contactModel);
            TheUnitOfWork.Commit();
            return true;
        }
        public ContactModel GetContactById(int id)
        {
            ContactModel contact = Mapper.Map<ContactModel>(TheUnitOfWork.Contact.GetById(id));
            contact.Phones = GetAllPhonesOfContact(id).ToList();
            return contact;
        }
        public IEnumerable<PhoneModel> GetAllPhonesOfContact(int contactId)
        {
            IEnumerable<Phone> res = TheUnitOfWork.Phone.GetWhere(ph => ph.ContactId == contactId);
            List<PhoneModel> phones = new List<PhoneModel>();
            foreach(var phone in res)
            {
                phones.Add(Mapper.Map<PhoneModel>(phone));
            }
            return phones;
        }
        public void DeleteAllPhonesOfContactByContactId(int contactId)
        {
            var phones = GetAllPhonesOfContact(contactId);
            if (phones != null)
            {
                foreach(var phone in phones)
                {
                    TheUnitOfWork.Phone.DeletePhone(phone.Id);
                }
            }
        }
        public bool DeleteContact(int id)
        {
            bool result = false;
            DeleteAllPhonesOfContactByContactId(id);
            TheUnitOfWork.Contact.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public int GetContactCount()
        {
            return TheUnitOfWork.Contact.CountEntity();
        }

        public IEnumerable<ContactModel> GetAllContactsOfUserByUserId(string userId)
        {
            var res = TheUnitOfWork.Contact.GetWhere(c => c.UserId == userId);
            List<ContactModel> contacts = new List<ContactModel>();
            foreach(var contact in res)
            {
                ContactModel contactModel = Mapper.Map<ContactModel>(contact);
                contactModel.Phones = GetAllPhonesOfContact(contact.Id).ToList();
                contacts.Add(contactModel);
            }
            contacts.Sort((x, y) => string.Concat(x.FirstName, x.LastName).CompareTo(string.Concat(y.FirstName, y.LastName)));
            return contacts;
        }
        public IEnumerable<ContactModel> SortSearchResult(IEnumerable<ContactModel>contacts, string KeyWord)
        {
            var list = new List<KeyValuePair<int, ContactModel>> ();
            foreach(var contact in contacts)
            {
                int idx = string.Concat(contact.FirstName, contact.LastName).IndexOf(KeyWord);
                list.Add(new KeyValuePair<int, ContactModel>(idx, contact));
            }
            list.Sort((x, y) => x.Key.CompareTo(y.Key));
            List<ContactModel> res = new List<ContactModel>();
            foreach(var l in list)
            {
                res.Add(l.Value);
            }
            return res;
        }
        public IEnumerable<ContactModel> SearchByName(string userId, string KeyWord)
        {
            var res = TheUnitOfWork.Contact.GetWhere(c => c.UserId == userId && string.Concat(c.FirstName, c.LastName).Contains(KeyWord));
            List<ContactModel> contacts = new List<ContactModel>();
            foreach (var contact in res)
            {
                ContactModel contactModel = Mapper.Map<ContactModel>(contact);
                contactModel.Phones = GetAllPhonesOfContact(contact.Id).ToList();
                contacts.Add(contactModel);
            }
            return SortSearchResult(contacts, KeyWord);
        }
    }
}
