using System;
using System.Collections.Generic;

namespace SalesSystem.Models
{
    public partial class Client
    {
        public Client()
        {
            Sale = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Sale> Sale { get; set; }
    }
}
