using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace VolumeTrackerApp.Models
{
    public class MangaObject
    {
        [PrimaryKey]
        public int id { get; set; }
        public string title { get; set; }

        public string imageURL { get; set; }

        public string description {get;set;}

        public int volumesCollected { get; set; }
    }
}
