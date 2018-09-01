using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportStore.Model;

namespace SportStore.Repository.Entity
{
    public class SessionRepository : ISessionRepository
    {
        public Session GetByToken(string token)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.SessionDbSet.Find(token);
            }
        }

        public IEnumerable<Session> Find(Predicate<Session> predicate)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.SessionDbSet.AsEnumerable().Where(s => predicate(s)).ToList();
            }
        }

        public void Add(Session session)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                session.User = dbContext.UserDbSet.Find(session.User.UserName);

                dbContext.SessionDbSet.Add(session);
                dbContext.SaveChanges();
            }
        }

        public void Update(Session session)
        {
            //using (var dbContext = new EntityFrameworkSportStoreDbContext())
            //{
            //    var sessionInRepository = dbContext.SessionDbSet.Find(session.Token);

            //    dbContext.SaveChanges();
            //}
        }
    }
}
