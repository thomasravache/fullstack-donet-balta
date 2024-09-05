using System.Reflection;
using Dima.Api.Models;
using Dima.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data;

public class AppDbContext : IdentityDbContext<
    User, // AspNetUsers
    IdentityRole<long>, // AspNetRoles (AspnetUserRoles)
    long, // Claim, Login, RoleClaim, Tokens
    IdentityUserClaim<long>,
    IdentityUserRole<long>,
    IdentityUserLogin<long>,
    IdentityRoleClaim<long>,
    IdentityUserToken<long>
>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {  }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // configuracoes do banco de dados
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}