using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sfEDI
{
    class Segment
    {
        public string SegmentID { get; set; }
        public List<Element> Elements { get; set; }
        public void Add(Element element) { Elements.Add(element); }

        public string GetLine()
        {
            string result = SegmentID;

            foreach (Element e in Elements)
            {
                result += "*";
                result += e.Value;

            }

            result += "~";
            return result;
        }
        public Segment(string id)
        {
            this.Elements = new List<Element>();
            this.SegmentID = id;
        }
    }
}
