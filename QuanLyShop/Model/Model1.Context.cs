﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace QuanLyShop.Model
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class QUANLYSHOPEntities : DbContext
{
    public QUANLYSHOPEntities()
        : base("name=QUANLYSHOPEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<CTHD> CTHD { get; set; }

    public virtual DbSet<HOADON> HOADON { get; set; }

    public virtual DbSet<KHACHHANG> KHACHHANG { get; set; }

    public virtual DbSet<NHANVIEN> NHANVIEN { get; set; }

    public virtual DbSet<NHAPHANG> NHAPHANG { get; set; }

    public virtual DbSet<SANPHAM> SANPHAM { get; set; }

    public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

    public virtual DbSet<THELOAI> THELOAI { get; set; }

}

}

