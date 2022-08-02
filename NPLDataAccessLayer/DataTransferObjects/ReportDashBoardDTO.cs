using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.DataTransferObjects
{
    public class ReportDashBoardDTO
    {
        public int NumberOfSubscribers { get; set; }
        public int NewsLettersSent { get; set; }
        public decimal TotalAmountInSubscription { get; set; }
        public int NumberOfUsersRegisteredToday { get; set; }
        public IEnumerable<PublicationStats> PublicationStatistics  { get; set; }
        public IEnumerable<SubscriptionDTO> ActiveSubscriptions { get;set; }
        public IEnumerable<SubscriptionDTO> ExpiredSubscriptions { get; set; }
        public IEnumerable<PaymentTypeDTO> PaymentTypes { get; set; }
        public IEnumerable<RegionDTO> Regions { get; set; }
        public  SubscriptionDTO  UsersGroupedByPublications { get; set; }
        public  SubscriptionDTO  ActiveUsersGroupedByPublications { get; set; }

    }
}
