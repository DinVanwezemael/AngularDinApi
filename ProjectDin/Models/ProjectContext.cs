﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectDin.Models;

namespace ProjectDin.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) { }
        
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Optie> Opties { get; set; }
        public DbSet<Antwoord> Antwoorden { get; set; }
        public DbSet<PollUser> PollUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gebruiker>().ToTable("Gebruiker");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Friend>().ToTable("Friend");
            modelBuilder.Entity<Poll>().ToTable("Poll");
            modelBuilder.Entity<Optie>().ToTable("Optie");
            modelBuilder.Entity<Antwoord>().ToTable("Antwoord");
            modelBuilder.Entity<PollUser>().ToTable("PollUser");
        }

        public DbSet<ProjectDin.Models.Poll> Poll { get; set; }
    }
}
