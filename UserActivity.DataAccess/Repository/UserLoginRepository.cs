using UserActivity.DataAccess.Repository.IRepository;
using UserActivity.Models;


namespace UserActivity.DataAccess.Repository
{
    public class UserLoginRepository : Repository<UserLogin>, IUserLoginRepository
    {
        private readonly ApplicationDbContext _db;

        public UserLoginRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
    }
}
