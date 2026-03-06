using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Models;

[Table("ratings")]
public partial class Rating
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("userid")]
    public int? Userid { get; set; }

    [Column("filmid")]
    public int? Filmid { get; set; }

    [Column("score")]
    public int? Score { get; set; }

    [Column("rateddate", TypeName = "timestamp without time zone")]
    public DateTime? Rateddate { get; set; }

    [ForeignKey("Filmid")]
    [InverseProperty("Ratings")]
    public virtual Film? Film { get; set; }

    [ForeignKey("Userid")]
    [InverseProperty("Ratings")]
    public virtual User? User { get; set; }
}
