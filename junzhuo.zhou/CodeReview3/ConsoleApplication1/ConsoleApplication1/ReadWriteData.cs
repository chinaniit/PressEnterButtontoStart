using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    public class ReadWriteData
    {
        public Stream ReflectionStream(string path)
        {
            var sm = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            return sm;
        }
        public string GetFileText(string path)
        {
            var sm = ReflectionStream(path);
            if (sm == null) throw new IOException("Template " + path + " not found!");
            var bs = new byte[sm.Length];
            sm.Read(bs, 0, (int)sm.Length);
            sm.Close();
            string templateString = Encoding.UTF8.GetString(bs);
            return templateString;
        }

        public string GetFileText(Stream sm)
        {
            var bs = new byte[sm.Length];
            sm.Read(bs, 0, (int)sm.Length);
            sm.Close();
            string templateString = Encoding.UTF8.GetString(bs);
            return templateString;
        }

        public string GetWinningPeopleStrByHtml(string path, object source)
        {
            var templateString = GetFileText(path);
            return Nustache.Core.Render.StringToString(templateString, source);
        }
    }
}
