using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace Diary_WPF
{
    public class Note
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public Note(string name, string description, DateTime date)
        {
            this.Name = name;
            this.Description = description;
            Date = date;
        }

    }
}
