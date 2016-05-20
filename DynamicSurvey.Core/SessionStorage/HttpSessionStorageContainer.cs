using System.Web;
using NHibernate;

namespace DynamicSurvey.Core.SessionStorage
{
    public class HttpSessionStorageContainer : ISessionStorageContainer
    {
        private const string SessionKey = "NHSession";

        public ISession GetCurrentSession()
        {
            ISession nhSession = null;

            if (HttpContext.Current.Items.Contains(SessionKey))
            {
                nhSession = (ISession) HttpContext.Current.Items[SessionKey];
            }

            return nhSession;
        }

        public void Store(ISession session)
        {
            if (HttpContext.Current.Items.Contains(SessionKey))
            {
                HttpContext.Current.Items[SessionKey] = session;
            }
            else
            {
                HttpContext.Current.Items.Add(SessionKey, session);
            }
        }
    }
}
