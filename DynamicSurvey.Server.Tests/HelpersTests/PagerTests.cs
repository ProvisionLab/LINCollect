using DynamicSurvey.Server.DAL.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.Tests.HelpersTests
{
    [TestClass]
    public class PagerTests 
    {
        private int[] GeneratePages(int count)
        {
            var res = new int[count];
            for (int i = 0; i < count; i++)
            {
                res[i] = i;
            }

            return res;
        }

        private IPager GeneratePager(int currentPage, int pageSize)
        {
            return new Pager()
            {
                PageSize = pageSize,
                CurrentPage = currentPage
            };
        }


        [TestMethod]
        public void Pager_Displays_1st_Of_4_pages_by_5_items_per_page()
        {
            var pages = GeneratePages(20).AsQueryable();
            var pager = GeneratePager(1, 5);

            var res = pager.SelectPageQuery(pages, x => x).ToArray();

            Assert.AreEqual(4, pager.TotalPages, "Pager: Invalid Pages count");
            Assert.AreEqual(5, res.Length,  "Result: Invalid Lenght");
            Assert.AreEqual(0, res[0]);
            Assert.AreEqual(1, res[1]);
            Assert.AreEqual(2, res[2]);
            Assert.AreEqual(3, res[3]);
            Assert.AreEqual(4, res[4]);
        }

        [TestMethod]
        public void Pager_Displays_4th_Of_4_pages_by_5_items_per_page()
        {
            var pages = GeneratePages(20).AsQueryable();
            var pager = GeneratePager(4, 5);

            var res = pager.SelectPageQuery(pages, x => x).ToArray();

            Assert.AreEqual(4, pager.TotalPages, "Pager: Invalid Pages count");
            Assert.AreEqual(5, res.Length, "Result: Invalid Lenght");
            Assert.AreEqual(15, res[0]);
            Assert.AreEqual(16, res[1]);
            Assert.AreEqual(17, res[2]);
            Assert.AreEqual(18, res[3]);
            Assert.AreEqual(19, res[4]);
        }

        [TestMethod]
        public void Pager_Adjust_zero_out_of_bounds_to_1st_page()
        {
            var pages = GeneratePages(20).AsQueryable();
            var pager = GeneratePager(0, 5);

            var res = pager.SelectPageQuery(pages, x => x).ToArray();

            Assert.AreEqual(4, pager.TotalPages, "Pager: Invalid Pages count");
            Assert.AreEqual(5, res.Length, "Result: Invalid Lenght");
            Assert.AreEqual(0, res[0]);
            Assert.AreEqual(1, res[1]);
            Assert.AreEqual(2, res[2]);
            Assert.AreEqual(3, res[3]);
            Assert.AreEqual(4, res[4]);
        }

        [TestMethod]
        public void Pager_Adjust_negative_out_of_bounds_to_1st_page()
        {
            var pages = GeneratePages(20).AsQueryable();
            var pager = GeneratePager(-1, 5);

            var res = pager.SelectPageQuery(pages, x => x).ToArray();

            Assert.AreEqual(4, pager.TotalPages, "Pager: Invalid Pages count");
            Assert.AreEqual(5, res.Length, "Result: Invalid Lenght");
            Assert.AreEqual(0, res[0]);
            Assert.AreEqual(1, res[1]);
            Assert.AreEqual(2, res[2]);
            Assert.AreEqual(3, res[3]);
            Assert.AreEqual(4, res[4]);
        }

        [TestMethod]
        public void Pager_Adjust_page_index_overflow_out_of_bounds_to_last_page()
        {
            var pages = GeneratePages(20).AsQueryable();
            var pager = GeneratePager(5, 5);

            var res = pager.SelectPageQuery(pages, x => x).ToArray();

            Assert.AreEqual(4, pager.TotalPages, "Pager: Invalid Pages count");
            Assert.AreEqual(5, res.Length, "Result: Invalid Lenght");
            Assert.AreEqual(15, res[0]);
            Assert.AreEqual(16, res[1]);
            Assert.AreEqual(17, res[2]);
            Assert.AreEqual(18, res[3]);
            Assert.AreEqual(19, res[4]);
        }

        [TestMethod]
        public void Pager_Calculates_3_pages_from_10_items_and_page_size_is_4()
        {
            var pages = GeneratePages(10).AsQueryable();
            var pager = GeneratePager(1, 4);

            var res = pager.SelectPageQuery(pages, x => x).ToArray();

            Assert.AreEqual(3, pager.TotalPages);
        }

        [TestMethod]
        public void Pager_Calculates_1_extra_item_on_3rd_page_from_10_items_and_page_size_is_4()
        {
            var pages = GeneratePages(10).AsQueryable();
            var pager = GeneratePager(3, 4);

            var res = pager.SelectPageQuery(pages, x => x).ToArray();

            Assert.AreEqual(2, res.Length);
            Assert.AreEqual(8, res[0]);
            Assert.AreEqual(9, res[1]);
        }

        
    }
}
