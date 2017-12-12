using System;
using EmailProcessing.Repository.Abstract;
using EmailProcessing.DAL.Entities;
using System.Linq;
using EmailProcessing.DAL;
using Microsoft.EntityFrameworkCore;

namespace EmailProcessing.Repository.Concrete
{
    public class EmailProcessingRepository : IBaseRepository<Setting>
    {
        private readonly AppDbContext _appContext;
        public EmailProcessingRepository(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public Setting Add(Setting entity)
        {
            _appContext.Serrings.Add(entity);
            _appContext.SaveChanges();
            return entity;
        }

        public IQueryable<Setting> Find()
        {
            return _appContext.Serrings.Include(c => c.ParamSettings);
        }

        public IQueryable<Setting> Find(string filter)
        {
            throw new NotImplementedException();
        }

        public Setting FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Setting> FindPage(int page, int pageSize, string sort, string order, string filter)
        {
            throw new NotImplementedException();
        }

        public Setting Remove(Setting entity)
        {
            throw new NotImplementedException();
        }

        public Setting Update(Setting entity)
        {
            throw new NotImplementedException();
        }
    }
}
