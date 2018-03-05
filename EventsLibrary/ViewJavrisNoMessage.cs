using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary
{
    public delegate void ViewJarvisEventDelegate();

    public class ViewJarvisNoMessageEvent
    {
        public event ViewJarvisEventDelegate ViewJarvisEvnt;

        public void OnViewJarvisMessage()
        {
            ViewJarvisEvnt?.Invoke();
        }
    }
}
