﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XPTO.Data;

namespace XPTO.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220505174945_PrimeiraMigracao")]
    partial class PrimeiraMigracao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("XPTO.Models.CaixaEletronico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Notas10")
                        .HasColumnType("int");

                    b.Property<int>("Notas100")
                        .HasColumnType("int");

                    b.Property<int>("Notas20")
                        .HasColumnType("int");

                    b.Property<int>("Notas50")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CaixaEletronicos");
                });

            modelBuilder.Entity("XPTO.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NumeroCartao")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<int>("Saldo")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
