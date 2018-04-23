using GenCore.DataAccesLayer.Provider;
using GenCore.Domain;
using System.Collections.Generic;
using System.Linq;

namespace GenCore.Services
{
    public interface IColumnsService
    {
        List<Columns> GetAll();
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
    }
}
