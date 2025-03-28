using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace champsModel;

[Table("User")]
public partial class User 
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(3)]
    [Unicode(false)]
    public string Region { get; set; } = null!;

    // Nullable value TeamId (Could have no associated team)
    [Column("TeamID")]
    public int? TeamId { get; set; }

    public int MatchPlays { get; set; }

    public int MatchWins { get; set; }

    public int TournamentPlays { get; set; }

    public int TournamentWins { get; set; }

}
