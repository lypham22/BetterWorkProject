using BW.Website.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BW.Website.Common.Utilities
{
    public class SessionManager : IDisposable
    {
        private static SessionManager _instance = null;

        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SessionManager();
                }

                return _instance;
            }
        }
        public string SessionID
        {
            get { return HttpContext.Current.Session.SessionID; }
        }

        public string Culture
        {
            get
            {
                return GetDataString("Culture");
            }
            set
            {
                AddItem("Culture", value);
            }
        }

        public void AddItem(string key, object data)
        {
            var val = SessionValue;
            if (val == null)
            {
                val = new Dictionary<string, object>();
            }
            if (!val.Keys.Contains(key))
            {
                val.Add(key, data);
            }
            else
            {
                val[key] = data;
            }

            SessionValue = val;
        }

        public void RemoveItem(string key)
        {
            var val = SessionValue;
            if (val == null)
            {
                return;
            }

            val.Remove(key);
            SessionValue = val;
        }

        public TLoad GetData<TLoad>(string key) where TLoad : class
        {
            TLoad value;
            object val = null;
            if (SessionValue == null)
            {
                return null;
            }

            if (!SessionValue.TryGetValue(key, out val))
            {
                return null;
            }

            value = val as TLoad;

            return value;
        }

        public int GetDataInt(string key)
        {
            object val = null;
            if (SessionValue == null)
            {
                return int.MinValue;
            }

            if (!SessionValue.TryGetValue(key, out val))
            {
                return int.MinValue;
            }

            return Convert.ToInt32(val);
        }

        public long GetDataLong(string key)
        {
            object val = null;
            if (SessionValue == null)
            {
                return long.MinValue;
            }

            if (!SessionValue.TryGetValue(key, out val))
            {
                return long.MinValue;
            }

            return Convert.ToInt64(val);
        }

        public long? GetDataLongNullable(string key)
        {
            object val = null;
            if (SessionValue == null || !SessionValue.TryGetValue(key, out val))
            {
                return null;
            }
            
            return Convert.ToInt64(val);
        }

        public bool GetDataBool(string key)
        {
            object val = null;
            if (SessionValue == null)
            {
                return false;
            }

            if (!SessionValue.TryGetValue(key, out val))
            {
                return false;
            }

            return Convert.ToBoolean(val);
        }

        public string GetDataString(string key)
        {
            object val = SessionValue;
            if (SessionValue == null)
            {
                return string.Empty;
            }

            if (!SessionValue.TryGetValue(key, out val))
            {
                return string.Empty;
            }

            return Convert.ToString(val);
        }

        public DateTime GetDataDateTime(string key)
        {
            object val = null;
            if (SessionValue == null)
            {
                return DateTime.MinValue;
            }

            if (!SessionValue.TryGetValue(key, out val))
            {
                return DateTime.MinValue;
            }

            return Convert.ToDateTime(val);
        }

        public void Dispose()
        {
            CleanSession();
        }

        protected bool HasSession()
        {
            return SessionValue != null;
        }

        protected void CleanSession()
        {
            if (HasSession())
            {
                HttpContext.Current.Session.RemoveAll();
            }
        }

        private Dictionary<string, object> SessionValue
        {
            get
            {
                if (HttpContext.Current == null || HttpContext.Current.Session == null)
                {
                    throw new ArgumentException("Session is not available");
                }

                var val = HttpContext.Current.Session[SessionKeys.USERINFO];
                if (val == null)
                {
                    return null;
                }

                return val as Dictionary<string, object>;
            }
            set
            {
                if (HttpContext.Current == null || HttpContext.Current.Session == null)
                {
                    throw new ArgumentException("Session is not available");
                }

                HttpContext.Current.Session[SessionKeys.USERINFO] = value;
            }
        }
    }
}