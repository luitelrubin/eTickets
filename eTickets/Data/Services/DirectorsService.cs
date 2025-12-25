using eTickets.Data.Base;
using eTickets.Models;
namespace eTickets.Data.Services
{
    public class DirectorsService : EntityBaseRepository<Director>, IDirectorsService
    {
        public DirectorsService(AppDbContext context) : base(context)
        {

        }
    }
}
