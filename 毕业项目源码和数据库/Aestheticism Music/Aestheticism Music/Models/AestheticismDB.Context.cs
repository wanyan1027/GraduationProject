﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aestheticism_Music.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AestheticismMSEntities : DbContext
    {
        public AestheticismMSEntities()
            : base("name=AestheticismMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Administer> Administer { get; set; }
        public virtual DbSet<Discuss> Discuss { get; set; }
        public virtual DbSet<MusicList> MusicList { get; set; }
        public virtual DbSet<MusicListType> MusicListType { get; set; }
        public virtual DbSet<Singer> Singer { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<UserMusic> UserMusic { get; set; }
    }
}
