using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace FixerApp.Models
{
    public class Faktura
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string NameOfFixer { get; set; }
        public string Materials { get; set; }
        public decimal MaterialsPrice { get; set; }
        public double HoursWorked { get; set; }
        public decimal TotalPrice { get; set; }

        [ForeignKey(typeof(FixerJob))]
        public int FixerJobId { get; set; }

        [OneToOne]
        public FixerJob FixerJob { get; set; }
    }
}
