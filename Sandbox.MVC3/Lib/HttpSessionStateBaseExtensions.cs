using System;
using System.Web;

namespace Sandbox.MVC.Lib
{
    public static class HttpSessionStateBaseExtensions
    {
        public static T GetValue<T>(this HttpSessionStateBase session, string key)
        {
            try
            {
                object value = session[key];
                return (T)value;
            }
            catch (Exception)
            {
                return default(T);                
            }
        }

        public static void SetValue<T>(this HttpSessionStateBase session, string key, T value)
        {
            session[key] = value;
        }
    }
}