namespace MyProject.Services.ORM
{
    public class BaseDao<T> : PetaPocoRepository<T> where T : new()
    {
        public BaseDao()
        {
            this.ConnectionStringName = DbFactory.GetKeyFrom(this);
        }
    }
}