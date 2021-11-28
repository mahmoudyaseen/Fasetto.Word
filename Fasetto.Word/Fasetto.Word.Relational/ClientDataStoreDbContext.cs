using Fasetto.Word.Core;
using Microsoft.EntityFrameworkCore;

namespace Fasetto.Word.Relational
{
    /// <summary>
    /// The database context for the client data store
    /// </summary>
    public class ClientDataStoreDbContext : DbContext
    {
        #region DbSets

        /// <summary>
        /// The client login credentials
        /// </summary>
        public DbSet<LoginCredentialsDataModel> LoginCredentials { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Construtor
        /// </summary>
        public ClientDataStoreDbContext(DbContextOptions<ClientDataStoreDbContext> options) : base(options) { }

        #endregion

        #region Model Creation

        /// <summary>
        /// Configures the database structures and relationships
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API

            // Configure LoginCredentials
            // --------------------------
            // 
            // Set Id as primary key
            modelBuilder.Entity<LoginCredentialsDataModel>().HasKey(a => a.Id);
            modelBuilder.Entity<LoginCredentialsDataModel>().Property(a => a.Id).ValueGeneratedOnAdd();

            // TODO: Set up limits
            //modelBuilder.Entity<LoginCredentialsDataModel>().Property( a => a.FirstName).HasMaxLength(50);

        }

        #endregion
    }
}
