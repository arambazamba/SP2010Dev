using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//9.
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
namespace Lab06.TrainingEvents
{
    /// <summary>
    /// All the methods for retrieving, updating and deleting data are implemented in this class file.
    /// The samples below show the finder and specific finder method for Entity1.
    /// </summary>
    public class TrainingEventEntityService
    {
        //10. Helper function
        static SqlConnection getSqlConnection()
        {
            SqlConnection sqlConn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=HRTrainingManagement;Data Source=SHAREPOINT\SharePoint");
            return (sqlConn);
        }
        public static TrainingEvent ReadItem(int id)
        {
            //throw (new Exception(id.ToString()));
            //11.
            SqlConnection thisConn = null;
            TrainingEvent evt = null;
            try
            {
                evt = new TrainingEvent();
                thisConn = getSqlConnection();
                thisConn.Open();
                SqlCommand thisCmd = new SqlCommand();
                thisCmd.CommandText = "SELECT e.TrainingEventID, s.LoginName, t.Title, t.EventType, t.Description, e.EventDate, e.Status"
                    + " FROM Student s"
                    + " INNER JOIN TrainingEvent e ON s.StudentID = e.StudentID"
                    + " INNER JOIN TrainingObjects t ON e.TrainingID = t.TrainingID"
                    + " WHERE e.TrainingEventID = " + id.ToString();
                thisCmd.Connection = thisConn;
                SqlDataReader thisReader = thisCmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (thisReader.Read())
                {
                    evt.TrainingEventID = id;
                    evt.LoginName = thisReader[1].ToString();
                    evt.Title = thisReader[2].ToString();
                    evt.EventType = thisReader[3].ToString();
                    evt.Description = thisReader[4].ToString();
                    evt.EventDate = DateTime.Parse(thisReader[5].ToString());
                    evt.Status = thisReader[6].ToString();
                }
                else
                {
                    evt.TrainingEventID = -1;
                    evt.LoginName = "Data Not Found";
                    evt.Title = "Data Not Found";
                    evt.EventType = "Data Not Found";
                    evt.Description = "Data Not Found";
                    evt.EventDate = DateTime.MinValue;
                    evt.Status = "Data Not Found";
                }
                thisReader.Close();
                return (evt);
            }
            catch
            {
                evt.TrainingEventID = -1;
                evt.LoginName = "Data Not Found";
                evt.Title = "Data Not Found";
                evt.EventType = "Data Not Found";
                evt.Description = "Data Not Found";
                evt.EventDate = DateTime.MinValue;
                evt.Status = "Data Not Found";
                return (evt);
            }
            finally
            {
                thisConn.Dispose();
            }
        }
       
        public static IEnumerable<TrainingEvent> ReadList()
        {
            //12.
            SqlConnection thisConn = null;
            List<TrainingEvent> allEvents;
            try
            {
                thisConn = getSqlConnection();
                allEvents = new List<TrainingEvent>();
                thisConn.Open();
                SqlCommand thisCommand = new SqlCommand();
                thisCommand.Connection = thisConn;
                thisCommand.CommandText = "SELECT e.TrainingEventID, s.LoginName, t.Title, t.EventType, t.Description, e.EventDate, e.Status"
                    + " FROM Student s"
                    + " INNER JOIN TrainingEvent e ON s.StudentID = e.StudentID"
                    + " INNER JOIN TrainingObjects t ON e.TrainingID = t.TrainingID";
                SqlDataReader thisReader = thisCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (thisReader.Read())
                {
                    TrainingEvent evt = new TrainingEvent();
                    evt.TrainingEventID = int.Parse(thisReader[0].ToString());
                    evt.LoginName = thisReader[1].ToString();
                    evt.Title = thisReader[2].ToString();
                    evt.EventType = thisReader[3].ToString();
                    evt.Description = thisReader[4].ToString();
                    evt.EventDate = DateTime.Parse(thisReader[5].ToString());
                    evt.Status = thisReader[6].ToString();
                    allEvents.Add(evt);
                }
                thisReader.Close();
                TrainingEvent[] eventList = new TrainingEvent[allEvents.Count];
                for (int evtCounter = 0; evtCounter <= allEvents.Count - 1; evtCounter++)
                {
                    eventList[evtCounter] = allEvents[evtCounter];
                }
                return (eventList);
            }
            catch (Exception ex)
            {
                TrainingEvent[] errEventList = new TrainingEvent[1]; 
                TrainingEvent errEvt = new TrainingEvent();
                errEvt.TrainingEventID = -1;
                errEvt.LoginName = ex.Message;
                errEvt.Title = ex.Message;
                errEvt.EventType = ex.Message;
                errEvt.Description = ex.Message;
                errEvt.EventDate = DateTime.MinValue;
                errEvt.Status = ex.Message;
                errEventList[0] = errEvt;
                return (errEventList);
            }
            finally
            {
                thisConn.Dispose();
            }
        }

        public static void Delete(int trainingEventID)
        {
            //13.

            SqlConnection thisConn = null;
            try
            {
                thisConn = getSqlConnection();
                thisConn.Open();
                SqlCommand thisCommand = new SqlCommand();
                thisCommand.Connection = thisConn;
                thisCommand.CommandText = "DELETE TrainingEvent WHERE TrainingEventID = "
                    + trainingEventID.ToString();
                thisCommand.ExecuteNonQuery();
            }
            finally
            {
                thisConn.Dispose();
            }
        }

       

        public static void Update(TrainingEvent trainingEventEntity)
        {
            SqlConnection thisConn = null;
            try
            {
                thisConn = getSqlConnection();
                thisConn.Open();
                //Get New Values to push through in the update
                int trainingEventID = trainingEventEntity.TrainingEventID;
                string studentName = trainingEventEntity.LoginName;
                string trainingTitle = trainingEventEntity.Title;
                string trainingType = trainingEventEntity.EventType;
                string trainingDescription = trainingEventEntity.Description;
                DateTime trainingDate = trainingEventEntity.EventDate;
                string trainingStatus = trainingEventEntity.Status;
                //15. See if LoginName was updated
                int studentID = 0;
                SqlCommand studentCmd = new SqlCommand();
                studentCmd.Connection = thisConn;
                studentCmd.CommandText = "SELECT s.StudentID, s.LoginName"
                    + " FROM Student s"
                    + " INNER JOIN TrainingEvent e ON s.StudentID = e.StudentID"
                    + " WHERE e.TrainingEventID = " + trainingEventID.ToString();
                SqlDataReader studentReader = studentCmd.ExecuteReader(CommandBehavior.Default);
                studentReader.Read();
                if (studentReader[1].ToString() == studentName)
                {
                    studentID = int.Parse(studentReader[0].ToString());
                    studentReader.Close();
                }
                else
                {
                    studentReader.Close();
                    //16. If updated, was it updated to a user who already exists?
                    SqlCommand changeStudentCmd = new SqlCommand();
                    changeStudentCmd.Connection = thisConn;
                    changeStudentCmd.CommandText = "SELECT StudentID"
                    + " FROM Student"
                    + " WHERE LoginName = '" + studentName + "'";
                    SqlDataReader changeStudentReader = changeStudentCmd.ExecuteReader(CommandBehavior.Default);
                    if (changeStudentReader.Read())
                    {
                        //17. If so, get existing Student ID
                        studentID = int.Parse(changeStudentReader[0].ToString());
                        changeStudentReader.Close();
                    }
                    else
                    {
                        changeStudentReader.Close();
                        //18. If not, insert new student and get the new user ID
                        SqlCommand addStudentCommand = new SqlCommand();
                        addStudentCommand.Connection = thisConn;
                        addStudentCommand.CommandText = "INSERT Student(LoginName) VALUES('" + studentName + "')";
                        addStudentCommand.ExecuteNonQuery();
                        SqlCommand getNewStudentCmd = new SqlCommand();
                        getNewStudentCmd.Connection = thisConn;
                        getNewStudentCmd.CommandText = "SELECT StudentID"
                        + " FROM Student"
                        + " WHERE LoginName = '" + studentName + "'";
                        SqlDataReader getNewStudentReader = getNewStudentCmd.ExecuteReader(CommandBehavior.Default);
                        getNewStudentReader.Read();
                        studentID = int.Parse(getNewStudentReader[0].ToString());
                        getNewStudentReader.Close();
                    }
                }
                //19. See if training object was updated
                int trainingID = 0;
                SqlCommand trainingCmd = new SqlCommand();
                studentCmd.Connection = thisConn;
                studentCmd.CommandText = "SELECT t.TrainingID, t.Title, t.EventType, t.Description"
                    + " FROM TrainingObjects t"
                    + " INNER JOIN TrainingEvent e ON t.TrainingID = e.TrainingID"
                    + " WHERE e.TrainingEventID = " + trainingEventID.ToString();
                SqlDataReader trainingReader = studentCmd.ExecuteReader(CommandBehavior.Default);
                trainingReader.Read();
                if ((trainingReader[1].ToString() == trainingTitle)
                    && (trainingReader[2].ToString() == trainingType)
                    && (trainingReader[3].ToString() == trainingDescription))
                {
                    trainingID = int.Parse(trainingReader[0].ToString());
                    trainingReader.Close();
                }
                else
                {
                    trainingReader.Close();
                    //20. If updated, was it updated to a training object that already exists?
                    SqlCommand changeTrainingCmd = new SqlCommand();
                    changeTrainingCmd.Connection = thisConn;
                    changeTrainingCmd.CommandText = "SELECT TrainingID"
                    + " FROM TrainingObjects"
                    + " WHERE Title = '" + trainingTitle + "'"
                    + " AND EventType = '" + trainingType + "'"
                    + " AND Description = '" + trainingDescription + "'";
                    SqlDataReader changeTrainingReader = changeTrainingCmd.ExecuteReader(CommandBehavior.Default);
                    if (changeTrainingReader.Read())
                    {
                        //21. If so, get existing Training Object ID
                        trainingID = int.Parse(changeTrainingReader[0].ToString());
                        changeTrainingReader.Close();
                    }
                    else
                    {
                        changeTrainingReader.Close();
                        //22. If not, insert new training object and get the new training ID
                        SqlCommand addTrainingCommand = new SqlCommand();
                        addTrainingCommand.Connection = thisConn;
                        addTrainingCommand.CommandText = "INSERT TrainingObjects(Title, EventType, Description) VALUES('"
                            + trainingTitle + "','"
                            + trainingType + "','"
                            + trainingDescription + "')";
                        addTrainingCommand.ExecuteNonQuery();
                        SqlCommand getNewTrainingCmd = new SqlCommand();
                        getNewTrainingCmd.Connection = thisConn;
                        getNewTrainingCmd.CommandText = "SELECT TrainingID"
                    + " FROM TrainingObjects"
                    + " WHERE Title = '" + trainingTitle + "'"
                    + " AND EventType = '" + trainingType + "'"
                    + " AND Description = '" + trainingDescription + "'";
                        SqlDataReader getNewTrainingReader = getNewTrainingCmd.ExecuteReader(CommandBehavior.Default);
                        getNewTrainingReader.Read();
                        trainingID = int.Parse(getNewTrainingReader[0].ToString());
                        getNewTrainingReader.Close();
                    }
                }
                //23. At this point, we have studentID, and trainingID, so go ahead and update the TrainingEvents table
                SqlCommand updateEventCommand = new SqlCommand();
                updateEventCommand.Connection = thisConn;
                updateEventCommand.CommandText = "UPDATE TrainingEvent"
                    + " SET StudentID=" + studentID
                    + ", TrainingID=" + trainingID
                    + ", EventDate='" + trainingDate.ToShortDateString() + "'"
                    + ", Status='" + trainingStatus + "'"
                    + " WHERE TrainingEventID=" + trainingEventID;
                updateEventCommand.ExecuteNonQuery();
            }
            finally
            {
                thisConn.Dispose();
            }
        }

        public static TrainingEvent Create(TrainingEvent newTrainingEventEntity)
        {
            SqlConnection thisConn = null;
            try
            {
                thisConn = getSqlConnection();
                thisConn.Open();
                //Get new values to push through in the create
                string studentName = newTrainingEventEntity.LoginName;
                string trainingTitle = newTrainingEventEntity.Title;
                string trainingType = newTrainingEventEntity.EventType;
                string trainingDescription = newTrainingEventEntity.Description;
                DateTime trainingDate = newTrainingEventEntity.EventDate;
                string trainingStatus = newTrainingEventEntity.Status;
                //25. See if LoginName already exists
                int studentID = 0;
                SqlCommand studentCmd = new SqlCommand();
                studentCmd.Connection = thisConn;
                studentCmd.CommandText = "SELECT StudentID"
                    + " FROM Student"
                    + " WHERE LoginName='" + studentName + "'";
                SqlDataReader studentReader = studentCmd.ExecuteReader(CommandBehavior.Default);
                if (studentReader.Read())
                {
                    //26. If so, get existing Student ID
                    studentID = int.Parse(studentReader[0].ToString());
                    studentReader.Close();
                }
                else
                {
                    studentReader.Close();
                    //27. If not, insert new student and get the new user ID
                    SqlCommand addStudentCommand = new SqlCommand();
                    addStudentCommand.Connection = thisConn;
                    addStudentCommand.CommandText = "INSERT Student(LoginName) VALUES('" + studentName + "')";
                    addStudentCommand.ExecuteNonQuery();
                    SqlCommand getNewStudentCmd = new SqlCommand();
                    getNewStudentCmd.Connection = thisConn;
                    getNewStudentCmd.CommandText = "SELECT StudentID"
                    + " FROM Student"
                    + " WHERE LoginName = '" + studentName + "'";
                    SqlDataReader getNewStudentReader = getNewStudentCmd.ExecuteReader(CommandBehavior.Default);
                    getNewStudentReader.Read();
                    studentID = int.Parse(getNewStudentReader[0].ToString());
                    getNewStudentReader.Close();
                }
                //28. See if training object already exists
                int trainingID = 0;
                SqlCommand trainingCmd = new SqlCommand();
                trainingCmd.Connection = thisConn;
                trainingCmd.CommandText = "SELECT TrainingID"
                    + " FROM TrainingObjects"
                    + " WHERE Title = '" + trainingTitle + "'"
                    + " AND EventType = '" + trainingType + "'"
                    + " AND Description = '" + trainingDescription + "'";
                SqlDataReader trainingReader = trainingCmd.ExecuteReader(CommandBehavior.Default);
                if (trainingReader.Read())
                {
                    //29. If so, get existing training object ID
                    trainingID = int.Parse(trainingReader[0].ToString());
                    trainingReader.Close();
                }
                else
                {
                    trainingReader.Close();
                    //30. If not, insert new training object and get the new training ID
                    SqlCommand addTrainingCommand = new SqlCommand();
                    addTrainingCommand.Connection = thisConn;
                    addTrainingCommand.CommandText = "INSERT TrainingObjects(Title, EventType, Description) VALUES('"
                        + trainingTitle + "','"
                        + trainingType + "','"
                        + trainingDescription + "')";
                    addTrainingCommand.ExecuteNonQuery();
                    SqlCommand getNewTrainingCmd = new SqlCommand();
                    getNewTrainingCmd.Connection = thisConn;
                    getNewTrainingCmd.CommandText = "SELECT TrainingID"
                + " FROM TrainingObjects"
                + " WHERE Title = '" + trainingTitle + "'"
                + " AND EventType = '" + trainingType + "'"
                + " AND Description = '" + trainingDescription + "'";
                    SqlDataReader getNewTrainingReader = getNewTrainingCmd.ExecuteReader(CommandBehavior.Default);
                    getNewTrainingReader.Read();
                    trainingID = int.Parse(getNewTrainingReader[0].ToString());
                    getNewTrainingReader.Close();
                }
                //31. At this point, we have studentID, and trainingID, so go ahead and insert a new row in the TrainingEvent table
                SqlCommand insertEventCommand = new SqlCommand();
                insertEventCommand.Connection = thisConn;
                insertEventCommand.CommandText = "INSERT TrainingEvent(StudentID, TrainingID, EventDate, Status) VALUES("
                    + studentID
                    + ", " + trainingID
                    + ", '" + trainingDate.ToShortDateString() + "'"
                    + ", '" + trainingStatus + "')";
                insertEventCommand.ExecuteNonQuery();
                return (newTrainingEventEntity);
            }
            finally
            {
                thisConn.Dispose();
            }
        }

            
    }
}
