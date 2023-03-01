using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomBase.Helpers
{
    public static class FileReader
    {
        public static  string ReadFile(string Url )
        {
            FileStream  fileStream = new FileStream(Url, FileMode.Open);
           StringBuilder json = new StringBuilder();
            using (StreamReader sr = new StreamReader(fileStream))
            {
                string line;
                //read the line by line and print each line
                while ((line = sr.ReadLine()) != null)
                {
                    json.Append( line);
                }

            }
            return json.ToString();
        }
    }
}
