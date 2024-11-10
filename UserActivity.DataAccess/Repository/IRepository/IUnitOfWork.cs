using System.Threading.Tasks;
using UserActivity.DataAccess.Repository.IRepository;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IStatusRepository Status {  get; }
        IApplicationUserRepository ApplicationUser {  get; }

        void Save();
    }
}
