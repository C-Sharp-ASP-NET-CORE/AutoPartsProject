using AutoParts.Infrastructure.Data.Common;

namespace AutoParts.Infrastructure.Data.Repositories
{
    public class ApplicationDbRepository : Repository, IApplicationDbRepository
    {
        public ApplicationDbRepository(AutoPartsDbContext context)
        {
            this.Context = context;
        }
    }
}