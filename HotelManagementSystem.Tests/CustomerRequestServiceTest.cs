using System;
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
    public class CustomerRequestServiceTest
    {
        private Mock<IRepository<CustomerRequest>> _customerRequestRepository;
        private ICustomerRequestServices _customerRequestService;
        private Mock<IUnitOfWork> _unitOfWork;
        private List<CustomerRequest> customerRequests;

        [TestInitialize]
        public void SetUp()
        {
            customerRequests = new List<CustomerRequest>
            {
                new CustomerRequest
                {
                    Id = 0,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now,
                    Sleeps = 1,
                    RoomTypeId = 1,
                    CustomerRequestStatusId = 1,
                    HotelUserId = "0"
                },
                new CustomerRequest
                {
                    Id = 1,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now,
                    Sleeps = 1,
                    RoomTypeId = 1,
                    CustomerRequestStatusId = 1,
                    HotelUserId = "1"
                },
                new CustomerRequest
                {
                    Id = 2,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now,
                    Sleeps = 3,
                    RoomTypeId = 3,
                    CustomerRequestStatusId = 3,
                    HotelUserId = "3"
                },
                new CustomerRequest
                {
                    Id = 3,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now,
                    Sleeps = 3,
                    RoomTypeId = 3,
                    CustomerRequestStatusId = 3,
                    HotelUserId = "3"
                }
            };
            // Create a new mock of the repository
            _customerRequestRepository = new Mock<IRepository<CustomerRequest>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            // Set up the mock for the repository
            _unitOfWork.Setup(x => x.CustomerRequests.GetAll()).Returns(customerRequests);
            _customerRequestRepository.Setup(x => x.GetAll())
                .Returns(customerRequests);
            // Create the service and inject the repository into the service
            _customerRequestService = new CustomerRequestService(_unitOfWork.Object);
        }

        [TestMethod]
        public void CustomerRequestService_Get_All_CustomerRequest()
        {
            // Act
            var customerRequestActual = _customerRequestService.GetAll();

            // Assert
            Assert.AreEqual(4, customerRequestActual.Count(), "The room count is not correct");
        }

        [TestMethod]
        public void CustomerRequestService_Can_GetById_Valid_CustomerRequest()
        {
            //Arrange
            var customerRequest = new CustomerRequest
            {
                Id = 1,
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
                HotelUserId = "1"
            };


            _unitOfWork.Setup(m => m.CustomerRequests.GetById(customerRequest.Id)).Returns(customerRequest);
            // Act
            var actual = _customerRequestService.GetById(customerRequest.Id);

            //Assert
            _unitOfWork.Verify(); //verify that GetByID was called based on setup.
            Assert.IsNotNull(actual); //assert that a result was returned
            Assert.AreEqual(customerRequest, actual); //assert that actual result was as expected
        }

        [TestMethod]
        public void CustomerRequestService_Can_Update_CustomerRequest()
        {
            //Arrange
            var customerRequest = customerRequests.ElementAt(0);
            customerRequest.Sleeps = 5;
            // Act

            _customerRequestService.Update(customerRequest);
            var actual = _customerRequestService.GetAll().ToList();
            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(customerRequest, actual[0], "Actual result was not as expected");
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void CustomerRequestService_Can_Delete_CustomerRequest()
        {
            //Arrange
            var DeletedID = 1;

            _unitOfWork.Setup(m => m.Confirmations.Delete(DeletedID));

            // Act
            _customerRequestService.Delete(DeletedID);
            // Assert
            _unitOfWork.Verify(v => v.CustomerRequests.Delete(DeletedID), Times.Once());
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void CustomerRequestService_Can_Create_Room()
        {
            //Arrange
            var customerRequest = new CustomerRequest
            {
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
                HotelUserId = "1"
            };
            _unitOfWork.Setup(m => m.CustomerRequests.Create(customerRequest));
            // Act
            _customerRequestService.Create(customerRequest);
            // Assert
            _unitOfWork.Verify(v => v.CustomerRequests.Create(customerRequest), Times.Once());
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }
    }
}