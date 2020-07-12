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
    public class RoomTypeServiceTest
    {
        private Mock<IRepository<RoomType>> _roomTypeRepository;
        private IRoomTypeServices _roomTypeServices;
        private Mock<IUnitOfWork> _unitOfWork;
        private List<RoomType> roomTypes;

        [TestInitialize]
        public void SetUp()
        {
            roomTypes = new List<RoomType>
            {
                new RoomType
                {
                    Id = 0,
                    Name = "0"
                },
                new RoomType
                {
                    Id = 1,
                    Name = "1"
                },
                new RoomType
                {
                    Id = 2,
                    Name = "2"
                },
                new RoomType
                {
                    Id = 3,
                    Name = "3"
                }
            };
            // Create a new mock of the repository
            _roomTypeRepository = new Mock<IRepository<RoomType>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            // Set up the mock for the repository
            _unitOfWork.Setup(x => x.RoomTypes.GetAll()).Returns(roomTypes);
            _roomTypeRepository.Setup(x => x.GetAll())
                .Returns(roomTypes);
            // Create the service and inject the repository into the service
            _roomTypeServices = new RoomTypeService(_unitOfWork.Object, new ModelStateDictionary());
        }

        [TestMethod]
        public void RoomTypeService_Get_All_RoomType()
        {
            // Act
            var actual = _roomTypeServices.GetAll();

            // Assert
            Assert.AreEqual(4, actual.Count(), "The room count is not correct");
        }

        [TestMethod]
        public void RoomTypeService_Can_GetById_Valid_RoomType()
        {
            //Arrange
            var roomType = new RoomType
            {
                Id = 1,
                Name = "4"
            };


            _unitOfWork.Setup(m => m.RoomTypes.GetById(roomType.Id)).Returns(roomType);
            // Act
            var actual = _roomTypeServices.GetById(roomType.Id);

            //Assert
            _unitOfWork.Verify(); //verify that GetByID was called based on setup.
            Assert.IsNotNull(actual); //assert that a result was returned
            Assert.AreEqual(roomType, actual); //assert that actual result was as expected
        }

        [TestMethod]
        public void RoomTypeService_Can_Update_RoomType()
        {
            //Arrange
            var roomType = roomTypes.ElementAt(0);
            roomType.Name = "5";
            // Act

            _roomTypeServices.Update(roomType);
            var actual = _roomTypeRepository.Object.GetAll().ToList();
            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(roomType, actual[0], "Actual result was not as expected");
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }


        [TestMethod]
        public void RoomTypeService_Can_Delete_RoomType()
        {
            //Arrange
            var DeletedID = 1;

            // Act
            _roomTypeServices.Delete(DeletedID);

            // Assert
            _unitOfWork.Verify(v => v.RoomTypes.Delete(DeletedID), Times.Once());
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void RoomTypeService_Can_Create_Room()
        {
            //Arrange
            var roomType = new RoomType
            {
                Name = "NewRoomType"
            };

            _unitOfWork.Setup(m => m.RoomTypes.Create(roomType));
            // Act
            _roomTypeServices.Create(roomType);
            // Assert
            _unitOfWork.Verify(v => v.RoomTypes.Create(roomType), Times.Once());
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }
    }
}