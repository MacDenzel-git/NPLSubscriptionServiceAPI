using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.DataTransferObjects
{
    public class PublicationStats
    {

        public string PublicationTitle { get; set; }
        public string RegionName { get; set; }
        public int RegionId { get; set; }
        public int NumberOfSubscribers { get; set; }
    }
}
