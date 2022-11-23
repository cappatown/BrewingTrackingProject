using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using bitsEFClasses.Models;

namespace bitsEFClassesTests
{
    [TestFixture]  
    public class AddressTests
    {
        bitsContext dbContext;
        Address? a;
        List<Address> addresses;

        [SetUp]
        public void SetUp()
        {
            dbContext = new bitsContext();
        }

        [Test]
        public void GetAllAddressesTest()
        {
            addresses = dbContext.Addresses.OrderBy(a => a.AddressId).ToList();
            Assert.AreEqual(7, addresses.Count);
            Assert.AreEqual("800 West 1st Ave", addresses[0].StreetLine1);
            Assert.AreEqual(null, addresses[0].StreetLine2);
            Assert.AreEqual("Shakopee", addresses[0].City);
            Assert.AreEqual("MN", addresses[0].State);
            Assert.AreEqual("55379", addresses[0].Zipcode);
            Assert.AreEqual("USA", addresses[0].Country);
            PrintAll(addresses);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            
        }

        [Test]
        public void GetUsingWhereTest()
        {
                     
        }

        [Test]
        public void GetWithJoinedAddressesTest()
        {
            
        }

        [Test]
        public void CreateSupplierTest()
        {
         
        }

        [Test]
        public void DeleteSupplierTest()
        {

        }

        [Test]
        public void UpdateSupplierTest()
        {

        }

        public void PrintAll(List<Address> addresses)
        {
            foreach (Address a in addresses)
            {
                Console.WriteLine(a);
            }
        }






    }
}