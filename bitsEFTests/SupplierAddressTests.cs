using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using bitsEFClasses.Models;

namespace bitsEFClassesTests
{
    [TestFixture]

    // Issue with test on line 77

    // Having trouble with updating supplier_address, when I am able to do this, writing deleteSupplierAddressTest should be easy.

    // This table is using foreign keys... I have more information about what I was thinking in the method

    public class SupplierAddressTests
    {
        bitsContext dbContext;
        SupplierAddress? sA;
        List<SupplierAddress> supplierAddresses;

        [SetUp]
        public void SetUp()
        {
            dbContext = new bitsContext();
        }

        [Test]
        public void GetAllSuppliersAddressesTest()
        {
            supplierAddresses = dbContext.SupplierAddresses.OrderBy(sA => sA.SupplierId).ToList();
            Assert.AreEqual(13, supplierAddresses.Count);
            Assert.AreEqual(1, supplierAddresses[0].SupplierId);
            Assert.AreEqual(1, supplierAddresses[0].AddressId);
            Assert.AreEqual(1, supplierAddresses[0].AddressTypeId);
            PrintAll(supplierAddresses);           
        }

        [Test]
        public void GetAddressIdandTypeIdUsingWhere()
        {
            // This test was a little confusing.. I cannot use .Find() because that says it finds PRIMARY keys, while this is a 
            // foreign key. So I used a WHERE and ORDERBY keyword to be able to find all the address information for Supplier with ID of 1 for example
            supplierAddresses = dbContext.SupplierAddresses.Where(sA => sA.SupplierId == 1).OrderBy(sA => sA.SupplierId).ToList();
            Assert.IsNotNull(supplierAddresses);
            Assert.AreEqual(2, supplierAddresses.Count);
            PrintAll(supplierAddresses);
        }

        [Test]
        public void GetWithJoinedSupplierTest()
        {
            /*
            WIP I'm working on figuring this out.. I'd like you to check my SupplierTest Join method, if I got that right, I should be able to do this.

            Want to get an opinion, while waiting I will work on something else.
            */
        }

        [Test]
        public void CreateSupplierAddressTest()
        {
            sA = new SupplierAddress();
            sA.SupplierId = 8;
            sA.AddressId = 1;
            sA.AddressTypeId = 1;

            dbContext.SupplierAddresses.Add(sA);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.SupplierAddresses.Where(sA => sA.SupplierId == 8));
            Assert.AreEqual(8, sA.SupplierId);
            Console.WriteLine(sA);
        }

        [Test]
        public void UpdateSupplierAddressTest()
        {
            /* I'm unsure of how to update these fields. I would think I would update by index... but what I tried did not work.
             I keep getting a "'object reference not set to an instance of object, it's coming up as null
             I thought I would use an index, since 1 supplier can have multiple rows of information. I am not sure how I would access
             this field without an index - but I must be accessing index wrong.

            It did add something to the database it looks like, but I don't want to add, I want to update. It appears that it inserted 2 and 2
            into address_id and address_type_id, and put in it's own value for supplier_id?

            These are foreign keys, so I don't believe I can use same methods as Primary keys. (Like Find() )

            I believe to delete, I would want the same thing, to be able to access the index?

            */ 

            /* What I tried...
             
            int index = supplierAddresses.FindIndex(sA => sA.SupplierId == 9 && sA.AddressId == 1 && sA.AddressTypeId == 1);  // Trying to find the index of the row
            supplierAddresses[index] = new SupplierAddress(); // Trying to take the index of the row, putting it into a new SupplierAddress

            sA.AddressId = 2;
            sA.AddressTypeId = 2;

            dbContext.SupplierAddresses.Update(supplierAddresses[index]);
            dbContext.SaveChanges();
            Assert.AreEqual(2, sA.AddressId);
            Assert.AreEqual(2, sA.AddressTypeId);
            Console.WriteLine(supplierAddresses[index]);

            */
        }

        public void PrintAll(List<SupplierAddress> supplierAddresses)
        {
            foreach (SupplierAddress sA in supplierAddresses)
            {
                Console.WriteLine(sA);
            }
        }






    }
}