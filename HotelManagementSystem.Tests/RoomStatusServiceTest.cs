using System.Collections.Generic;
using System.Linq;
using System.Web.ModelBinding;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;
using HotelManagementSystem.BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HotelManagementSystem.Tests
{
    [TestClass]
    public class RoomStatusServiceTest
    {
        private Mock<IRepository<RoomStatus>> _roomRepository;
        private IRoomStatusService _roomStatusService;
        private Mock<IUnitOfWork> _unitOfWork;
        private List<RoomStatus> roomStatus;

        [TestInitialize]
        public void SetUp()
        {
            roomStatus = new List<RoomStatus>
            {
                new RoomStatus
                {
                    Id = 0,
                    Name = "0"
                },
                new RoomStatus
                {
                    Id = 1,
                    Name = "1"
                },
                new RoomStatus
                {
                    Id = 2,
                    Name = "2"
                },
                new RoomStatus
                {
                    Id = 3,
                    Name = "3"
                }
            };
            // Create a new mock of the repository
            _roomRepository = new Mock<IRepository<RoomStatus>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            // Set up the mock for the repository
            _unitOfWork.Setup(x => x.RoomStatuses.GetAll()).Returns(roomStatus);
            _roomRepository.Setup(x => x.GetAll())
                .Returns(roomStatus);
            // Create the service and inject the repository into the service
            _roomStatusService = new RoomStatusService(_unitOfWork.Object, new ModelStateDictionary());
        }

        [TestMethod]
        public void RoomStatusService_Get_All_RoomStatus()
        {
            // Act
            var roomsActual = _roomStatusService.GetAll();

            // Assert
            Assert.AreEqual(4, roomsActual.Count(), "The room count is not correct");
        }

        [TestMethod]
        public void RoomStatusService_Can_GetById_Valid_RoomStatus()
        {
            //Arrange
            var roomStatus = new RoomStatus
            {
                Id = 1,
                Name = "4"
            };


            _unitOfWork.Setup(m => m.RoomStatuses.GetById(roomStatus.Id)).Returns(roomStatus);
            // Act
            var actual = _roomStatusService.GetById(roomStatus.Id);

            //Assert
            _unitOfWork.Verify(); //verify that GetByID was called based on setup.
            Assert.IsNotNull(actual); //assert that a result was returned
            Assert.AreEqual(roomStatus, actual); //assert that actual result was as expected
        }

        [TestMethod]
        public void RoomStatusService_Can_Update_RoomStatus()
        {
            //Arrange
            var roomStatus = this.roomStatus.ElementAt(0);
            roomStatus.Name = "5";

            // Act

            _roomStatusService.Update(roomStatus);
            var actual = _roomRepository.Object.GetAll().ToList();
            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(roomStatus, actual[0], "Actual result was not as expected");
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void RoomStatusService_Can_Delete_RoomStatus()
        {
            //Arrange
            var DeletedID = 1;
            _unitOfWork.Setup(m => m.RoomStatuses.Delete(DeletedID));

            // Act
            _roomStatusService.Delete(DeletedID);
            // Assert
            _unitOfWork.Verify(v => v.RoomStatuses.Delete(DeletedID), Times.Once());
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void RoomStatusService_Can_Create_Room()
        {
            //Arrange
            var roomStatus = new RoomStatus
            {
                Name = "NewStatus"
            };
            _unitOfWork.Setup(m => m.RoomStatuses.Create(roomStatus));
            // Act
            _roomStatusService.Create(roomStatus);
            // Assert
            _unitOfWork.Verify(v => v.RoomStatuses.Create(roomStatus), Times.Once());
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }
    }
}