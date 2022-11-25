using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace bitsEFClasses.Models
{
    public partial class SupplierAddress
    {
        public int SupplierId { get; set; }
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }

        public override string ToString()
        {
            return SupplierId + ", " + AddressId + ", " + AddressTypeId;
        }

        public virtual Address? Address { get; set; } //= null!;
        public virtual AddressType? AddressType { get; set; } //= null!;
        public virtual Supplier? Supplier { get; set; } //= null!;
    }
}
