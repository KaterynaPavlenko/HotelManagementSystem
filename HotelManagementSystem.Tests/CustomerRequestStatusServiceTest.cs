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
    public class CustomerRequestStatusServiceTest
    {
        private Mock<IRepository<CustomerRequestStatus>> _customerRequestStatusRepository;
        private ICustomerRequestStatusService _customerRequestStatusService;
        private Mock<IUnitOfWork> _unitOfWork;
        private List<CustomerRequestStatus> customerRequestStatuses;


        [TestInitialize]
        public void SetUp()
        {
            customerRequestStatuses = new List<CustomerRequestStatus>
            {
                new CustomerRequestStatus
                {
                    Id = 0,
                    Name = "0"
                },
                new CustomerRequestStatus
                {
                    Id = 1,
                    Name = "1"
                },
                new CustomerRequestStatus
                {
                    Id = 2,
                    Name = "2"
                },
                new CustomerRequestStatus
                {
                    Id = 3,
                    Name = "3"
                }
            };
            // Create a new mock of the repository
            _customerRequestStatusRepository = new Mock<IRepository<CustomerRequestStatus>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            // Set up the mock for the repository
            _unitOfWork.Setup(x => x.CustomerRequestStatuses.GetAll()).Returns(customerRequestStatuses);
            _customerRequestStatusRepository.Setup(x => x.GetAll())
                .Returns(customerRequestStatuses);
            // Create the service and inject the repository into the service
            _customerRequestStatusService = new CustomerRequestStatusService(_unitOfWork.Object);
        }

        [TestMethod]
        public void CustomerRequestStatusService_Get_All_CustomerRequestStatus()
        {
            // Act
            var customerRequestActual = _customerRequestStatusService.GetAll();

            // Assert
            Assert.AreEqual(4, customerRequestActual.Count(), "The room count is not correct");
        }

        [TestMethod]
        public void CustomerRequestStatusService_Can_GetById_Valid_CustomerRequestStatus()
        {
            //Arrange
            var customerRequestStatus = new CustomerRequestStatus
            {
                Id = 1,
                Name = "4"
            };

            _unitOfWork.Setup(m => m.CustomerRequestStatuses.GetById(customerRequestStatus.Id))
                .Returns(customerRequestStatus);
            // Act
            var actual = _customerRequestStatusService.GetById(customerRequestStatus.Id);

            //Assert
            _unitOfWork.Verify(); //verify that GetByID was called based on setup.
            Assert.IsNotNull(actual); //assert that a result was returned
            Assert.AreEqual(customerRequestStatus, actual); //assert that actual result was as expected
        }

        [TestMethod]
        public void CustomerRequestStatusService_Can_Update_CustomerRequestStatus()
        {
            //Arrange
            var customerRequestStatus = customerRequestStatuses.ElementAt(0);
            customerRequestStatus.Name = "5";
            // Act

            _customerRequestStatusService.Update(customerRequestStatus);
            var actual = _customerRequestStatusService.GetAll().ToList();
            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(customerRequestStatus, actual[0], "Actual result was not as expected");
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void CustomerRequestStatusService_Can_Delete_CustomerRequestStatus()
        {
            //Arrange
            var DeletedID = 1;
            _unitOfWork.Setup(m => m.CustomerRequestStatuses.Delete(DeletedID));

            // Act
            _customerRequestStatusService.Delete(DeletedID);
            // Assert
            _unitOfWork.Verify(v => v.CustomerRequestStatuses.Delete(DeletedID), Times.Once());
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void CustomerRequestService_Can_Create_Room()
        {
            //Arrange
            var customerRequestStatus = new CustomerRequestStatus
            {
                Name = "NewRequest"
            };

            _unitOfWork.Setup(m => m.CustomerRequestStatuses.Create(customerRequestStatus));
            // Act
            _customerRequestStatusService.Create(customerRequestStatus);
            // Assert
            _unitOfWork.Verify(v => v.CustomerRequestStatuses.Create(customerRequestStatus), Times.Once());
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }
    }
}