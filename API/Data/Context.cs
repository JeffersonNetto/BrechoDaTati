﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using API.Models;

#nullable disable

namespace API.Data
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<ClienteProdutoFavorito> ClienteProdutoFavorito { get; set; }
        public virtual DbSet<Condicao> Condicao { get; set; }
        public virtual DbSet<Cupom> Cupom { get; set; }
        public virtual DbSet<Manga> Manga { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Modelagem> Modelagem { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<PedidoItem> PedidoItem { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<StatusPedido> StatusPedido { get; set; }
        public virtual DbSet<Tamanho> Tamanho { get; set; }
        public virtual DbSet<Tecido> Tecido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nome).IsUnicode(false);
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
            });

            modelBuilder.Entity<ClienteProdutoFavorito>(entity =>
            {
                entity.HasKey(e => new { e.ClienteId, e.ProdutoId });

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.ClienteProdutoFavorito)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClienteProdutoFavorito_Cliente");

                entity.HasOne(d => d.Produto)
                    .WithMany(p => p.ClienteProdutoFavorito)
                    .HasForeignKey(d => d.ProdutoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClienteProdutoFavorito_Produto");
            });

            modelBuilder.Entity<Condicao>(entity =>
            {
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descricao).IsUnicode(false);
            });

            modelBuilder.Entity<Cupom>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descricao).IsUnicode(false);
            });

            modelBuilder.Entity<Manga>(entity =>
            {
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descricao).IsUnicode(false);
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nome).IsUnicode(false);
            });

            modelBuilder.Entity<Modelagem>(entity =>
            {
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descricao).IsUnicode(false);
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedido_Cliente");

                entity.HasOne(d => d.Cupom)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.CupomId)
                    .HasConstraintName("FK_Pedido_Cupom");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedido_StatusPedido");
            });

            modelBuilder.Entity<PedidoItem>(entity =>
            {
                entity.HasKey(e => new { e.PedidoId, e.ProdutoId });

                entity.HasOne(d => d.Pedido)
                    .WithMany(p => p.PedidoItem)
                    .HasForeignKey(d => d.PedidoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PedidoItem_Pedido");

                entity.HasOne(d => d.Produto)
                    .WithMany(p => p.PedidoItem)
                    .HasForeignKey(d => d.ProdutoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PedidoItem_Produto");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CondicaoId).HasDefaultValueSql("(CONVERT([smallint],(0)))");

                entity.Property(e => e.Cor)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.Property(e => e.Medidas)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Observacoes).IsUnicode(false);

                entity.Property(e => e.TamanhoId).HasDefaultValueSql("(CONVERT([smallint],(0)))");

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
            });

            modelBuilder.Entity<StatusPedido>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descricao).IsUnicode(false);
            });

            modelBuilder.Entity<Tamanho>(entity =>
            {
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descricao).IsUnicode(false);
            });

            modelBuilder.Entity<Tecido>(entity =>
            {
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descricao).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}