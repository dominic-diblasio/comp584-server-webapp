using champsModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace champsProjectServer.Dtos
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; }
        public string Region { get; set; }
    }
}