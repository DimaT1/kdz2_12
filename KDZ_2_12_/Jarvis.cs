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
    /// <summary>
    /// Контроллер
    /// </summary>
    public static class Jarvis
    {
        /// <summary>
        /// Модель агрегирована в контроллер
        /// </summary>
        public static QuakeInfo quakeInfo;
        /// <summary>
        /// Имя файла, с которым ведётся работа
        /// </summary>
        private static string currentFileName = null;
        /// <summary>
        /// Свойство корректного открытия файла
        /// </summary>
        public static bool FileOpened => currentFileName != null;

        /// <summary>
        /// Событие обработчика открытия файла
        /// </summary>
        public static ViewJarvisMessageEvent<string> viewOpenFileEvent = new ViewJarvisMessageEvent<string>();
        /// <summary>
        /// Событие обработчика сохранения файла
        /// </summary>
        public static ViewJarvisMessageEvent<SaveFileArgs> viewSaveFileEvent = new ViewJarvisMessageEvent<SaveFileArgs>();
        /// <summary>
        /// Событие закрытия файла
        /// </summary>
        public static ViewJarvisNoMessageEvent viewCloseFileEvent = new ViewJarvisNoMessageEvent();
        /// <summary>
        /// Событие обработчика изменения ячейки таблицы
        /// </summary>
        public static ViewJarvisMessageEvent<CellEventArgs> viewCellChangedEvent = new ViewJarvisMessageEvent<CellEventArgs>();
        /// <summary>
        /// Событие обработчика модификации списка
        /// </summary>
        public static ViewJarvisMessageEvent<ModifyListClass> viewModListEvent = new ViewJarvisMessageEvent<ModifyListClass>();

        /// <summary>
        /// обработчик модификации списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs"аргументы></param>
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

        /// <summary>
        /// Обработчик открытия файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs"></param>
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
            Form1.JarvisMinDepthUpdatedEvent.OnViewJarvisMessage(quakeInfo.MinDepthQuake);
            Form1.JarvisMaxDepthUpdatedEvent.OnViewJarvisMessage(quakeInfo.MaxDepthQuake);
        }


        /// <summary>
        /// Обработчик сохранения файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs"></param>
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

        /// <summary>
        /// Обработчик закрытия файла
        /// </summary>
        private static void OnFileClosed()
        {
            quakeInfo = new QuakeInfo();
            currentFileName = null;
            Form1.JarvisSetTitleEvent.OnViewJarvisMessage("");
            Form1.JarvisMinDepthUpdatedEvent.OnViewJarvisMessage(quakeInfo.MinDepthQuake);
            Form1.JarvisMaxDepthUpdatedEvent.OnViewJarvisMessage(quakeInfo.MaxDepthQuake);
        }

        /// <summary>
        /// Обработчик изменения ячейки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs"></param>
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

        /// <summary>
        /// Конструктор
        /// </summary>
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
