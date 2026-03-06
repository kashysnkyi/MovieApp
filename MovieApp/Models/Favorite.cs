using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Models;

[Table("favorites")]
public partial class Favorite
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("userid")]
    public int? Userid { get; set; }

    [Column("filmid")]
    public int? Filmid { get; set; }

    [Column("addeddate", TypeName = "timestamp without time zone")]
    public DateTime? Addeddate { get; set; }

    [ForeignKey("Filmid")]
    [InverseProperty("Favorites")]
    public virtual Film? Film { get; set; }

    [ForeignKey("Userid")]
    [InverseProperty("Favorites")]
    public virtual User? User { get; set; }
}
