using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace champsProjectServer.Dtos
{
    public class UsersDTO
    {
        public string username { get; set; } = null!;
        public string password { get; set; } = null!;
        public string name { get; set; } = null!;
        public string region { get; set; } = null!;
        public int? teamid { get; set; }
        public int matchplays { get; set; }
        public int matchwins { get; set; }
        public int tournamentplays { get; set; }
        public int tournamentwins { get; set; }
    }
}