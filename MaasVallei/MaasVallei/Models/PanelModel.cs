using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaasVallei.Models
{
    public class PanelModel
    {
        public int Reservations { get; set; }

        public List<ComplaintsModel> Complaints { get; set; }

        public ReservationModel CurrentUserReservationModel { get; set; }
    }
}
