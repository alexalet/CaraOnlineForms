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

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.EmailAddress == email && x.IsActive);
        }

        public User UpdateUser(User updatedUser)
        {
            User user = _context.Users.FirstOrDefault(x=>x.UserId==updatedUser.UserId);
            if (user == null)
                return null;
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
                return null;
            }

            return _context.Users.FirstOrDefault(x => x.UserId == updatedUser.UserId);
        }

        public User TryLogin(string userId, string encryptedPWD)
        {
           User user =  _context.Users.FirstOrDefault(x => x.EmailAddress == userId && x.Password == encryptedPWD);
           if (user != null)
           {
               user.LastLoginDate = DateTime.Now;
               _context.SaveChanges();
           }

           return user; 
        
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
        /// <summary>
        /// creates record in table PasswordToResets  for the selected user
        /// returns generated code
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string PrepareResetPassword(int userId)
        {
            PasswordToReset reset = _context.PasswordToResets.FirstOrDefault(x => x.UserId == userId && x.PasswordResetDate == null);
            if (reset == null) // no records
            {
                reset = new PasswordToReset { UserId = userId, CreatedDate = DateTime.Now, Code = Guid.NewGuid().ToString() };
                _context.PasswordToResets.Add(reset);
            }
            else // re-use existing record
            {
                reset.Code = Guid.NewGuid().ToString();
                reset.CreatedDate = DateTime.Now;
            }
            _context.SaveChanges();
            
            return reset.Code;
        }

        public bool ChangePassword(int userId, string newPassword)
        {
            User user = GetUser(userId);
            user.Password = newPassword;
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// returns true if code is valid for reset password
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool IsResetPasswordCodeValid(string code)
        {
            if (string.IsNullOrEmpty(code))
                return false;

            return _context.PasswordToResets.FirstOrDefault(x => x.Code == code && x.PasswordResetDate == null)!=null;
        }

        /// <summary>
        /// newPassword must be already encrypted
        /// </summary>
        /// <param name="code"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ResetPassword(string code, string newPassword)
        {
            PasswordToReset reset = _context.PasswordToResets.FirstOrDefault(x => x.Code==code && x.PasswordResetDate == null);
            if (reset == null) // no records
            {
                return false;
            }

           bool res = ChangePassword(reset.UserId, newPassword);
           if (!res)
           {
               return false;
           }
            // mark record as used:
            reset.PasswordResetDate = DateTime.Now;
            _context.SaveChanges();

            return true;
        }
    }
}
