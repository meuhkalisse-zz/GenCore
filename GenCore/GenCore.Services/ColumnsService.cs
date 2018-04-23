using GenCore.DataAccesLayer.Provider;
using GenCore.Domain;
using System.Collections.Generic;
using System.Linq;

namespace GenCore.Services
{
    public interface IColumnsService
    {
        List<Columns> GetAll();
        List<Columns> GetAllByCatalogAndTableName(string pCatalog, string pTableName);
    }

    internal class ColumnsService : IColumnsService
    {
        private readonly IColumnsProvider _columnsProvider;

        public ColumnsService(IColumnsProvider pColumnsProvider)
        {
            _columnsProvider = pColumnsProvider;
        }

        public List<Columns> GetAll()
        {
            return _columnsProvider.GetAll().ToList();
        }

        public List<Columns> GetAllByCatalogAndTableName(string pCatalog, string pTableName)
        {
            return _columnsProvider.GetAllForCatalogAndTableName(pCatalog, pTableName);
        }
    }
}
