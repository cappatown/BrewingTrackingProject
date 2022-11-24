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
        public void GetByPrimaryKeyAddressTest()
        {
            a = dbContext.Addresses.Find(1);
            Assert.IsNotNull(a);
            Assert.AreEqual("800 West 1st Ave", a.StreetLine1);
            Assert.AreEqual(null, a.StreetLine2);
            Assert.AreEqual("Shakopee", a.City);
            Assert.AreEqual("MN", a.State);
            Assert.AreEqual("55379", a.Zipcode);
            Assert.AreEqual("USA", a.Country);
            Console.WriteLine(a);
        }

        [Test]
        public void GetUsingWhereAddressTest()
        {
            // Get all addresses from Washington
            addresses = dbContext.Addresses.Where(a => a.State == "WA").OrderBy(a => a.AddressId).ToList();
            Assert.AreEqual(2, addresses.Count);
            PrintAll(addresses);                    
        }

        [Test]
        public void GetWithJoinedAddressesTest()
        {
            // This is another Join test that I am waiting on, I would like to get feedback on my join test from my "SupplierTests" - before I write this. 

            // I am not sure if I got it right, but I think I am at least close. Unsure if it's really joining.. unsure if WHAT to assert isEqual - for confirmation

            // Want to get feedback, and work on other stuff while I wait. Happy Thanksgiving ;)
        }

        [Test]
        public void CreateSupplierAddressTest()
        {
            // Add address for Riverbend Malt House
            a = new Address();
            a.StreetLine1 = "12 Gerber Rd Ste C";
            a.StreetLine2 = null;
            a.City = "Asheville";
            a.State = "NC";
            a.Zipcode = "28803";
            a.Country = "USA";

            dbContext.Addresses.Add(a);
            dbContext.SaveChanges();
            Assert.AreEqual("12 Gerber Rd Ste C", a.StreetLine1);
            Assert.AreEqual(null, a.StreetLine2);
            Assert.AreEqual("Asheville", a.City);
            Assert.AreEqual("NC", a.State);
            Assert.AreEqual("28803", a.Zipcode);
            Assert.AreEqual("USA", a.Country);
            Console.WriteLine(a);
        }


        [Test]
        public void UpdateSupplierAddressTest()
        {
            // Update so the 'Ste C' is in the street_line_2 insead of street_line_1, will get rid of null - should be better formatting
            a = dbContext.Addresses.Find(8);
            a.StreetLine1 = "12 Gerber Rd";
            a.StreetLine2 = "Ste C";
            a.City = "Asheville";
            a.State = "NC";
            a.Zipcode = "28803";
            a.Country = "USA";

            dbContext.SaveChanges();
            a = dbContext.Addresses.Find(8);
            Assert.AreEqual("Ste C", a.StreetLine2);
            Console.WriteLine(a);
        }

        [Test]
        public void DeleteSupplierAddressTest()
        {
            // Delete Riverbend Malt House Address
            a = dbContext.Addresses.Find(8);
            dbContext.Addresses.Remove(a);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Addresses.Find(8));
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