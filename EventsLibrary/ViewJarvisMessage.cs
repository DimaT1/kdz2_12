using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary
{
    public class ViewJarvisMessageEventArgs<T> : EventArgs
    {
        public T Content;
    }

    public class ViewJarvisMessageEvent<T>
    {
        public event EventHandler<ViewJarvisMessageEventArgs<T>> ViewJarvisMessage;

        public void OnJarvisMessage(T content)
        {
            ViewJarvisMessageEventArgs<T> args = new ViewJarvisMessageEventArgs<T>();

            if (ViewJarvisMessage != null)
            {
                args.Content = content;
                ViewJarvisMessage(this, args);
            }
        }
    }
}
