using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using bitsEFClasses.Models;

namespace bitsEFClassesTests
{
    [TestFixture]  
    
    // Line 68 get with joined test not done?? Could you please look at what I have and tell me if I am close? 
    // Not sure how to confirm the join, and what I should assert to equal for confirmation

    public class SupplierTests
    {
        bitsContext dbContext;
        Supplier? s;
        List<Supplier> suppliers;

        [SetUp]
        public void SetUp()
        {
            dbContext = new bitsContext();
        }

        [Test]
        public void GetAllSuppliersTest()
        {
            // Test all fields
            suppliers = dbContext.Suppliers.OrderBy(s => s.SupplierId).ToList();
            Assert.AreEqual(9, suppliers.Count());
            Assert.AreEqual("BSG Craft Brewing", suppliers[0].Name);
            Assert.AreEqual("18003742739", suppliers[0].Phone);
            Assert.AreEqual("sales@bsgcraft.com", suppliers[0].Email);
            Assert.AreEqual("https://bsgcraftbrewing.com/", suppliers[0].Website);

            Assert.AreEqual("Zach", suppliers[2].ContactFirstName);
            Assert.AreEqual("Grossfeld", suppliers[2].ContactLastName);
            Assert.AreEqual("3606996765", suppliers[2].ContactPhone);
            Assert.AreEqual("zgrossfeld@countrymalt.com", suppliers[2].ContactEmail);
            Assert.AreEqual(null, suppliers[2].Note);

            PrintAll(suppliers);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            s = dbContext.Suppliers.Find(6);
            Assert.IsNotNull(s);
            Assert.AreEqual("White Labs", s.Name);
            Console.WriteLine(s);
        }

        [Test]
        public void GetUsingWhereTest()
        {
            suppliers = dbContext.Suppliers.Where(s => s.Name.StartsWith("M")).OrderBy(s => s.SupplierId).ToList();
            Assert.AreEqual(2, suppliers.Count);
            PrintAll(suppliers);         
        }

        [Test]
        public void GetWithJoinedSupplierAddressTest()
        {
            // Will you look at this code down below? I based it off out or MMABooks lab.. I think it's joining.. but I am not sure? I am not really sure what I should assert equal to check this..

            // I'm trying to connect my supplier database TO the supplier_address

            // The supplier_id from the supplier data, should be joining with the supplier_id database, so we can hook up the same supplier_id to each database. Is this close?

            var supplier = dbContext.Suppliers.Join(
                dbContext.SupplierAddresses,
                s => s.SupplierId,
                sA => sA.SupplierId,
                (s, sA) => new { s.SupplierId, s.Name, s.Phone, s.Email, s.Website, s.ContactFirstName, s.ContactLastName, s.ContactPhone, s.ContactEmail, s.Note }).OrderBy(r => r.SupplierId).ToList();
            foreach (var s in supplier)
            {
                Console.WriteLine(s);
            }


            /*   NOT WORKING, IGNORE
             * There are 4 tables that need to be joined... Supplier has the supplier_id - which should be joined with
             * supplier_address - address_type - and address
             */

            /*
             I need to work on this more.. this was giving me errors: No overload for method 'Join' take 6 arguements. I'm confused if I am supposed
             to write all other tests first, before I can join?
             * 
            var suppliers = dbContext.Suppliers.Join(               --- GroupJoin maybe)
                dbContext.SupplierAddresses,
                dbContext.Addresses,
                dbContext.AddressTypes,
                s => s.SupplierId,
                sA => sA.SupplierId,           
                (s, sA) => new { s.SupplierId, s.Name, s.Phone, s.Email, s.Website, s.ContactFirstName, s.ContactLastName, s.ContactPhone, s.ContactEmail, s.Note}).ToList();
            */
        }

        [Test]
        public void CreateSupplierTest()
        {           
            s = new Supplier();
            s.Name = "Riverbend Malt House";
            s.Phone = "8284501081";
            s.Email = "contact@riverbendmalt.com";
            s.Website = "https://riverbendmalt.com/";
            s.ContactFirstName = "John";
            s.ContactLastName = "Doe";
            s.ContactPhone = "18859626258";
            s.ContactEmail = null;
            s.Note = "Based in NC";

            dbContext.Suppliers.Add(s);
            dbContext.SaveChanges();
            Assert.AreEqual("Riverbend Malt House", s.Name);
            Console.WriteLine(s);          

            // Created 2 more suppliers to be able to set sort bys
            /*
            s = new Supplier();
            s.Name = "The Malt Company";
            s.Phone = "0124-6019000";
            s.Email = "info@maltcompany.com";
            s.Website = "https://maltcompany.com/";
            s.ContactFirstName = "P K";
            s.ContactLastName = "Jain";
            s.ContactPhone = "0124-6019000";
            s.ContactEmail = null;
            s.Note = "Based in India";

            dbContext.Suppliers.Add(s);
            dbContext.SaveChanges();
            Assert.AreEqual("The Malt Company", s.Name);
            Console.WriteLine(s);
            */

            /*
            s = new Supplier();
            s.Name = "Malt Master";
            s.Phone = null;
            s.Email = "email@maltmastersupplier.co.uk";
            s.Website = "https://maltcompany.com/";
            s.ContactFirstName = null;
            s.ContactLastName = null;
            s.ContactPhone = null;
            s.ContactEmail = null;
            s.Note = "Based in the UK";

            dbContext.Suppliers.Add(s);
            dbContext.SaveChanges();
            Assert.AreEqual("Malt Master", s.Name);
            Console.WriteLine(s);
            */
        }

        [Test]
        public void DeleteSupplierTest()
        {
            s = dbContext.Suppliers.Find(7);
            dbContext.Suppliers.Remove(s);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Suppliers.Find(7));
        }

        [Test]
        public void UpdateSupplierTest()
        {
            s = dbContext.Suppliers.Find(11);
            s.ContactEmail = "doejohn@riverbendmalt.com";

            dbContext.SaveChanges();
            s = dbContext.Suppliers.Find(11);
            Assert.AreEqual("doejohn@riverbendmalt.com", s.ContactEmail);
            Assert.AreEqual("Riverbend Malt House", s.Name);
            Console.WriteLine(s);
        }

        public void PrintAll(List<Supplier > suppliers)
        {
            foreach (Supplier s in suppliers)
            {
                Console.WriteLine(s);
            }
        }


    }
}