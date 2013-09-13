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


        public string CreateUser(User user)
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
            
            var guid = Guid.NewGuid();
            UserToConfirm userToConfirm = new UserToConfirm { UserId = user.UserId, CodeForEmail = guid.ToString(), Created = DateTime.Now };
            _context.UserToConfirms.Add(userToConfirm);
            _context.SaveChanges();

            return guid.ToString();
        }


        public User GetUser(int userId)
        {
            return _context.Users.FirstOrDefault(x => x.UserId == userId);
           
       }


        public bool UpdateUser(User updatedUser)
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
                user.Phone = updatedUser.Phone;
                user.Cell = updatedUser.Cell;
                user.Fax = updatedUser.Fax;
                user.Website = updatedUser.Website;

                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public User TryLogin(string userId, string encryptedPWD)
        {
           return _context.Users.FirstOrDefault(x => x.EmailAddress == userId && x.Password == encryptedPWD); 
        
        }

        public bool ConfirmEmail(string code)
        {
            UserToConfirm user = _context.UserToConfirms.FirstOrDefault(x => x.CodeForEmail == code);
            if (user == null)
                return false;
            
            if (user.EmailConfirmed == null)
            {
                user.EmailConfirmed = DateTime.Now;
                _context.SaveChanges();
            }
            return true;
        
        }

    }
}
