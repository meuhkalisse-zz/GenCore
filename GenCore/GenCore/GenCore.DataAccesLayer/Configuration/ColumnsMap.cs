using FluentNHibernate.Mapping;
using GenCore.Domain;

namespace GenCore.DataAccesLayer.Configuration
{
    internal class ColumnsMap : ClassMap<Columns>
    {
        public ColumnsMap()
        {
            Schema("INFORMATION_SCHEMA");
            Id(x => x.COLUMN_NAME);
            Map(x => x.CHARACTER_MAXIMUM_LENGTH);
            Map(x => x.COLUMN_DEFAULT);
            Map(x => x.DATA_TYPE);
            Map(x => x.IS_NULLABLE);
            Map(x => x.NUMERIC_PRECISION);
            Map(x => x.NUMERIC_SCALE);
            Map(x => x.ORDINAL_POSITION);
        }
    }
}
