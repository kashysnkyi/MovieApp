using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Models;

[Table("watchhistories")]
public partial class Watchhistory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("userid")]
    public int? Userid { get; set; }

    [Column("filmid")]
    public int? Filmid { get; set; }

    [Column("watcheddate", TypeName = "timestamp without time zone")]
    public DateTime? Watcheddate { get; set; }

    [Column("watchduration")]
    public int? Watchduration { get; set; }

    [ForeignKey("Filmid")]
    [InverseProperty("Watchhistories")]
    public virtual Film? Film { get; set; }

    [ForeignKey("Userid")]
    [InverseProperty("Watchhistories")]
    public virtual User? User { get; set; }
}
