using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fasetto.Word.Web.Server
{
    /// <summary>
    /// The database represntaional model for our application
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Public Properties

        /// <summary>
        /// The settings for the application
        /// </summary>
        public DbSet<SettingsDataModel> Settings { get; set; }

        #endregion

        #region Constractor

        /// <summary>
        /// Default constractor, expecting database options passed in
        /// </summary>
        /// <param name="options">The database context options</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // this is how u can make some thing diffrent from default in database
            // soul1: add this by data annotations [tags] in model
            // soul2: Fluent API
            // this should make an index for the name column
            // there is some things u can't make by annotations like index, but u can make anything by Fluent API
            // Fluent API
            modelBuilder.Entity<SettingsDataModel>().HasIndex(a => a.Name);
        }

    }
}
