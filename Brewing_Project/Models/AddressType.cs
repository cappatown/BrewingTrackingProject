using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;

namespace bitsEFClasses.Models
{
    public partial class AddressType
    {
        public AddressType()
        {
            SupplierAddresses = new HashSet<SupplierAddress>();
        }

        public int AddressTypeId { get; set; }
        public string? Name { get; set; }

        public override string ToString()
        {
            return AddressTypeId + ", " + Name;
        }

        public virtual ICollection<SupplierAddress> SupplierAddresses { get; set; }
    }
}
