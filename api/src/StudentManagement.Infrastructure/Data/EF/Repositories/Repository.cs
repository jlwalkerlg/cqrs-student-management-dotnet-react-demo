namespace StudentManagement.Infrastructure.Data.EF.Repositories
{
    public abstract class Repository
    {
        protected readonly AppDbContext context;

        public Repository(AppDbContext context)
        {
            this.context = context;
        }
    }
}
