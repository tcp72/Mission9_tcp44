using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bookstore.Infrastructure
{//all making so we can put our info into JSON oject and pull out own info out
    public static class SessionExtensions //make it static so only applies to this object everywhere
    {//he said to just memorize this and not worry too much about what's happening here. Copy and paste
        public static void SetJson (this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetJson<T> (this ISession session, string key)
        {
            var sessionData = session.GetString(key);
                            //if statement syntax below
            return sessionData == null ? default (T) : JsonSerializer.Deserialize<T>(sessionData);
        }
    }
}
