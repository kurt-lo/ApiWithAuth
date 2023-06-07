using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ApiWithAuth.Entities
{
    public partial class ApiContext : DbContext
    {
        public ApiContext() { } //optional constructor

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { } //required constructor
        
        // put your database sets here
        public DbSet<User> Users{ get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<UserExpense> UserExpense_tbl { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserExpense>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("userexpense_tbl");

                entity.Property(e => e.Firstname).HasMaxLength(45);
                entity.Property(e => e.Lastname).HasMaxLength(45);
                entity.Property(e => e.Username).HasMaxLength(45);
                entity.Property(e => e.Email).HasMaxLength(45);
                entity.Property(e => e.Password).HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserExpense>().ToTable("userexpense_tbl");
        }*/
    }
}
