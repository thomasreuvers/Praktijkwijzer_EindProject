using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaasVallei.Models
{
    public class MaasValleiDatabaseSettings : IMaasValleiDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ReservationsCollectionName { get; set; }
        public string SchedulesCollectionName { get; set; }
        public string ComplaintsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMaasValleiDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string ReservationsCollectionName { get; set; }
        string SchedulesCollectionName { get; set; }
        string ComplaintsCollectionName {get; set;}
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
