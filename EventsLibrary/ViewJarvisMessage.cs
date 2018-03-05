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
        public event EventHandler<ViewJarvisMessageEventArgs<T>> ViewJarvisMessageEvnt;

        public void OnViewJarvisMessage(T content)
        {
            ViewJarvisMessageEventArgs<T> args = new ViewJarvisMessageEventArgs<T>();

            if (ViewJarvisMessageEvnt != null)
            {
                args.Content = content;
                ViewJarvisMessageEvnt(this, args);
            }
        }
    }
}
