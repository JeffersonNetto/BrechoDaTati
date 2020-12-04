﻿using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Condicao> Condicao { get; set; }
        public virtual DbSet<Manga> Manga { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Modelagem> Modelagem { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<Tamanho> Tamanho { get; set; }
        public virtual DbSet<Tecido> Tecido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {                
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.HasQueryFilter(_ => _.Ativo);                
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");                

                entity.Property(e => e.Celular)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Cpf)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Senha).IsUnicode(false);

                entity.Property(e => e.Sobrenome).IsUnicode(false);

                entity.HasQueryFilter(_ => _.Ativo);
            });

            modelBuilder.Entity<Condicao>(entity =>
            {                
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.HasQueryFilter(_ => _.Ativo);
            });

            modelBuilder.Entity<Manga>(entity =>
            {                
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.HasQueryFilter(_ => _.Ativo);
            });

            modelBuilder.Entity<Marca>(entity =>
            {                
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.HasQueryFilter(_ => _.Ativo);
            });

            modelBuilder.Entity<Modelagem>(entity =>
            {                
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.HasQueryFilter(_ => _.Ativo);
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                
                entity.Property(e => e.Cor).IsUnicode(false);

                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.Property(e => e.Medidas).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Observacoes).IsUnicode(false);

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.CategoriaId)
                    .HasConstraintName("FK_Produto_Categoria");

                entity.HasOne(d => d.Condicao)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.CondicaoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Produto__Condica__6C190EBB");

                entity.HasOne(d => d.Manga)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.MangaId)
                    .HasConstraintName("FK__Produto__MangaId__787EE5A0");

                entity.HasOne(d => d.Marca)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.MarcaId)
                    .HasConstraintName("FK_Produto_Marca");

                entity.HasOne(d => d.Modelagem)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.ModelagemId)
                    .HasConstraintName("FK__Produto__Modelag__797309D9");

                entity.HasOne(d => d.Tamanho)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.TamanhoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Produto__Tamanho__5FB337D6");

                entity.HasOne(d => d.Tecido)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.TecidoId)
                    .HasConstraintName("FK__Produto__TecidoI__7A672E12");

                entity.HasQueryFilter(_ => _.Ativo);
            });

            modelBuilder.Entity<Tamanho>(entity =>
            {                
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.HasQueryFilter(_ => _.Ativo);
            });

            modelBuilder.Entity<Tecido>(entity =>
            {                
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.HasQueryFilter(_ => _.Ativo);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}