﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CaraEntites
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CaraEntities : DbContext
    {
        public CaraEntities()
            : base("name=CaraEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Country> Countries { get; set; }
        public DbSet<UserToConfirm> UserToConfirms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FilmAltTitle> FilmAltTitles { get; set; }
        public DbSet<FilmSubmission> FilmSubmissions { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}
