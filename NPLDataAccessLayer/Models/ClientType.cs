using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class ClientType
    {
        public ClientType()
        {
            Clients = new HashSet<Client>();
        }

        public int ClientTypeId { get; set; }
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
