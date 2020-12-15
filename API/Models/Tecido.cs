﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace API.Models
{
    public partial class Tecido
    {
        public Tecido()
        {
            Produto = new HashSet<Produto>();
        }

        [Key]
        public short Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCriacao { get; set; }

        [InverseProperty("Tecido")]
        public virtual ICollection<Produto> Produto { get; set; }
    }
}