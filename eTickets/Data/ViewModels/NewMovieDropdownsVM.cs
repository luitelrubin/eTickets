using eTickets.Models;

namespace eTickets.Data.ViewModels
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM()
        {
            DirectorList = new List<Director>();
            CinemaList = new List<Cinema>();
            ActorList = new List<Actor>();
        }
        public List<Director> DirectorList { get; set; }
        public List<Cinema> CinemaList { get; set; }
        public List<Actor> ActorList { get; set; }
    }
}
