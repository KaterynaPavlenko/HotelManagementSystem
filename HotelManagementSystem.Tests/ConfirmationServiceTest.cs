using System.Collections.Generic;
using System.Linq;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;
using HotelManagementSystem.BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HotelManagementSystem.Tests
{
    [TestClass]
    public class ConfirmationServiceTest
    {
        private Mock<IRepository<Confirmation>> _confirmationRepository;
        private IConfirmationService _confirmationService;
        private Mock<IUnitOfWork> _unitOfWork;
        private List<Confirmation> confirmations;

        [TestInitialize]
        public void SetUp()
        {
            confirmations = new List<Confirmation>
            {
                new Confirmation
                {
                    Id = 0,
                    RoomId = 0,
                    CustomerRequestId = 0
                },
                new Confirmation
                {
                    Id = 1,
                    RoomId = 1,
                    CustomerRequestId = 1
                },
                new Confirmation
                {
                    Id = 2,
                    RoomId = 1,
                    CustomerRequestId = 1
                },
                new Confirmation
                {
                    Id = 3,
                    RoomId = 1,
                    CustomerRequestId = 1
                }
            };
            // Create a new mock of the repository
            _confirmationRepository = new Mock<IRepository<Confirmation>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            // Set up the mock for the repository
            _unitOfWork.Setup(x => x.Confirmations.GetAll()).Returns(confirmations);
            _confirmationRepository.Setup(x => x.GetAll())
                .Returns(confirmations);
            // Create the service and inject the repository into the service
            _confirmationService = new ConfirmationService(_unitOfWork.Object);
        }

        [TestMethod]
        public void ConfirmationService_Get_All_Confirmation()
        {
            // Act
            var confirmationActual = _confirmationService.GetAll();

            // Assert
            Assert.AreEqual(4, confirmationActual.Count(), "The room count is not correct");
        }

        [TestMethod]
        public void ConfirmationService_Can_GetById_Valid_Confirmation()
        {
            //Arrange
            var confirmation = new Confirmation
            {
                Id = 1,
                CustomerRequestId = 1,
                RoomId = 1
            };


            _unitOfWork.Setup(m => m.Confirmations.GetById(confirmation.Id)).Returns(confirmation);
            // Act
            var actual = _confirmationService.GetById(confirmation.Id);

            //Assert
            _unitOfWork.Verify(); //verify that GetByID was called based on setup.
            Assert.IsNotNull(actual); //assert that a result was returned
            Assert.AreEqual(confirmation, actual); //assert that actual result was as expected
        }

        [TestMethod]
        public void ConfirmationService_Can_Update_Confirmation()
        {
            //Arrange
            var confirmation = confirmations.ElementAt(0);
            confirmation.CustomerRequestId = 5;

            // Act

            _confirmationService.Update(confirmation);
            var actual = _confirmationService.GetAll().ToList();
            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(confirmation, actual[0], "Actual result was not as expected");
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void ConfirmationService_Can_Delete_Confirmation()
        {
            //Arrange
            var DeletedID = 1;

            _unitOfWork.Setup(m => m.Confirmations.Delete(DeletedID));

            // Act
            _confirmationService.Delete(DeletedID);
            // Assert
            _unitOfWork.Verify(v => v.Confirmations.Delete(DeletedID), Times.Once());
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void ConfirmationService_Can_Create_Room()
        {
            //Arrange
            var confirmation = new Confirmation
            {
                CustomerRequestId = 1,
                RoomId = 1
            };
            _unitOfWork.Setup(m => m.Confirmations.Create(confirmation));
            // Act
            _confirmationService.Create(confirmation);
            // Assert
            _unitOfWork.Verify(v => v.Confirmations.Create(confirmation), Times.Once());
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }
    }
}