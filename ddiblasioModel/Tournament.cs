using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace champsModel;

[Table("Tournament")]
public partial class Tournament 
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public int ParticipatingTeams {  get; set; }

    public int MaxTeams { get; set; }

    [Column("GameID")]
    public int GameId { get; set; }

    public int PrizePool { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
