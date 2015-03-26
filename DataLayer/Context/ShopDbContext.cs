
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using DomainClasses.Configuraion;
using DomainClasses.Entities;
using EFSecondLevelCache;
namespace DataLayer.Context
{
    public class ShopDbContext : DbContext, IUnitOfWork
    {
        public ShopDbContext()
            : base("GoldenCityShop")
        {

        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SiteOption> SiteOptions { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Value> SpecificationValues { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<ProductImage> Images { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<SlideShow> SlideShows { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CommentConfig());
            modelBuilder.Configurations.Add(new SlideShowConfig());
            modelBuilder.Configurations.Add(new ProductConfig());
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new ShoppingCartConfig());
            modelBuilder.Configurations.Add(new RoleConfig());
            modelBuilder.Configurations.Add(new OrderConfig());
            modelBuilder.Configurations.Add(new OrderDetailConfig());
            modelBuilder.Configurations.Add(new ImageConfig());
            modelBuilder.Configurations.Add(new AttributeConfig());
            modelBuilder.Configurations.Add(new FolderConfig());
            modelBuilder.Configurations.Add(new CategoryConfig());
            modelBuilder.Configurations.Add(new ValueConfig());
            modelBuilder.Configurations.Add(new ContactConfig());
            modelBuilder.Configurations.Add(new PageConfig());
            modelBuilder.Configurations.Add(new SiteOptionConfig());
            base.OnModelCreating(modelBuilder);
        }

        #region UnitOfWork
        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public void ForceDatabaseInitialize()
        {
            Database.Initialize(true);
        }

        public override int SaveChanges()
        {
            return SaveAllChanges();
        }

        public int SaveAllChanges(bool invalidateCacheDependencies = true)
        {
            var changedEntityNames = GetChangedEntityNames();
            var result = base.SaveChanges();
            if (invalidateCacheDependencies)
            {
                new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
            }
            return result;
        }

        private string[] GetChangedEntityNames()
        {
            return ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added ||
                            x.State == EntityState.Modified ||
                            x.State == EntityState.Deleted)
                .Select(x => ObjectContext.GetObjectType(x.Entity.GetType()).FullName)
                .Distinct()
                .ToArray();
        }
        public override Task<int> SaveChangesAsync()
        {
            return SaveAllChangesAsync();
        }

        public Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true)
        {
            var changedEntityNames = GetChangedEntityNames();
            var result = base.SaveChangesAsync();
            if (invalidateCacheDependencies)
            {
                new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
            }
            return result;
        }
       
        public void AutoDetectChangesEnabled(bool flag = true)
        {
            Configuration.AutoDetectChangesEnabled = flag;
        }
        #endregion //UnitOfWork


    }
}
