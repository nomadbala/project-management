using Microsoft.EntityFrameworkCore;
using ProjectManagement.Model;

namespace ProjectManagement;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; init; }
    
    public DbSet<Assignment> Assignments { get; init; }
    
    public DbSet<Project> Projects { get; init; }
}