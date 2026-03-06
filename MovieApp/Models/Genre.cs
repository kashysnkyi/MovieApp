using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Models;

[Table("genres")]
[Index("Name", Name = "genres_name_key", IsUnique = true)]
public partial class Genre
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("Genre")]
    public virtual ICollection<Filmgenre> Filmgenres { get; set; } = new List<Filmgenre>();
}
