using System;
using System.Collections.Generic;
using System.Text;

namespace VolumeTrackerApp.Models
{

    public class Rootobject
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public Page Page { get; set; }
    }

    public class Page
    {
        public Pageinfo pageInfo { get; set; }
        public Media[] media { get; set; }
    }

    public class Pageinfo
    {
        public int total { get; set; }
        public int currentPage { get; set; }
        public int lastPage { get; set; }
        public bool hasNextPage { get; set; }
        public int perPage { get; set; }
    }

    public class Media
    {
        public int id { get; set; }
        public string description { get; set; }
        public Title title { get; set; }
        public int? volumes { get; set; }
        public Coverimage coverImage { get; set; }
    }

    public class Title
    {
        public string romaji { get; set; }
        public string english { get; set; }
        public string native { get; set; }
    }

    public class Coverimage
    {
        public string extraLarge { get; set; }
        public string large { get; set; }
        public string medium { get; set; }
        public string color { get; set; }
    }

}
