//using Coffee_Manage.Domain;
//using System.Threading.Tasks;

//namespace Coffee_Manage.Infrastructure
//{
//    public class UnitOfWork : IUnitOfWork
//    {
//        private readonly DbContext _context;

//        public UnitOfWork(DbContext context)
//        {
//            _context = context;
//        }

//        public async Task<int> SaveChangesAsync()
//        {
//            return await _context.SaveChangesAsync();
//        }

//        public void Dispose()
//        {
//            _context.Dispose();
//        }
//    }
//}
