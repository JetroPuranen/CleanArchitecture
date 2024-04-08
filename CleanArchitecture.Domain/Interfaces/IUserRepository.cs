﻿using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        //bool EmailExists(string email);
        Task<bool> EmailExistsAsync(string email);

        Task Addsync(User user);

        Task<User> GetUserByEmailAsync(string email);
    }
}
