namespace MyNotes.Entity
{
    using System.Data.Entity;

    public partial class NoteDbContext : DbContext
    {
        public NoteDbContext()
            : base("name=NoteDbContext")
        {
        }

        public virtual DbSet<tbl_Notes> tbl_Notes { get; set; }
        public virtual DbSet<tbl_User> tbl_User { get; set; }
        public virtual DbSet<tbl_Emails> tbl_Emails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
