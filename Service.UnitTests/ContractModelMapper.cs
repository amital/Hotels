using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payoneer.Payoneer.Hotels.Contracts;
using Payoneer.Payoneer.Hotels.Model.ExampleDomain2;
using Payoneer.Payoneer.Hotels.WebApi.ContractModelMapping;

namespace Payoneer.Payoneer.Hotels.Service.UnitTests
{
    [TestClass]
    public class ContractModelMapperTests
    {
        [TestMethod]
        public void Map_ModelToContract_Equivalent()
        {
            //Arrange
            var id = Guid.NewGuid();
            var model = GetTestExampleModel(id);
            var contract = GetTestExampleContract(id);

            //Act
            var modelToContract = model.ToContract();

            //Assert
            modelToContract.Should().BeEquivalentTo(contract);
        }

        [TestMethod]
        public void Map_NullModelToContract_NullContract()
        {
            //Arrange & Act
            var modelToContract = ((ExampleMessageModel)null).ToContract();

            //Assert
            Assert.IsNull(modelToContract);
        }

        [TestMethod]
        public void Map_ContractToModel_Equivalent()
        {
            //Arrange
            var id = Guid.NewGuid();
            var model = GetTestExampleModel(id);
            var contract = GetTestExampleContract(id);

            //Act
            var contractToModel = contract.ToModel();

            //Assert
            contractToModel.Should().BeEquivalentTo(model);
        }

        [TestMethod]
        public void Map_NullContractToModel_NullModel()
        {
            //Arrange & Act
            var contractToModel = ((ExampleMessage)null).ToModel();

            //Assert
            Assert.IsNull(contractToModel);
        }

        private static ExampleMessageModel GetTestExampleModel(Guid id)
        {
            return new ExampleMessageModel
            {
                Id = id
            };
        }

        private static ExampleMessage GetTestExampleContract(Guid id)
        {
            return new ExampleMessage
            {
                Id = id
            };
        }
    }
}
