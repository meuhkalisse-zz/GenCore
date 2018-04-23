using GenCore.Domain.Base;

namespace GenCore.Domain
{
    public class Columns : IBaseEntity
    {
        public virtual int ORDINAL_POSITION { get; set; }
        public virtual object COLUMN_DEFAULT { get; set; }
        public virtual bool IS_NULLABLE { get; set; }
        public virtual string DATA_TYPE { get; set; }
        public virtual int CHARACTER_MAXIMUM_LENGTH { get; set; }
        public virtual int NUMERIC_PRECISION { get; set; }
        public virtual int NUMERIC_SCALE { get; set; }
    }
}
