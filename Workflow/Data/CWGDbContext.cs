using Microsoft.EntityFrameworkCore;
using CWG.API.Workflow.Data.Entities.Auth;
using CWG.API.Workflow.Data.Entities.ClientModule;
using CWG.API.Workflow.Data.Seed;

namespace CWG.API.Workflow.Data
{
    public class CWGDbContext : DbContext
    {
        public CWGDbContext(DbContextOptions<CWGDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Auth
            modelBuilder.Entity<AuthUser>(entity => { entity.ToTable(name: "AuthUser", schema: "auth"); });
            modelBuilder.Entity<AuthRole>(entity => { entity.ToTable(name: "AuthRole", schema: "auth"); });
            modelBuilder.Entity<AuthUserRole>(entity => { entity.ToTable(name: "AuthUserRole", schema: "auth"); });

            //Client
            modelBuilder.Entity<Client>(entity => { entity.ToTable(name: "Client", schema: "cl"); });
           

            modelBuilder.Seed();

        }


        //Auth
        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<AuthRole> AuthRoles { get; set; }
        public DbSet<AuthUserRole> AuthUserRoles { get; set; }

        // //Client
        public DbSet<Client> Clients { get; set; }
    }
}