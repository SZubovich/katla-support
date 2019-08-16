using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class ServiceAccount
    {
        EmployeeInfo Info { get; set; }
        IEnumerable<Task> Tasks { get; set; }
    }
}
