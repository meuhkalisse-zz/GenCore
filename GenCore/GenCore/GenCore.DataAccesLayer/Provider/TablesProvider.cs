using GenCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenCore.DataAccesLayer.Provider
{
    public interface ITablesProvider : IBaseProvider<Tables>
    {
    }

    internal class TablesProvider : ITablesProvider
    {
        private readonly IRepository<Tables> _repository;
        public TablesProvider(IRepository<Tables> pRepository)
        {
            _repository = pRepository;
        }

        public void Create(Tables pEntity)
        {
            _repository.Create(pEntity);
        }

        public void Delete(Guid pId)
        {
            _repository.Delete(pId);
        }

        public IList<Tables> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Tables GetById(Guid pId)
        {
            return _repository.GetById(pId);
        }

        public void Update(Tables entity)
        {
            _repository.Update(entity);
        }
    }
}
