using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace CoreGeneral.Utilidades
{
    public static class Sesion
    {
        public static void SetObjectAsJson<T>(ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}