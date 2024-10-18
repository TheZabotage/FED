using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace FixerApp.Models
{
    public class Kalender
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Date { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<FixerJob> FixerJobs { get; set; }
    }
}