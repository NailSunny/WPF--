﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPF_CactusProject_2024.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Cactus_ProjectEntities1 : DbContext
    {
        public Cactus_ProjectEntities1()
            : base("name=Cactus_ProjectEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cactus> Cactus { get; set; }
        public virtual DbSet<Cactus_Vistavka> Cactus_Vistavka { get; set; }
        public virtual DbSet<Vid> Vid { get; set; }
        public virtual DbSet<Vistavka> Vistavka { get; set; }
    }
}