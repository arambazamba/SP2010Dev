using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab06.TrainingEvents
{
    /// <summary>
    /// This class contains the properties for Entity1. The properties keep the data for entity1.
    /// If you want to rename the class, don't forget to rename the entity in the model xml as well.
    /// </summary>
    public partial class TrainingEvent
    {
        //3. Set up Properties
        public Int32 TrainingEventID { get; set; } //from TrainingEvent table
        public DateTime EventDate { get; set; } //from TrainingEvent table
        public string Status { get; set; } //from TrainingEvent table
        public string LoginName { get; set; } //from Student table
        public string Title { get; set; } //from Training table
        public string EventType { get; set; } //from Training table
        public string Description { get; set; } //from Training table
    }
}
