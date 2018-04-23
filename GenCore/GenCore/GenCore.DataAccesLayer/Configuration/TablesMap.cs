using FluentNHibernate.Mapping;
using GenCore.Domain;

namespace GenCore.DataAccesLayer.Configuration
{
    internal class TablesMap : ClassMap<Tables>
    {
        public TablesMap()
        {
            Schema("INFORMATION_SCHEMA");
            Map(x => x.SCHEMA);
            Map(x => x.TABLE_CATALOG);
            Map(x => x.TABLE_NAME);
            Map(x => x.TABLE_TYPE);
        }
    }
}
