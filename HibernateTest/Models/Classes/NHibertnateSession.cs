using NHibernate;
using NHibernate.Cfg;

namespace HibernateTest.Models.Classes
{
    class NHibertnateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            configuration.Configure();
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }  

    }
}
