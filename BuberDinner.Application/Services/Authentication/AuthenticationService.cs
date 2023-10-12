using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BuberDinner.Application.Commands.GeneralCommands;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Queries.GeneralQueries;
using BuberDinner.Domain.Entities;
using MediatR;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IMediator mediator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            // 1. Validate the user doesn't exist
            var queryFindUserByEmail = new FindBy<User>(_ => _.Email == email, true);
            var resultQueryUser = (await _mediator.Send(queryFindUserByEmail)).FirstOrDefault();

            if (resultQueryUser is not null)
            {
                throw new Exception("User already exist.");
            }

            // 2. Crete user (generate unique ID) & Persist to DB
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            var commandAddUser = new Add<User>(user);
            var result = await _mediator.Send(commandAddUser);

            if (result <= 0) throw new Exception("Can not create User.");

            // 3. Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }

        public async Task<AuthenticationResult> Login(string email, string password)
        {
            // 1. Validate the user exist
            var queryFindUserByEmail = new FindBy<User>(_ => _.Email == email, true);
            var user = (await _mediator.Send(queryFindUserByEmail)).FirstOrDefault();

            if (user is null)
            {
                throw new Exception("User doesn't exist.");
            }

            // 2. Validate the password
            if (user.Password != password)
            {
                throw new Exception("Invalid password.");

            }

            // 3. Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
