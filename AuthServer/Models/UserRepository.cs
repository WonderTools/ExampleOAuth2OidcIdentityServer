﻿
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;

namespace AuthServer.Models
{
    public class UserRepository
    {
        private readonly ApplicationDbcontext _context;

        public UserRepository(ApplicationDbcontext context)
        {
            _context = context;
        }

        public User GetUserByUsernameOrNull(string username)
        { 
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            return user;
        }
        public string RegisterUser(string userName,string password,string userType)
        {
            var data = _context.Users.FirstOrDefault(p => p.Username ==userName);
            if (data != null)
            {
                throw new InvalidOperationException();
            }
            data = new User
            {
                Username =userName,
                Password = password,
                UserType = userType
            };
            _context.Users.Add(data);
            _context.SaveChangesAsync();
            if (userName.IsNullOrEmpty() || password.IsNullOrEmpty() || userType.IsNullOrEmpty())
            {
                throw new Exception();
            }
            return data.Id.ToString();
        }

        public async Task<int> DeleteUser(string userName)
        {
            var result = 0;
            if (userName.IsNullOrEmpty())
            {
                throw new Exception();
            }
            if (_context == null)
            {
                return result;
            }

            var data = await _context.Users.FirstOrDefaultAsync(x => x.Username == userName);
            if (data == null)
            {
                throw new InvalidOperationException();
            }
                      
            _context.Users.Remove(data);
             result = await _context.SaveChangesAsync();
             return result;
        }

         public async Task<int> UpdateUser(string userName, string password,string userType)
        {
            var result = 0;
            if (_context == null)
            {
                return result;
            }        
                var data = await _context.Users.FirstAsync(x => x.Username == userName);
            if (data == null)
            {
                throw new InvalidOperationException();
            }

            data.Password = password;
            _context.Users.Update(data);
             result = await _context.SaveChangesAsync();
            return result;
        }            
    }         
}

