﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ms_aula;

#nullable disable

namespace ms_aula.Migrations
{
    [DbContext(typeof(AulaDbContext))]
    [Migration("20230917131147_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ms_aula.Domains.AreaFisica", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AreaFisica");
                });

            modelBuilder.Entity("ms_aula.Domains.Aula", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AreaFisicaId")
                        .HasColumnType("bigint");

                    b.Property<long>("Curtido")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("Favoritado")
                        .HasColumnType("bigint");

                    b.Property<long>("ProfessorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AreaFisicaId");

                    b.ToTable("Aula");
                });

            modelBuilder.Entity("ms_aula.Domains.AulaComentario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AulaId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AulaId");

                    b.ToTable("AulaComentario");
                });

            modelBuilder.Entity("ms_aula.Domains.AulaFavoritada", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AulaId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AulaId");

                    b.ToTable("AulaFavoritada");
                });

            modelBuilder.Entity("ms_aula.Domains.AulaSessao", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AulaId")
                        .HasColumnType("bigint");

                    b.Property<int>("AulaSessaoTipo")
                        .HasColumnType("integer");

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("Favoritado")
                        .HasColumnType("bigint");

                    b.Property<long>("Ordem")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AulaId");

                    b.ToTable("AulaSessao");
                });

            modelBuilder.Entity("ms_aula.Domains.AulaSessaoFavoritada", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AulaSessaoId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AulaSessaoId");

                    b.ToTable("AulaSessaoFavoritada");
                });

            modelBuilder.Entity("ms_aula.Domains.WidgetConcluido", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AulaId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AulaId");

                    b.ToTable("WidgetConcluido");
                });

            modelBuilder.Entity("ms_aula.Domains.WidgetCursando", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AulaId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AulaId");

                    b.ToTable("WidgetCursando");
                });

            modelBuilder.Entity("ms_aula.Domains.WidgetCursar", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AulaId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AulaId");

                    b.ToTable("WidgetCursar");
                });

            modelBuilder.Entity("ms_aula.Domains.Aula", b =>
                {
                    b.HasOne("ms_aula.Domains.AreaFisica", "AreaFisica")
                        .WithMany("AulaMany")
                        .HasForeignKey("AreaFisicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AreaFisica");
                });

            modelBuilder.Entity("ms_aula.Domains.AulaComentario", b =>
                {
                    b.HasOne("ms_aula.Domains.Aula", "Aula")
                        .WithMany("AulaComentarioMany")
                        .HasForeignKey("AulaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aula");
                });

            modelBuilder.Entity("ms_aula.Domains.AulaFavoritada", b =>
                {
                    b.HasOne("ms_aula.Domains.Aula", "Aula")
                        .WithMany()
                        .HasForeignKey("AulaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aula");
                });

            modelBuilder.Entity("ms_aula.Domains.AulaSessao", b =>
                {
                    b.HasOne("ms_aula.Domains.Aula", "Aula")
                        .WithMany("AulaSessaoMany")
                        .HasForeignKey("AulaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aula");
                });

            modelBuilder.Entity("ms_aula.Domains.AulaSessaoFavoritada", b =>
                {
                    b.HasOne("ms_aula.Domains.AulaSessao", "AulaSessao")
                        .WithMany()
                        .HasForeignKey("AulaSessaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AulaSessao");
                });

            modelBuilder.Entity("ms_aula.Domains.WidgetConcluido", b =>
                {
                    b.HasOne("ms_aula.Domains.Aula", "Aula")
                        .WithMany()
                        .HasForeignKey("AulaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aula");
                });

            modelBuilder.Entity("ms_aula.Domains.WidgetCursando", b =>
                {
                    b.HasOne("ms_aula.Domains.Aula", "Aula")
                        .WithMany()
                        .HasForeignKey("AulaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aula");
                });

            modelBuilder.Entity("ms_aula.Domains.WidgetCursar", b =>
                {
                    b.HasOne("ms_aula.Domains.Aula", "Aula")
                        .WithMany()
                        .HasForeignKey("AulaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aula");
                });

            modelBuilder.Entity("ms_aula.Domains.AreaFisica", b =>
                {
                    b.Navigation("AulaMany");
                });

            modelBuilder.Entity("ms_aula.Domains.Aula", b =>
                {
                    b.Navigation("AulaComentarioMany");

                    b.Navigation("AulaSessaoMany");
                });
#pragma warning restore 612, 618
        }
    }
}
