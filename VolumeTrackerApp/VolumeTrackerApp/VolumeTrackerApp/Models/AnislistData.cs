using System;
using System.Collections.Generic;
using System.Text;

namespace VolumeTrackerApp.Models
{
    class AnislistData
    {

        public class Rootobject
        {
            public Data data { get; set; }
        }

        public class Data
        {
            public Media Media { get; set; }
        }

        public class Media
        {
            public int id { get; set; }
            public object volumes { get; set; }
            public Title title { get; set; }
        }

        public class Title
        {
            public string english { get; set; }
        }

    }
}
