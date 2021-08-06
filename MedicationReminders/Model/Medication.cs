using System;
using SQLite;

namespace MedicationReminders.Model
{
    public class Medication
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Schedule { get; set; }

        public string DateTime { get; set; }

        public string Time { get; set; }

        public string Frequency { get; set; }

        public int Dosage { get; set; }

        public bool Confirmed { get; set; }

    }
}
