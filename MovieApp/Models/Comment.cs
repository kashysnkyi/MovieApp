using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Models;

[Table("comments")]
public partial class Comment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("userid")]
    public int? Userid { get; set; }

    [Column("filmid")]
    public int? Filmid { get; set; }

    [Column("text")]
    public string Text { get; set; } = null!;

    [Column("createddate", TypeName = "timestamp without time zone")]
    public DateTime? Createddate { get; set; }

    [ForeignKey("Filmid")]
    [InverseProperty("Comments")]
    public virtual Film? Film { get; set; }

    [ForeignKey("Userid")]
    [InverseProperty("Comments")]
    public virtual User? User { get; set; }
}
