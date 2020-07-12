using System;
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
    public class BookingServiceTest
    {
        private Mock<IRepository<Booking>> _bookingRepository;
        private IBookingService _bookingService;
        private Mock<IUnitOfWork> _unitOfWork;
        private List<Booking> bookings;

        [TestInitialize]
        public void SetUp()
        {
            bookings = new List<Booking>
            {
                new Booking
                {
                    Id = 0,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now,
                    RoomId = 0,
                    HotelUserId = "0",
                    TotalPrice = 100,
                    Payment = false
                },
                new Booking
                {
                    Id = 1,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now,
                    RoomId = 1,
                    HotelUserId = "1",
                    TotalPrice = 1000,
                    Payment = false
                },
                new Booking
                {
                    Id = 2,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now,
                    RoomId = 2,
                    HotelUserId = "2",
                    TotalPrice = 2000,
                    Payment = false
                },
                new Booking
                {
                    Id = 3,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now,
                    RoomId = 3,
                    HotelUserId = "3",
                    TotalPrice = 3000,
                    Payment = false
                }
            };
            // Create a new mock of the repository
            _bookingRepository = new Mock<IRepository<Booking>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            // Set up the mock for the repository
            _unitOfWork.Setup(x => x.Bookings.GetAll()).Returns(bookings);
            _bookingRepository.Setup(x => x.GetAll())
                .Returns(bookings);
            // Create the service and inject the repository into the service
            _bookingService = new BookingService(_unitOfWork.Object, new ModelStateDictionary());
        }

        [TestMethod]
        public void BookingService_Get_All_Room()
        {
            // Act
            var bookingActual = _bookingService.GetAll();

            // Assert
            Assert.AreEqual(4, bookingActual.Count(), "The room count is not correct");
        }

        [TestMethod]
        public void BookingService_Can_GetById_Valid_Room()
        {
            //Arrange
            var booking = new Booking
            {
                Id = 1,
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
                RoomId = 1,
                HotelUserId = "1",
                TotalPrice = 2000,
                Payment = false
            };

            _unitOfWork.Setup(m => m.Bookings.GetById(booking.Id)).Returns(booking);
            // Act
            var actual = _bookingService.GetById(booking.Id);

            //Assert
            _unitOfWork.Verify(); //verify that GetByID was called based on setup.
            Assert.IsNotNull(actual); //assert that a result was returned
            Assert.AreEqual(booking, actual); //assert that actual result was as expected
        }

        [TestMethod]
        public void BookingService_Can_Update_Room()
        {
            //Arrange
            var booking = bookings.ElementAt(0);
            booking.TotalPrice = 5;
            // Act

            _bookingService.Update(booking);
            var actual = _bookingService.GetAll().ToList();
            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(booking, actual[0], "Actual result was not as expected");
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void BookingService_Can_Delete_Room()
        {
            //Arrange
            var DeletedID = 1;
            _unitOfWork.Setup(m => m.Bookings.Delete(DeletedID));

            // Act
            _bookingService.Delete(DeletedID);
            // Assert
            _unitOfWork.Verify(v => v.Bookings.Delete(DeletedID), Times.Once());
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void BookingService_Can_Create_Room()
        {
            //Arrange
            var booking = new Booking
            {
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
                RoomId = 1,
                HotelUserId = "1",
                TotalPrice = 2000,
                Payment = false
            };
            _unitOfWork.Setup(m => m.Bookings.Create(booking));
            // Act
            _bookingService.Create(booking);
            // Assert
            _unitOfWork.Verify(v => v.Bookings.Create(booking), Times.Once());
            _unitOfWork.Verify(x => x.Save(), Times.Once());
        }
    }
}