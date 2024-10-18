using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace FixerApp.Models
{
    public class FixerJob
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Customer { get; set; }
        public string CustomerAddress { get; set; }
        public string MakeAndModel { get; set; }
        public string RegisterNumber { get; set; }
        public string DeliveryDateTime { get; set; }
        public string JobToDo { get; set; }

        public int KalenderId { get; set; }

    }
}