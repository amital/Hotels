using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Omu.ValueInjecter;
using Payoneer.Payoneer.Hotels.Model.ExampleDomain;
using Payoneer.ServicesInfra.DependencyInjection.Registering;
using Payoneer.ServicesInfra.DependencyInjection.Unity;
using Payoneer.ServicesInfra.TestingUtils.FakeItEasy;

namespace Payoneer.Payoneer.Hotels.Service.UnitTests
{
    [TestClass]
    public class ExampleServiceTests
    {
        private DiRegistrar di;
        private IExampleService service;
        private List<ExampleModel> examples;

        [TestInitialize]
        public void TestInit()
        {
            di = new UnityRegistrar();
            di.ConnectToServiceLocator();

            var context = Ax.FakeContext<IExampleDomainContext>();
            di.RegisterAsSingleInstance(context);

            examples = new List<ExampleModel>();
            Ax.UseList(() => context.Examples, examples);

            A.CallTo(() => context.Examples.Update(A<ExampleModel>.Ignored)).Invokes(call =>
            {
                var example = (ExampleModel)call.Arguments[0];
                examples.RemoveAll(x => x.Id == example.Id);
                examples.Add(example);
            });

            A.CallTo(() => context.Examples.Delete(A<Expression<Func<ExampleModel, bool>>>.Ignored)).Invokes(
                call =>
                {
                    var predicate = (Expression<Func<ExampleModel, bool>>)call.Arguments[0];
                    examples.RemoveAll(x => predicate.Compile()(x));
                });

            service = new ExampleService();
        }

        [TestMethod]
        public void Add_AddedToDb()
        {
            //Arrange
            var exampleToAdd = GetTestExample(Guid.NewGuid());

            //Act
            service.AddAsync(exampleToAdd);

            //Assert
            var exampleAdded = examples.SingleOrDefault();
            Assert.IsNotNull(exampleAdded);
            exampleAdded.Should().BeEquivalentTo(exampleToAdd);
        }
        
        [TestMethod]
        public void Update_UpdatedInDb()
        {
            //Arrange
            var toBeUpdatedId = Guid.NewGuid();
            var toNotBeUpdatedId = Guid.NewGuid();
            var existingExample = GetTestExample(toBeUpdatedId);
            examples.Add(existingExample);

            var shouldNotBeUpdatedExample = GetTestExample(toNotBeUpdatedId);
            examples.Add(shouldNotBeUpdatedExample);

            var updatedExample = CloneExample(existingExample);
            updatedExample.LookupDataId += 1;

            //Act
            service.UpdateAsync(updatedExample);

            //Assert
            var shouldBeUpdatedExampleFromDb = examples.SingleOrDefault(bd => bd.Id == toBeUpdatedId);
            Assert.IsNotNull(shouldBeUpdatedExampleFromDb);
            shouldBeUpdatedExampleFromDb.Should().BeEquivalentTo(updatedExample);

            var shouldNotBeUpdaetdExampleFromDb = examples.SingleOrDefault(bd => bd.Id == toNotBeUpdatedId);
            Assert.IsNotNull(shouldNotBeUpdaetdExampleFromDb);
            shouldNotBeUpdaetdExampleFromDb.Should().BeEquivalentTo(shouldNotBeUpdatedExample);
        }

        [TestMethod]
        public void Get_Exists_Returned()
        {
            //Arrange
            Guid id = Guid.NewGuid(), otherId = Guid.NewGuid();
            var expectedExample = GetTestExample(id);
            examples.Add(expectedExample);

            var otherExample = GetTestExample(otherId);
            examples.Add(otherExample);

            //Act
            var exampleFromService = service.GetAsync(id);

            //Assert
            Assert.IsNotNull(exampleFromService);
            exampleFromService.Should().BeEquivalentTo(expectedExample);
        }

        [TestMethod]
        public void Get_NotExists_ReturnedNull()
        {
            //Arrange
            Guid id = Guid.NewGuid(), otherId = Guid.NewGuid();
            var example = GetTestExample(id);
            examples.Add(example);

            //Act
            var exampleFromService = service.GetAsync(otherId);

            //Assert
            Assert.IsNull(exampleFromService);
        }

        [TestMethod]
        public void Delete_Exists_RemovedFromDb()
        {
            //Arrange
            Guid id = Guid.NewGuid(), otherId = Guid.NewGuid();

            var example = GetTestExample(id);
            examples.Add(example);

            var otherExample = GetTestExample(otherId);
            examples.Add(otherExample);

            //Act
            service.DeleteAsync(id);

            //Assert
            Assert.IsFalse(examples.Any(x => x.Id == id));
            Assert.IsTrue(examples.Any(x => x.Id == otherId));
        }

        [TestMethod]
        public void Delete_NotExists_ThrowsException()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act & Assert -- nothing should happen
            service.DeleteAsync(id);
        }

        private static ExampleModel GetTestExample(Guid id)
        {
            return new ExampleModel
            {
                Id = id,
                LookupDataId = 1
            };
        }

        private static ExampleModel CloneExample(ExampleModel existingExample)
        {
            return (ExampleModel)new ExampleModel().InjectFrom(existingExample);
        }
    }
}
