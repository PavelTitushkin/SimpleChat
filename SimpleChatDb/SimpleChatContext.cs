using Microsoft.EntityFrameworkCore;
using SimpleChat_Db.Entities;

namespace SimpleChat_Db
{
    public class SimpleChatContext : DbContext
    {
        public SimpleChatContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Connection> Connection { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>().ToTable(tb => tb.HasTrigger("TriggerConnection"));
            modelBuilder.Entity<Message>().ToTable(tb => tb.HasTrigger("TriggerMessage"));
            base.OnModelCreating(modelBuilder);
        }
    }
}
