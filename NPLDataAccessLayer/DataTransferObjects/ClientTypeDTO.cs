using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.DataTransferObjects
{
    public class ClientTypeDTO
    {
        public int ClientTypeId { get; set; }
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }

    }
}
