using System;
using System.Collections.Generic;

using SportStore.Model;

namespace SportStore.Repository
{
    public interface ISessionRepository
    {
        Session GetByToken(string token);
        IEnumerable<Session> Find(Predicate<Session> predicate);
        void Add(Session session);
        void Update(Session session);
    }
}
