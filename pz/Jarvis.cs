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
        public static QuakeInfo quakeInfo;
        private static string currentFileName = null;
        public static bool FileOpened => currentFileName != null;

        public static ViewJarvisMessageEvent<string> viewOpenFileEvent = new ViewJarvisMessageEvent<string>();
        public static ViewJarvisMessageEvent<SaveFileArgs> viewSaveFileEvent = new ViewJarvisMessageEvent<SaveFileArgs>();
        public static ViewJarvisNoMessageEvent viewCloseFileEvent = new ViewJarvisNoMessageEvent();
        public static ViewJarvisMessageEvent<CellEventArgs> viewCellChangedEvent = new ViewJarvisMessageEvent<CellEventArgs>();
        public static ViewJarvisMessageEvent<ModifyListClass> viewModListEvent = new ViewJarvisMessageEvent<ModifyListClass>();

        private static void OnViewModList(object sender, ViewJarvisMessageEventArgs<ModifyListClass> messageEventArgs)
        {
            ModifyListClass mod = messageEventArgs.Content;
            if (mod.SortByID)
            {
                quakeInfo.Quakes = quakeInfo.SortedByID();
            }
            if (mod.SortByStations)
            {
                quakeInfo.Quakes = quakeInfo.SortedByStations();
            }
            if (mod.FilterByMag)
            {
                quakeInfo.Quakes = quakeInfo.MaxListByMag(mod.MagValue);
            }

            Form1.JarvisListMessageEvent.OnViewJarvisMessage(quakeInfo.GetList(CultureInfo.GetCultureInfo("ru-RU")));
            Form1.JarvisMinDepthUpdatedEvent.OnViewJarvisMessage(quakeInfo.MinDepthQuake);
            Form1.JarvisMaxDepthUpdatedEvent.OnViewJarvisMessage(quakeInfo.MaxDepthQuake);
        }


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
                    Form1.JarvisMessageEvent.OnViewJarvisMessage($"");
                }
            }
            catch (ArgumentException e)
            {
                switch (e.ParamName)
                {
                    case "file":
                        Form1.JarvisMessageEvent.OnViewJarvisMessage($"");
                        OnFileClosed();
                        break;
                }
            }
            Form1.JarvisMinDepthUpdatedEvent.OnViewJarvisMessage(quakeInfo.MinDepthQuake);
            Form1.JarvisMaxDepthUpdatedEvent.OnViewJarvisMessage(quakeInfo.MaxDepthQuake);
        }


        private static void OnSaveFile(object sender, ViewJarvisMessageEventArgs<SaveFileArgs> messageEventArgs)
        {
            SaveFileArgs args = messageEventArgs.Content;
            if (args.FileName != null && args.Append == false)
            {
                currentFileName = args.FileName;
            }
            if (args.Append)
            {
                quakeInfo.SaveToFile(args.FileName, "append");
            }
            else
            {
                quakeInfo.SaveToFile(args.FileName, "rewrite");
                Form1.JarvisSetTitleEvent.OnViewJarvisMessage(currentFileName);
            }
        }

        private static void OnFileClosed()
        {
            quakeInfo = new QuakeInfo();
            currentFileName = null;
            Form1.JarvisSetTitleEvent.OnViewJarvisMessage("");
            Form1.JarvisMinDepthUpdatedEvent.OnViewJarvisMessage(quakeInfo.MinDepthQuake);
            Form1.JarvisMaxDepthUpdatedEvent.OnViewJarvisMessage(quakeInfo.MaxDepthQuake);
        }

        private static void OnCellChanged(object sender, ViewJarvisMessageEventArgs<CellEventArgs> messageEventArgs)
        {
            // newCell.content.tostring
            CellEventArgs newCell = messageEventArgs.Content;
            if (newCell.RowIndex != -1)
            {
                List<string> list = quakeInfo.NewCell(newCell.Content.ToString(), newCell.ColumnIndex, newCell.RowIndex, CultureInfo.GetCultureInfo("ru-RU"));
                Form1.JarvisRowChangedEvent.OnViewJarvisMessage(new RowChangedArgs(newCell.RowIndex, list));
                Form1.JarvisMinDepthUpdatedEvent.OnViewJarvisMessage(quakeInfo.MinDepthQuake);
                Form1.JarvisMaxDepthUpdatedEvent.OnViewJarvisMessage(quakeInfo.MaxDepthQuake);
            }
        }

        static Jarvis()
        {
            viewOpenFileEvent.ViewJarvisMessageEvnt += OnFileOpened;
            viewCloseFileEvent.ViewJarvisEvnt += OnFileClosed;
            viewCellChangedEvent.ViewJarvisMessageEvnt += OnCellChanged;
            quakeInfo = new QuakeInfo();
            Form1.JarvisMinDepthUpdatedEvent.OnViewJarvisMessage(quakeInfo.MinDepthQuake);
            Form1.JarvisMaxDepthUpdatedEvent.OnViewJarvisMessage(quakeInfo.MaxDepthQuake);
            viewModListEvent.ViewJarvisMessageEvnt += OnViewModList;
            viewSaveFileEvent.ViewJarvisMessageEvnt += OnSaveFile;
        }

        public static CellEventArgs CellEventArgs
        {
            get => default(CellEventArgs);
            set
            {
            }
        }

        public static ViewJarvisNoMessageEvent ViewJarvisNoMessageEvent
        {
            get => default(ViewJarvisNoMessageEvent);
            set
            {
            }
        }

        public static ModifyListClass ModifyListClass
        {
            get => default(ModifyListClass);
            set
            {
            }
        }

        public static SaveFileArgs SaveFileArgs
        {
            get => default(SaveFileArgs);
            set
            {
            }
        }

        public static RowChangedArgs RowChangedArgs
        {
            get => default(RowChangedArgs);
            set
            {
            }
        }

        public static ViewJarvisMessageEventArgs<object> ViewJarvisMessageEventArgs
        {
            get => default(ViewJarvisMessageEventArgs<object>);
            set
            {
            }
        }

        public static ViewJarvisMessageEvent<object> ViewJarvisMessageEvent
        {
            get => default(ViewJarvisMessageEvent<object>);
            set
            {
            }
        }
    }
}
