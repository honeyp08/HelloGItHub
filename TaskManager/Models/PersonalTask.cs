using TaskManagerApp.Interfaces;

namespace TaskManagerApp.Models
{
    public class PersonalTask : TaskItem, INotifiable
    {
        public PersonalTask() => TaskType = "Personal";

        public void SendNotification()
        {
            Console.WriteLine($" Reminder: Personal task '{Title}' is due!");
        }
    }
}
