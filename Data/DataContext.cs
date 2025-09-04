using csharp_url_shortener_api.Models;
using Microsoft.EntityFrameworkCore;

namespace csharp_url_shortener_api.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Url> Urls { get; set; }
   
    public DbSet<UrlClick> UrlClicks { get; set; }
    
    public DbSet<ProfitSharingRule> ProfitSharingRules { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User ↔ Url (1-to-many)
        modelBuilder.Entity<Url>()
            .HasOne(u => u.Creator)
            .WithMany(u => u.UrlsCreated)
            .HasForeignKey(u => u.CreatedBy)
            .OnDelete(DeleteBehavior.Cascade);

        // Url ↔ UrlClick (1-to-many)
        modelBuilder.Entity<UrlClick>()
            .HasOne(uc => uc.Url)
            .WithMany(u => u.UrlClicks)
            .HasForeignKey(uc => uc.UrlId)
            .OnDelete(DeleteBehavior.Cascade);

        // User ↔ UrlClick (1-to-many)
        modelBuilder.Entity<UrlClick>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UrlClicks)
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // ProfitSharingRule ↔ UrlClick (1-to-many)
        modelBuilder.Entity<UrlClick>()
            .HasOne(uc => uc.ProfitSharingRule)
            .WithMany(p => p.UrlClicks)
            .HasForeignKey(uc => uc.ProfitSharingRuleId)
            .OnDelete(DeleteBehavior.Restrict); // Restrict deletion if clicks exist for this rule

        // Make Username unique
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        // Make ShortUrl unique
        modelBuilder.Entity<Url>()
            .HasIndex(u => u.ShortUrl)
            .IsUnique();
    }
}