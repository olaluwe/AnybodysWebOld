﻿using AnybodysModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AnybodysWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        /*public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }*/
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
