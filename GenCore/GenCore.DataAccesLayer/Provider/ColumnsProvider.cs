using GenCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenCore.DataAccesLayer.Provider
{
    public interface IColumnsProvider : IBaseProvider<Columns>
    {
        List<Columns> GetAllForCatalogAndTableName(string pCatalog, string pTableName);
    }

    internal class ColumnsProvider : IColumnsProvider
    {
        private readonly IRepository<Columns> _repository;
        public ColumnsProvider(IRepository<Columns> pRepository)
        {
            _repository = pRepository;
        }

        public void Create(Columns pEntity)
        {
            _repository.Create(pEntity);
        }

        public void Delete(Guid pId)
        {
            _repository.Delete(pId);
        }

        public IList<Columns> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public List<Columns> GetAllForCatalogAndTableName(string pCatalog, string pTableName)
        {
            return _repository.GetAll().Where(x => x.TABLE_CATALOG == pCatalog && x.TABLE_NAME == pTableName).ToList();
        }

        public Columns GetById(Guid pId)
        {
            return _repository.GetById(pId);
        }

        public void Update(Columns entity)
        {
            _repository.Update(entity);
        }
    }
}
