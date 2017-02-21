using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueTerminal.Config
{
    class ConfigHelper
    {
        private static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"/QueueTerminal";
        private static string fileName = "/config.txt";

        public static bool SaveConfig(string postNumber, string ip)
        {
            var text = postNumber + ";" + ip;
            try
            {
                Directory.CreateDirectory(folderPath);
                File.WriteAllText(folderPath + fileName, text);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return false;
        }

        public static string[] GetConfig()
        {
            string[] sArray = null;

            try
            {
                string text = File.ReadAllText(folderPath + fileName);
                if (text != null && text.Contains(";"))
                    sArray = text.Split(';');
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return sArray;
        }
    }
}
