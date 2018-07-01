using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payoneer.Payoneer.Hotels.Model.ExampleDomain;
using Payoneer.ServicesInfra.Repositories;
using Payoneer.ServicesInfra.TestingUtils.Database;

namespace Payoneer.Payoneer.Hotels.Service.IntegrationTests
{
    [TestClass]
    public class ExampleDomainMappingTest
    {

        #region Initialization and Cleanup

        private UnitOfWork unitOfWork;

        private const string LocalDbInstance = @"TestDB";
        private static readonly string ConnectionString =
            $@"data source=(localdb)\{LocalDbInstance};initial catalog=Hotels_{nameof(ExampleDomainMappingTest)};Integrated Security=SSPI;MultipleActiveResultSets=True;MultiSubnetFailover=True;App=Hotels_{nameof(ExampleDomainMappingTest)}";

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            DbDeployer.StartLocalDb(LocalDbInstance, testContext, typeof(ExampleDomainContext));
            InitDb();
        }

        private static void InitDb()
        {
            using (var context = GetDb())
            {
                context.ApplyChanges();
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            unitOfWork = new UnitOfWork();
            unitOfWork.BeginOuterReadCommited();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            unitOfWork.Rollback();
            unitOfWork.Dispose();
        }

        #endregion

        [TestMethod]
        public void ExampleModel_SaveAndGet_Success()
        {
            //Arrange
            var toAdd = GetTestItem(Guid.NewGuid());
            using (var context = GetDb())
            {
                context.Examples.Add(toAdd);
                context.ApplyChanges();
            }

            //Act
            ExampleModel fromDb;
            using (var context = GetDb())
            {
                fromDb = context.Examples.AsQueryable().FirstOrDefault(x => x.Id == toAdd.Id);
            }

            //Assert
            Assert.IsNotNull(fromDb);
            fromDb.Should().BeEquivalentTo(toAdd);
        }

        private static ExampleModel GetTestItem(Guid id)
        {
            return new ExampleModel
            {
                Id = id
            };
        }

        private static ExampleDomainContext GetDb()
        {
            return new ExampleDomainContext(ConnectionString);
        }
    }
}
