using NPLDataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.ReportsContainer
{
    public interface IReportService
    {
        Task<IEnumerable<SubscriptionDTO>> ListActiveSubscribedUsers();
        Task<IEnumerable<SubscriptionDTO>> ListExpiredSubscribedUsers();
        Task<IEnumerable<ClientDTO>> ListClientsByRegion(int regionId);

        
        Task<SubscriptionDTO> ActiveUsersGroupedByPublications();
        Task<SubscriptionDTO>  UsersGroupedByPublications();

        Task<ReceiptDTO> GetReceipt(int paymentId);

        Task<ReportDashBoardDTO> SetupReportDashboard();
    }
}
