using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoLoginTool.Model
{
    public class ModelClass
    {
        public string BatPath
        {
            get
            {
                return StartupDir + @"\netuse.bat";
            }
        }

        [DllImport("shell32.dll")]
        static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, [Out] StringBuilder lpszPath, int nFolder, bool fCreate);
        const int CSIDL_COMMON_STARTMENU = 0x16;  // All Users\Start Menu
        public string StartupDir
        {
            get
            {
                if (string.IsNullOrEmpty(_startupDir))
                {
                    StringBuilder path = new StringBuilder(260);
                    SHGetSpecialFolderPath(IntPtr.Zero, path, CSIDL_COMMON_STARTMENU, false);
                    _startupDir = path.ToString()+ @"\Programs\Startup";
                }
                return _startupDir;
            }
        }
        private string _startupDir;
    }
    public class ConnectionInfo
    {
        [DisplayName("IP位址")]
        public string IP { get; set; }
        [DisplayName("登入帳號")]
        public string UserName { get; set; }
        [DisplayName("登入密碼")]
        public string Password { get; set; }
    }
}
