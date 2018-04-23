using GenCore.Domain.Base;

namespace GenCore.Domain
{
    public class Tables : IBaseEntity
    {
        public virtual string TABLE_CATALOG { get; set; }
        public virtual string SCHEMA { get; set; }
        public virtual string TABLE_NAME { get; set; }
        public virtual string TABLE_TYPE { get; set; }
    }
}
