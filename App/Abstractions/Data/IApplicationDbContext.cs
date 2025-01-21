﻿using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace App.Abstractions.Data;
public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
