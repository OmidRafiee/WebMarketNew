using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses;
using WebMarket.DomainClasses.Configuraion;
using WebMarket.DomainClasses.Entity;

namespace WebMarket.DataLayer.Context
{
    public class MarketDbContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>,
        IUnitOfWork
    {

        public MarketDbContext()
            : base("WebMarket")
        {

        }

        public DbSet<Group> Groups { set; get; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<FactorItem> FactorItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Factor> Factors { set; get; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Reseller> Resellers { set; get; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { set; get; }
       
        #region Overrides of DbContext

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly ( System.Reflection.Assembly.GetAssembly ( typeof(FactorConfig) ) );

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<CustomRole>().ToTable("Roles");
            modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<CustomUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogins");

            //modelBuilder.Configurations.Add(new GroupConfig());
            //modelBuilder.Configurations.Add(new FactorItemConfig());
            //modelBuilder.Configurations.Add(new ProductConfig());
            //modelBuilder.Configurations.Add(new FactorConfig());
            //modelBuilder.Configurations.Add(new UserConfig());
            //modelBuilder.Configurations.Add(new ResellerConfig());
            //modelBuilder.Configurations.Add(new CityConfig());
            //modelBuilder.Configurations.Add(new StateConfig());

          
        }

        #endregion

        #region IUnitOfWork

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void ForceDatabaseInitialize()
        {
            using (var context = new MarketDbContext())
            {
                context.Database.Initialize(force: true);
            }
        }

        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        #endregion //UnitOfWork
    }
}
