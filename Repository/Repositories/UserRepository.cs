using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using CaraEntites;



namespace Repository
{
    public class UserRepository: RepositoryBase
    {

        public bool IsEmailUnique(string email)
        {
           
            var u = _context.Users.Where(x => x.EmailAddress == email).ToList();
            return u.Count == 0;
        }


        public string CreateUser(User user, string phone, string cell, string fax)
        {

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                var txt = ex;
            }
            var userPhones = new UserPhone {UserId=user.UserId, PhoneTypeCode="WORK", PhoneNumber=phone };
            _context.UserPhones.Add(userPhones);
            _context.SaveChanges();

            if (cell != null)
            {
                userPhones.PhoneNumber = cell;
                userPhones.PhoneTypeCode = "CELL";
                _context.UserPhones.Add(userPhones);
                _context.SaveChanges();
            }

            if (fax != null)
            {
                userPhones.PhoneNumber = fax;
                userPhones.PhoneTypeCode = "FAX";
                _context.UserPhones.Add(userPhones);
                _context.SaveChanges();
            }

            var guid = Guid.NewGuid();
            EmailsToConfirm emailsToConfirm = new EmailsToConfirm { UserId = user.UserId, Code = guid.ToString(), Created = DateTime.Now };
            _context.EmailsToConfirms.Add(emailsToConfirm);
            _context.SaveChanges();

            return guid.ToString();
        }


        public User GetUser(int userId, out string phone, out string cell, out string fax)
        {
            phone = "";
            cell = "";
            fax = "";
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
                return null;

            List<UserPhone> uph = _context.UserPhones.Where(x => x.UserId == userId).ToList();
            foreach (UserPhone u in uph)
            {
                if (u.PhoneTypeCode == "WORK")
                    phone = u.PhoneNumber;

                if (u.PhoneTypeCode == "CELL")
                    cell = u.PhoneNumber;

                if (u.PhoneTypeCode == "FAX")
                    fax = u.PhoneNumber;
            }

            return user;
       }


        public bool UpdateUser(User updatedUser, string phone, string cell, string fax)
        {
            User user = _context.Users.FirstOrDefault(x=>x.UserId==updatedUser.UserId);
            if (user == null)
                return false;
            try
            {
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.Title = updatedUser.Title;
                user.Address1 = updatedUser.Address1;
                user.Address2 = updatedUser.Address2;
                user.City = updatedUser.City;
                user.CountryId = updatedUser.CountryId;
                user.State = updatedUser.State;
                user.Zip = updatedUser.Zip;
                user.Website = updatedUser.Website;

                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            UserPhone uph = _context.UserPhones.FirstOrDefault(x=>x.UserId==updatedUser.UserId && x.PhoneTypeCode=="WORK");
            if (uph == null)
            {
                uph = new UserPhone { UserId = updatedUser.UserId, PhoneNumber = (phone ?? ""), PhoneTypeCode = "WORK" };
            }
            else
            {
                uph.PhoneNumber = (phone ?? "");
            }
            _context.SaveChanges();

            uph = _context.UserPhones.FirstOrDefault(x => x.UserId == updatedUser.UserId && x.PhoneTypeCode == "CELL");
            if (uph == null)
            {
                uph = new UserPhone { UserId = updatedUser.UserId, PhoneNumber = (cell ?? ""), PhoneTypeCode = "CELL" };
            }
            else
            {
                uph.PhoneNumber = (cell ?? "");
            }
            _context.SaveChanges();

            uph = _context.UserPhones.FirstOrDefault(x => x.UserId == updatedUser.UserId && x.PhoneTypeCode == "FAX");
            if (uph == null)
            {
                uph = new UserPhone { UserId = updatedUser.UserId, PhoneNumber = (fax ?? ""), PhoneTypeCode = "FAX" };
            }
            else
            {
                uph.PhoneNumber = (fax ?? "");
            }
            _context.SaveChanges();

            return true;
        }

        public User TryLogin(string userId, string encryptedPWD)
        {
           return _context.Users.FirstOrDefault(x => x.EmailAddress == userId && x.Password == encryptedPWD); 
        
        }
    }
}
