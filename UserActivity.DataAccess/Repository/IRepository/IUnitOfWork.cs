using System.Threading.Tasks;
using UserActivity.DataAccess.Repository.IRepository;

namespace UserActivity.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IStatusRepository Status {  get; }
        IApplicationUserRepository ApplicationUser {  get; }
        IUserLoginRepository UserLogins {  get; }

        void Save();
    }
}
