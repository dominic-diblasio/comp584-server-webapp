using System.Diagnostics.Metrics;
using champsModel;

namespace champsProjectServer.Dtos
{
    public class TournamentDTO
    {
        public string name { get; set; } = null!;
        public int participatingteams { get; set; }
        public int maxteams { get; set; }
        public int gameid { get; set; }
        public int prizepool { get; set; }
        public DateTime starttime { get; set; }
        public DateTime endtime { get; set; }

    }
}