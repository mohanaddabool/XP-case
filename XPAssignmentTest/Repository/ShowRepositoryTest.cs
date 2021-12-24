using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XPAssignment.Context;
using XPAssignment.DbModels;
using XPAssignment.Repository.Implementations;

namespace XPAssignmentTest.Repository
{
    [TestClass]
    public class ShowRepositoryTest
    {
        [TestMethod]
        public void GetAllShowsTest()
        {
            var options = new DbContextOptionsBuilder<XPAssignmentDbContext>()
                .UseInMemoryDatabase(databaseName: "MyDatabase")
                .Options;
            var dbContext = new XPAssignmentDbContext(options);
            var list =  AddShows(2);
            dbContext.Shows.AddRange(list);
            dbContext.SaveChanges();

            var showRepository = new ShowRepository(dbContext);

            var outShow = showRepository.GetAll();
            Assert.IsNotNull(outShow, $"Shows are not found");
            Assert.AreEqual(2, outShow.Result.Count, $"Show counts are not equal");
        }

        [TestMethod]
        public void GetShowByNameTest()
        {
            var options = new DbContextOptionsBuilder<XPAssignmentDbContext>()
                .UseInMemoryDatabase(databaseName: "MyDatabase")
                .Options;
            var dbContext = new XPAssignmentDbContext(options);
            var list = AddShows(1);
            dbContext.Shows.Add(list[0]);
            dbContext.SaveChanges();

            var showRepository = new ShowRepository(dbContext);

            var outShow = showRepository.GetByName(list[0].Name);
            Assert.IsNotNull(outShow, $"Show with name {list[0].Name} is not found");
            Assert.AreEqual(list[0].Name, outShow.Result?.Name, "Show are not equal to expected name");
        }

        [TestMethod]
        public void AddShowTest()
        {
            var options = new DbContextOptionsBuilder<XPAssignmentDbContext>()
                .UseInMemoryDatabase(databaseName: "MyDatabase")
                .Options;
            var dbContext = new XPAssignmentDbContext(options);

            var list = AddShows(1);
            dbContext.Shows.Add(list[0]);
            dbContext.SaveChanges();

            var showRepository = new ShowRepository(dbContext);
            var show2 = new Show
            {
                Name = "My test show",
                Language = "test language",
                Premiered = DateTime.Now,
                Summary = "Test summary"
            };

            var outShow = showRepository.AddShow(show2);
            Assert.IsNotNull(outShow, $"Show with name {show2.Name} is not found");
            Assert.AreEqual(show2.Name, outShow?.Result?.Name, $"Names are not equal");
            Assert.AreEqual(show2.Id, outShow?.Result?.Id, $"Ids are not equal");
        }

        private List<Show> AddShows(int count)
        {
            var list = new List<Show>();
            for (var i = 0; i < count; i++)
            {
                string name = $"My test show {i + 1}";
                var show = new Show
                {
                    Id = i + 1,
                    Name = name,
                    Language = $"Test language {i + 1}",
                    Premiered = DateTime.Now,
                    Summary = $"Test summary {i + 1}",
                    Genres = AddGenres(i + 1),
                };
                list.Add(show);
            }

            return list;
        }

        private List<Genre> AddGenres(int id)
        {
            var result = new List<Genre>();
            for (var i = 0; i < 2; i++)
            {
                var name = $"Genre {i + 1}";
                result.Add(new Genre
                {
                    Name = name,
                    ShowId = id,
                });
            }

            return result;
        }

    }
}
