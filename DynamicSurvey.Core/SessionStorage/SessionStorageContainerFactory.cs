using System.Web;

namespace DynamicSurvey.Core.SessionStorage
{
    public static class SessionStorageContainerFactory
    {
        public static ISessionStorageContainer GetStorageContainer()
        {
            if (HttpContext.Current == null)
            {
                return new ThreadSessionStorageContainer();
            }

            return new HttpSessionStorageContainer();
        }
    }
}
