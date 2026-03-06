using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Models;

[Table("filmgenres")]
public partial class Filmgenre
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("filmid")]
    public int? Filmid { get; set; }

    [Column("genreid")]
    public int? Genreid { get; set; }

    [ForeignKey("Filmid")]
    [InverseProperty("Filmgenres")]
    public virtual Film? Film { get; set; }

    [ForeignKey("Genreid")]
    [InverseProperty("Filmgenres")]
    public virtual Genre? Genre { get; set; }
}
