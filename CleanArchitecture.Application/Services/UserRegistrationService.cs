using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Mappers;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepository _userRepository;

        public UserRegistrationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepository.EmailExistsAsync(email);
        }

        public async Task<bool> RegisterUserAsync(string name, string email)
        {
            try
            {
                if (await EmailExistsAsync(email))
                {
                    return false;
                }

                var user = new User { Id = Guid.NewGuid(), Name = name, Email = email };
                _userRepository.Add(user);
                return true;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }
        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                return null; // Voit myös heittää poikkeuksen tai käsitellä tilanteen muuten tarpeen mukaan
            return UserMapper.ToDto(user);
        }
    }
}
