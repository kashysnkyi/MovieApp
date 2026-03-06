using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Models;

public partial class MovieDbContext : DbContext
{
    public MovieDbContext()
    {
    }

    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Filmgenre> Filmgenres { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Watchhistory> Watchhistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=cinema_db;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comments_pkey");

            entity.Property(e => e.Createddate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Film).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("comments_filmid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("comments_userid_fkey");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("favorites_pkey");

            entity.Property(e => e.Addeddate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Film).WithMany(p => p.Favorites)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("favorites_filmid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("favorites_userid_fkey");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("films_pkey");
        });

        modelBuilder.Entity<Filmgenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("filmgenres_pkey");

            entity.HasOne(d => d.Film).WithMany(p => p.Filmgenres)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("filmgenres_filmid_fkey");

            entity.HasOne(d => d.Genre).WithMany(p => p.Filmgenres)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("filmgenres_genreid_fkey");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genres_pkey");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ratings_pkey");

            entity.Property(e => e.Rateddate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Film).WithMany(p => p.Ratings)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ratings_filmid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ratings_userid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.Property(e => e.Createddate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Watchhistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("watchhistories_pkey");

            entity.Property(e => e.Watcheddate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Film).WithMany(p => p.Watchhistories)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("watchhistories_filmid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Watchhistories)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("watchhistories_userid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
