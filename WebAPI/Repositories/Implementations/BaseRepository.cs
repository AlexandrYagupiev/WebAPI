using WebAPI.Models.Base;
using WebAPI.Repositories.Interfases;

namespace WebAPI.Repositories.Implementations
{
    public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseModel
    {
        private ApplicationContext Context { get; set; }
        public BaseRepository(ApplicationContext context)
        {
            Context = context;
        }

        public TDbModel Get(Guid id)
        {
            return Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
        }

        public List<TDbModel> GetAll()
        {
            return Context.Set<TDbModel>().ToList();
        }

        public TDbModel Create(TDbModel model)
        {
            Context.Set<TDbModel>().Add(model);
            Context.SaveChanges();
            return model;
        }

        public void Delete(Guid id)
        {
            var toDelete = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
            Context.Set<TDbModel>().Remove(toDelete);
            Context.SaveChanges();
        }

        public TDbModel Update(TDbModel model)
        {
            var toUpdate = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == model.Id);
            if (toUpdate != null)
            {
                toUpdate = model;
            }
            Context.Update(toUpdate);
            Context.SaveChanges();
            return toUpdate;
        }      
    }
}
