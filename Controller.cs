using AutoLoginTool.Model;
using System.ComponentModel;
using System.IO;

namespace AutoLoginTool.Controller
{
    public class ControlClass
    {
        public BindingList<ConnectionInfo> ReadBat(string batPath)
        {
            BindingList<ConnectionInfo> res = new BindingList<ConnectionInfo>();
            ConnectionInfo ci;
            using (StreamReader sr = new StreamReader(batPath))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    if (line.Contains("delete") || line.Contains("::"))
                    {
                        continue;
                    }
                    if (line.Contains("net use"))
                    {
                        ci = new ConnectionInfo();
                        int indStart = line.IndexOf("\\") + 2;
                        int indEnd = line.IndexOf("/") - 1;
                        ci.IP = line.Substring(indStart, indEnd - indStart);

                        indStart = line.IndexOf(':') + 1;
                        line = line.Substring(indStart);
                        string userName = line.Substring(0, line.IndexOf(' '));
                        string password = line.Substring(line.IndexOf(' ') + 1);
                        ci.UserName = userName;
                        ci.Password = password;
                        res.Add(ci);
                    }
                }
            }
            return res;
        }
        public void WriteBat(BindingList<ConnectionInfo> connInfoList, string batPath)
        {
            File.Delete(batPath);
            using (StreamWriter sw = new StreamWriter(batPath))
            {
                foreach (ConnectionInfo ci in connInfoList)
                {
                    sw.WriteLine(string.Format(@"net use /delete \\{0}", ci.IP));
                    sw.WriteLine(string.Format(@"net use \\{0} /user:{1} {2}", ci.IP, ci.UserName, ci.Password));
                    sw.WriteLine();
                }
            }
        }
    }
}
