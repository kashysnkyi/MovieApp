using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Models;

[Table("films")]
public partial class Film
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("duration")]
    public int? Duration { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("releasedate")]
    public DateOnly? Releasedate { get; set; }

    [InverseProperty("Film")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("Film")]
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    [InverseProperty("Film")]
    public virtual ICollection<Filmgenre> Filmgenres { get; set; } = new List<Filmgenre>();

    [InverseProperty("Film")]
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    [InverseProperty("Film")]
    public virtual ICollection<Watchhistory> Watchhistories { get; set; } = new List<Watchhistory>();
}
