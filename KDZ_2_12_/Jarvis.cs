using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using EventsLibrary;
using System.Globalization;

namespace KDZ_2_12_
{
    public static class Jarvis
    {
        private static QuakeInfo quakeInfo;
        private static string currentFileName = null;
        public static bool FileOpened => currentFileName != null;

        public static ViewJarvisMessageEvent<string> viewOpenFileEvent = new ViewJarvisMessageEvent<string>();
        public static ViewJarvisMessageEvent<string> viewSaveFileEvent = new ViewJarvisMessageEvent<string>();
        public static ViewJarvisNoMessageEvent viewCloseFileEvent = new ViewJarvisNoMessageEvent();
        public static ViewJarvisMessageEvent<CellEventArgs> viewCellChangedEvent = new ViewJarvisMessageEvent<CellEventArgs>();


        private static void OnFileOpened(object sender, ViewJarvisMessageEventArgs<string> messageEventArgs)
        {
            string fileName = messageEventArgs.Content;
            try
            {
                quakeInfo = new QuakeInfo(fileName);
                Form1.JarvisListMessageEvent.OnViewJarvisMessage(quakeInfo.GetList(CultureInfo.GetCultureInfo("ru-RU")));
                currentFileName = fileName;
                Form1.JarvisSetTitleEvent.OnViewJarvisMessage(fileName);
                if (!quakeInfo.Valid || !quakeInfo.Correct)
                {
                    Form1.JarvisMessageEvent.OnViewJarvisMessage($"Внимаие! Файл {fileName} открыт и содержит некорректные значения");
                }
            }
            catch (ArgumentException e)
            {
                switch (e.ParamName)
                {
                    case "file":
                        Form1.JarvisMessageEvent.OnViewJarvisMessage($"Файл {fileName} повреждён и не может быть открыт");
                        OnFileClosed();
                        break;
                }
            }
        }

        private static void OnFileClosed()
        {
            quakeInfo = new QuakeInfo();
            currentFileName = null;
        }

        private static void OnCellChanged(object sender, ViewJarvisMessageEventArgs<CellEventArgs> messageEventArgs)
        {
            // newCell.content.tostring
            CellEventArgs newCell = messageEventArgs.Content;

        }

        static Jarvis()
        {
            viewOpenFileEvent.ViewJarvisMessageEvnt += OnFileOpened;
            viewCloseFileEvent.ViewJarvisEvnt += OnFileClosed;
            viewCellChangedEvent.ViewJarvisMessageEvnt += OnCellChanged;
            quakeInfo = new QuakeInfo();
        }
    }
}
