using System;
using SQLite;

namespace MedicationReminders.Model
{
    public class Medication
    {
        [PrimaryKey,Unique]
        public string Name { get; set; }

        public string Schedule { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public int Dosage { get; set; }

    }
}
