using NHibernate;

namespace GenCore.DataAccesLayer
{
    public interface ISessionManager
    {
        ISession GetCurrentSession();
        ISession OpenSession();
        void CloseSession(ISession pSession, bool error = false);
        void CloseSession(bool error = false);
    }
}
