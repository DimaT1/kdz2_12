using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using EventsLibrary;

namespace KDZ_2_12_
{
    public static class Jarvis
    {
        private static QuakeInfo quakeInfo;
        private static string currentFileName = null;
        public static bool FileOpened => currentFileName != null;

        public static ViewJarvisMessageEvent<string> viewOpenFileEvent = new ViewJarvisMessageEvent<string>(),
                                                     viewSaveFileEvent = new ViewJarvisMessageEvent<string>();

        public static void OnFileOpened(object sender, ViewJarvisMessageEventArgs<string> messageEventArgs)
        {
            string fileName = messageEventArgs.Content;
            try
            {
                quakeInfo = new QuakeInfo(fileName);
            }
            catch (ArgumentNullException e)
            {
                switch (e.ParamName)
                {
                    case "file":
                        currentFileName = fileName;

                        break;
                    case "obj":
                        break;
                }
            }
        }

        static Jarvis()
        {
            viewOpenFileEvent.ViewJarvisMessage += OnFileOpened;
        }
    }
}
