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
    public class RoomServiceTest
    {
        private Mock<IRepository<Room>> _roomRepository;
        private List<Room> _rooms;
        private IRoomService _roomService;
        private Mock<IUnitOfWork> _unitOfWork;

        [TestInitialize]
        public void SetUp()
        {
            _rooms = new List<Room>
            {
                new Room
                {
                    Id = 1, RoomStatusId = 1, RoomDescription = "Room", RoomTypeId = 1, RoomNumber = "1",
                    RoomPriceForOneNight = 100, RoomImage = "image"
                },
                new Room
                {
                    Id = 2, RoomStatusId = 1, RoomDescription = "Room", RoomTypeId = 1, RoomNumber = "2",
                    RoomPriceForOneNight = 100, RoomImage = "image"
                },
                new Room
                {
                    Id = 3, RoomStatusId = 1, RoomDescription = "Room", RoomTypeId = 1, RoomNumber = "3",
                    RoomPriceForOneNight = 100, RoomImage = "image"
                },
                new Room
                {
                    Id = 4, RoomStatusId = 1, RoomDescription = "Room", RoomTypeId = 1, RoomNumber = "4",
                    RoomPriceForOneNight = 100, RoomImage = "image"
                }
            };
            // Create a new mock of the repository
            _roomRepository = new Mock<IRepository<Room>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            // Set up the mock for the repository
            _unitOfWork.Setup(x => x.Rooms.GetAll()).Returns(_rooms);
            _roomRepository.Setup(x => x.GetAll())
                .Returns(_rooms);
            // Create the service and inject the repository into the service
            _roomService = new RoomService(_unitOfWork.Object, new ModelStateDictionary());
        }

        [TestMethod]
        public void RoomService_Get_All_Room()
        {
            // Act
            var roomsActual = _roomService.GetAll();

            // Assert
            Assert.AreEqual(4, roomsActual.Count(), "The room count is not correct");
        }

        [TestMethod]
        public void RoomService_Can_GetById_Valid_Room()
        {
            //Arrange
            var room = new Room
            {
                Id = 3,
                RoomStatusId = 1,
                RoomDescription = "Room",
                RoomTypeId = 1,
                RoomNumber = "3",
                RoomPriceForOneNight = 100,
                RoomImage = "image"
            };


            _unitOfWork.Setup(m => m.Rooms.GetById(room.Id)).Returns(room);
            // Act
            var actual = _roomService.GetById(room.Id);

            //Assert
            _unitOfWork.Verify(); //verify that GetByID was called based on setup.
            Assert.IsNotNull(actual); //assert that a result was returned
            Assert.AreEqual(room, actual); //assert that actual result was as expected
        }

        [TestMethod]
        public void RoomService_Can_Update_Room()
        {
            //Arrange
            var room = _rooms.ElementAt(0);
            room.RoomDescription = "NewRoom";
            _roomRepository.Setup(x => x.Update(room));
            // Act

            _roomService.Update(room);
            var actual = _roomRepository.Object.GetAll().ToList();
            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(room, actual[0], "Actual result was not as expected");
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void RoomService_Can_Delete_Room()
        {
            //Arrange
            var DeletedID = 1;
            _unitOfWork.Setup(m => m.Rooms.Delete(DeletedID));

            // Act
            _roomService.Delete(DeletedID);
            // Assert
            _unitOfWork.Verify(v => v.Rooms.Delete(DeletedID), Times.Once());
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void RoomService_Can_Create_Room()
        {
            //Arrange
            var room = new Room
            {
                RoomDescription = "CreateRoom",
                RoomTypeId = 1,
                RoomNumber = "3",
                RoomPriceForOneNight = 100,
                RoomImage = "image"
            };
            _unitOfWork.Setup(m => m.Rooms.Create(room));
            // Act
            _roomService.Create(room);
            // Assert
            _unitOfWork.Verify(v => v.Rooms.Create(room), Times.Once());
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }
    }
}