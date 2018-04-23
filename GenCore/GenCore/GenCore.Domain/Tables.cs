using GenCore.Domain.Base;

namespace GenCore.Domain
{
    public class Tables : IBaseEntity
    {
        public virtual string TABLE_CATALOG { get; set; }
        public virtual string SCHEMA { get; set; }
        public virtual string TABLE_NAME { get; set; }
        public virtual string TABLE_TYPE { get; set; }

        public override bool Equals(object obj)
        {
            return TABLE_NAME.Equals(((Tables)obj).TABLE_NAME);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
