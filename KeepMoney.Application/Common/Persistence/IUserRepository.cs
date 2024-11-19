﻿using KeepMoney.Domain.Users;

namespace KeepMoney.Application.Common.Persistence;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
}

