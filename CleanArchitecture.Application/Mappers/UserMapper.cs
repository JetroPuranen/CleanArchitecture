using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Mappers
{
    public class UserMapper
    {
        public static UserDTO ToDto(User user)
        {
            return new UserDTO
            {
                Name = user.Name,
                Email = user.Email,
                
            };
        }
    }
}
