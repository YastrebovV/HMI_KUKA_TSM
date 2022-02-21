using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace HMI_KUKA_TSM
{
    public class IniClass
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key,string val,string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size,string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public void IniFile(string INIPath)
        {
            path = INIPath;
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section,string Key,string Value)
        {
            WritePrivateProfileString(Section,Key,Value,this.path);
        }
        
        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section,string Key)
        {
            string[] str = new string[2];
            // StringBuilder temp = new StringBuilder(255);

            //int i = GetPrivateProfileString(Section,Key,"", temp, 
            //   255, this.path);

            // byte[] bytes = Encoding.Default.GetBytes(temp.ToString());
            // return Encoding.ASCII.GetString(bytes);

            using (StreamReader reader = new StreamReader(path, Encoding.GetEncoding(1251)))
            {
                string line;
                
                bool sectionCheck = false;
                str[0] = "";
                str[1] = "";

                while ((line = reader.ReadLine()) != null)
                {
                    if (sectionCheck)
                    {
                        if (line.Contains(Key))
                        {
                            str = line.Split('=');
                            break;
                        }                                               
                    }
                    if (Section == line)
                    {
                        sectionCheck = true;
                    }
                }
            }

            return str[1];//temp.ToString();           
        }
    }
}
