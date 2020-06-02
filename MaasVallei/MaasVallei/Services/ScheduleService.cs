using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaasVallei.Entities;
using MaasVallei.Models;
using MongoDB.Driver;

namespace MaasVallei.Services
{
    public class ScheduleService
    {

        private readonly IMongoCollection<Rooster> _schedules;

        public ScheduleService(IMaasValleiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _schedules = database.GetCollection<Rooster>(settings.SchedulesCollectionName);
        }

        public IEnumerable<Rooster> Get() => _schedules.Find(schedule => true).ToList();

        public void Create(Rooster rooster) => _schedules.InsertOne(rooster);
    }
}
