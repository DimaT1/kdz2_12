﻿using System;
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

        private static void OnFileOpened(object sender, ViewJarvisMessageEventArgs<string> messageEventArgs)
        {
            string fileName = messageEventArgs.Content;
            try
            {
                quakeInfo = new QuakeInfo(fileName);
                Form1.JarvisListMessageEvent.OnViewJarvisMessage(quakeInfo.GetList());
                currentFileName = fileName;

            }
            catch (ArgumentException e)
            {
                switch (e.ParamName)
                {
                    case "file":
                        Form1.JarvisMessageEvent.OnViewJarvisMessage($"Файл {fileName} повреждён и не может быть открыт");
                        break;
                    case "obj":
                        Form1.JarvisMessageEvent.OnViewJarvisMessage($"Некоторые значения файла {fileName} повреждены");
                        currentFileName = fileName;
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
