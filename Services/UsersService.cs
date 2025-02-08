using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Abstract.Auth;
using API.Abstract.Repository;
using API.Abstract.Service;
using API.Data.Repository;
using API.Infrastructure;
using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
    public class UsersService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly UsersRepository _usersRepository;
        private readonly JwtProvider _jwtProvider;

        public UsersService(UsersRepository usersRepository, IPasswordHasher passwordHasher, JwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<Guid> Register(string username, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = new User(username, email, hashedPassword);

            await _usersRepository.Add(user);

            return user.Id;
        }

        public async Task<User?> Get(Guid id)
        {
            var user = await _usersRepository.GetById(id);

            return user;
        }

        public async Task<User?> GetByEmail(string email)
        {
            var user = await _usersRepository.GetByEmail(email);

            return user;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if(result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }
    }
}