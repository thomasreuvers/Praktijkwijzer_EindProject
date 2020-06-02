using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaasVallei.Entities;
using MaasVallei.Models;
using MongoDB.Driver;

namespace MaasVallei.Services
{
    public class ComplaintsService
    {
        private readonly IMongoCollection<Complaint> _complaints;

        public ComplaintsService(IMaasValleiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _complaints = database.GetCollection<Complaint>(settings.ComplaintsCollectionName);
        }

        public IEnumerable<Complaint> Get() => _complaints.Find(complaint => true).ToList();

        public Complaint Get(string id) => _complaints.Find(x => x.Id == id).FirstOrDefault();

        public void Create(Complaint complaint) => _complaints.InsertOne(complaint);

        public void Update(Complaint complaint) => _complaints.ReplaceOne(x => x.Id == complaint.Id, complaint);
    }
}
