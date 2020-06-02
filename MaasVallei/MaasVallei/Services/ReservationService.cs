using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaasVallei.Controllers;
using MaasVallei.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MaasVallei.Services
{
    public class ReservationService
    {
        private readonly IMongoCollection<Reservation> _reservations;

        public ReservationService(IMaasValleiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _reservations = database.GetCollection<Reservation>(settings.ReservationsCollectionName);
        }

        public IEnumerable<Reservation> Get() => _reservations.Find(reservation => true).ToList();

        public Reservation Get(string id)
        {
            return _reservations.Find(reservation => reservation.Id == id).FirstOrDefault();
        } 

        public void Create(Reservation reservation) => _reservations.InsertOne(reservation);

        public void Update(Reservation reservation) => _reservations.ReplaceOne(x => x.Id.Equals(reservation.Id), reservation);

        public void Delete(string id) => _reservations.DeleteOne(reservation => reservation.Id == id);
    }
}
