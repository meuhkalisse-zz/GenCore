using GenCore.Domain.Base;

namespace GenCore.Domain
{
    public class Columns : IBaseEntity
    {
        public virtual string COLUMN_NAME { get; set; }
        public virtual int ORDINAL_POSITION { get; set; }
        public virtual string COLUMN_DEFAULT { get; set; }
        public virtual string IS_NULLABLE { get; set; }
        public virtual string DATA_TYPE { get; set; }
        public virtual int CHARACTER_MAXIMUM_LENGTH { get; set; }
        public virtual int NUMERIC_PRECISION { get; set; }
        public virtual int NUMERIC_SCALE { get; set; }
        public virtual string TABLE_CATALOG { get; set; }
        public virtual string TABLE_NAME { get; set; }
    }
}
