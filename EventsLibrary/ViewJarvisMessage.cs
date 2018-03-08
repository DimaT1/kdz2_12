using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary
{
    /// <summary>
    /// Параметр обобщённого события
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ViewJarvisMessageEventArgs<T> : EventArgs
    {
        public T Content;
    }

    /// <summary>
    /// Обобщённое событие с параметром
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ViewJarvisMessageEvent<T>
    {
        private bool plugged = true;
        public bool Plugged {
            get { return plugged; }
            set { plugged = value; }
        }

        public event EventHandler<ViewJarvisMessageEventArgs<T>> ViewJarvisMessageEvnt;

        public void OnViewJarvisMessage(T content)
        {
            ViewJarvisMessageEventArgs<T> args = new ViewJarvisMessageEventArgs<T>();

            if (ViewJarvisMessageEvnt != null && plugged)
            {
                args.Content = content;
                ViewJarvisMessageEvnt(this, args);
            }
        }
    }
}
