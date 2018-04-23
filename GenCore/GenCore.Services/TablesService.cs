using GenCore.DataAccesLayer.Provider;
using GenCore.Domain;
using System.Collections.Generic;
using System.Linq;

namespace GenCore.Services
{
    public interface ITablesService
    {
        List<Tables> GetAll();
    }

    internal class TablesService : ITablesService
    {
        private readonly ITablesProvider _tablesProvider;

        public TablesService(ITablesProvider pTablesProvider)
        {
            _tablesProvider = pTablesProvider;
        }

        public List<Tables> GetAll()
        {
            return _tablesProvider.GetAll().ToList();
        }
    }
}
