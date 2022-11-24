using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using bitsEFClasses.Models;

namespace bitsEFClassesTests
{
    [TestFixture]  
    public class AddressTypeTests
    {
        bitsContext dbContext;
        AddressType? aT;
        List<AddressType> addressTypes;

        [SetUp]
        public void SetUp()
        {
            dbContext = new bitsContext();
        }

        [Test]
        public void GetAllAddressesTypeTest()
        {
            addressTypes = dbContext.AddressTypes.OrderBy(aT => aT.AddressTypeId).ToList();
            Assert.AreEqual(3, addressTypes.Count);
            Assert.AreEqual("billing", addressTypes[0].Name);
            Assert.AreEqual("mailing", addressTypes[1].Name);
            Assert.AreEqual("shipping", addressTypes[2].Name);
            PrintAll(addressTypes);
        }

        [Test]
        public void GetByPrimaryKeyAddressTypeTest()
        {
            aT = dbContext.AddressTypes.Find(1);
            Assert.IsNotNull(aT);
            Assert.AreEqual("billing", aT.Name);
            Console.WriteLine(aT);
        }

        [Test]
        public void GetUsingWhereAddressTypeTest()
        {
            addressTypes = dbContext.AddressTypes.Where(aT => aT.Name == "shipping").OrderBy(aT => aT.AddressTypeId).ToList();
            Assert.AreEqual(1, addressTypes.Count);
            PrintAll(addressTypes);              
        }

        [Test]
        public void GetWithJoinedAddressesTypeTest()
        {
            // This is another Join test that I am waiting on, I would like to get feedback on my join test from my "SupplierTests" - before I write this. 

            // I am not sure if I got it right, but I think I am at least close. Unsure if it's really joining.. unsure if WHAT to assert isEqual - for confirmation

            // Want to get feedback, and work on other stuff while I wait. Happy Thanksgiving ;)
        }

        [Test]
        public void CreateSupplierAddressTypeTest()
        {
            aT = new AddressType();
            aT.Name = "personal";
            dbContext.AddressTypes.Add(aT);
            dbContext.SaveChanges();
            Assert.AreEqual("personal", aT.Name);
            Console.WriteLine(aT);
        }


        [Test]
        public void UpdateSupplierAddressTypeTest()
        {
            aT = dbContext.AddressTypes.Find(4);
            aT.Name = "contact";

            dbContext.SaveChanges();
            aT = dbContext.AddressTypes.Find(4);
            Assert.AreEqual("contact", aT.Name);
            Console.WriteLine(aT);
        }

        [Test]
        public void DeleteSupplierAddressTypeTest()
        {
            aT = dbContext.AddressTypes.Find(4);
            dbContext.AddressTypes.Remove(aT);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.AddressTypes.Find(4));
        }

        
        public void PrintAll(List<AddressType> addressTypes)
        {
            foreach (AddressType a in addressTypes)
            {
                Console.WriteLine(a);
            }
        }






    }
}