using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace champsModel;

[Table("Team")]
public partial class Team
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(3)]
    [Unicode(false)]
    public string Region { get; set; } = null!;

    public int MaxMembers { get; set; }

    public int TournamentsEntered { get; set; }

    public int TournamentWins { get; set; }
}
