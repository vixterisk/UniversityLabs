using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GistForms
{
    public class Gist
    {
        public string url { set; get; }
        public string id { set; get; }
        public string description { set; get; }
        public bool @public { set; get; }
        public List<GistFile> files { set; get; }
        public Gist()
        {
            this.url = string.Empty;
            this.description = string.Empty;
            this.files = new List<GistFile>();
        }
    }

    public class GistFile
    {
        public string filename { set; get; }
        public string raw_url { set; get; }
        public string content { set; get; }
        public GistFile()
        {
            this.filename = string.Empty;
            this.raw_url = string.Empty;
        }
        public GistFile(string filename, string raw_url)
        {
            this.filename = filename;
            this.raw_url = raw_url;
        }
        
    }

}
