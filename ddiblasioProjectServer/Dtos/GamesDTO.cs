using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace champsProjectServer.Dtos
{
    public class GamesDTO
    {
        public string name { get; set; } = null!;
        public string? genre { get; set; } = null!;
        public int? teamsize { get; set; }
    }
}