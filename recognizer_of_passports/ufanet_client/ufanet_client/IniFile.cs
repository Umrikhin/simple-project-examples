using System.Runtime.InteropServices;
using System.Text;

namespace ufanet_client
{
    /// <summary>
    /// Класс внесен в наш неймспейс с кодепроджекта
    /// Чтение запись текстовых полей ini файла
    /// Выполняеться через ядро винды - функции ДЛЯ поддержки совместимости со старыми версиями
    /// Совместимость на текущий момент (по MSDN) 
    /// Client: Included in Windows XP, Windows 2000 Professional, Windows NT Workstation, Windows Me, Windows 98, and Windows 95. 
    /// Server: Included in Windows Server 2003, Windows 2000 Server, and Windows NT Server. 
    /// Unicode: Implemented as Unicode and ANSI versions. Note that Unicode support on Windows Me/98/95 requires Microsoft Layer for Unicode.
    /// </summary>
    public class IniFile
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="INIFile">Имя ini-файла</param>
        public IniFile(string INIFile)
        {
            m_path = INIFile;
        }

        #region Private property

        private string m_path;

        #endregion

        #region Импорт kernel32

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal,
            int size, string filePath);

        #endregion

        #region Public property

        /// <summary>
        /// Текущий путь к ini-файлу
        /// </summary>
        public string Path
        {
            get { return m_path; }
            set { m_path = value; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, Path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", temp,
                255, Path);
            return temp.ToString();
        }

        #endregion
    }
}
