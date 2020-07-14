using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagementSystem.DAL.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelManagementSystem.DAL.Context
{
    public class HotelContextInitializer : DropCreateDatabaseAlways<HotelDbContext>
    {
        protected override void Seed(HotelDbContext context)
        {
            var userManager = new UserManager<HotelUserEntity>(new UserStore<HotelUserEntity>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //create a new role Administrator
            var newRoleAdmin = new IdentityRole("administrator");
            var roleCreateResult = roleManager.Create(newRoleAdmin);

            if (!roleCreateResult.Succeeded) throw new Exception(string.Join("; ", roleCreateResult.Errors));
            //create a new role Manager
            var newRoleManager = new IdentityRole("manager");
            roleCreateResult = roleManager.Create(newRoleManager);

            if (!roleCreateResult.Succeeded) throw new Exception(string.Join("; ", roleCreateResult.Errors));
            //create a new role Client
            var newRoleClient = new IdentityRole("client");
            roleCreateResult = roleManager.Create(newRoleClient);
            if (!roleCreateResult.Succeeded) throw new Exception(string.Join("; ", roleCreateResult.Errors));
            //create a new user as a admin
            var admin = new HotelUserEntity {Email = "admin@mail.com", UserName = "admin@mail.com"};
            var password = "123456789";
            var resultAdmin = userManager.Create(admin, password);
            //create a new user as a manager
            var manager = new HotelUserEntity {Email = "manager@mail.com", UserName = "manager@mail.com"};
            password = "123456789";
            var resultManager = userManager.Create(manager, password);
            //create a new user as a user
            var user = new HotelUserEntity
            {
                Email = "user@mail.com",
                UserName = "user@mail.com",
                FirstName = "User",
                LastName = "User",
                PhoneNumber = "380998654312"
            };
            password = "123456789";
            var resultUser = userManager.Create(user, password);

            // if user creation was successful
            if (resultAdmin.Succeeded)
                // add a role for the user
                userManager.AddToRole(admin.Id, newRoleAdmin.Name);
            // if user creation was successful
            if (resultManager.Succeeded)
                // add a role for the user
                userManager.AddToRole(manager.Id, newRoleManager.Name);
            // if user creation was successful
            if (resultUser.Succeeded)
                // add a role for the user
                userManager.AddToRole(user.Id, newRoleClient.Name);

            var roomTypes = new List<RoomTypeEntity>
            {
                new RoomTypeEntity
                {
                    Id = 1,
                    Name = "All"
                },
                new RoomTypeEntity
                {
                    Id = 2,
                    Name = "Standart"
                },
                new RoomTypeEntity
                {
                    Id = 3,
                    Name = "Superior"
                },
                new RoomTypeEntity
                {
                    Id = 4,
                    Name = "Studio"
                },
                new RoomTypeEntity
                {
                    Id = 5,
                    Name = "Business Room"
                },
                new RoomTypeEntity
                {
                    Id = 6,
                    Name = "Apartment"
                },
                new RoomTypeEntity
                {
                    Id = 7,
                    Name = "President Suites"
                }
            };
            roomTypes.ForEach(std => context.RoomTypes.Add(std));
            context.SaveChanges();

            var customerRequestStatusEntities = new List<CustomerRequestStatusEntity>
            {
                new CustomerRequestStatusEntity
                {
                    Id = 1,
                    Name = "New request"
                },
                new CustomerRequestStatusEntity
                {
                    Id = 2,
                    Name = "Answer received"
                }
            };
            customerRequestStatusEntities.ForEach(std => context.CustomerRequestStatuses.Add(std));
            context.SaveChanges();

            var customerRequestEntities = new List<CustomerRequestEntity>
            {
                new CustomerRequestEntity
                {
                    Id = 1,
                    DateFrom = new DateTime(2020, 11, 19),
                    DateTo = new DateTime(2021, 12, 10),
                    Sleeps = 2,
                    RoomType = roomTypes.ElementAt(0),
                    HotelUser = user,
                    HotelUserId = user.Id,
                    CustomerRequestStatus = customerRequestStatusEntities.ElementAt(0)
                },
                new CustomerRequestEntity
                {
                    Id = 2,
                    DateFrom = new DateTime(2020, 11, 19),
                    DateTo = new DateTime(2021, 12, 10),
                    Sleeps = 3,
                    RoomType = roomTypes.ElementAt(1),
                    HotelUser = user,
                    HotelUserId = user.Id,
                    CustomerRequestStatus = customerRequestStatusEntities.ElementAt(0)
                }
            };
            customerRequestEntities.ForEach(std => context.CustomerRequests.Add(std));
            context.SaveChanges();
            var bookingStatusEntities = new List<RoomStatusEntity>
            {
                new RoomStatusEntity
                {
                    Id = 1,
                    Name = "All"
                },
                new RoomStatusEntity
                {
                    Id = 2,
                    Name = "Booked"
                },
                new RoomStatusEntity
                {
                    Id = 3,
                    Name = "Free"
                },
                new RoomStatusEntity
                {
                    Id = 4,
                    Name = "Booked up"
                },
                new RoomStatusEntity
                {
                    Id = 5,
                    Name = "Unavailable"
                }
            };
            bookingStatusEntities.ForEach(std => context.RoomStatuses.Add(std));
            context.SaveChanges();
            var rooms = new List<RoomEntity>
            {
                new RoomEntity
                {
                    RoomNumber = "1",
                    RoomDescription = "Room have front sea view",
                    RoomPriceForOneNight = 1000,
                    Sleeps = 2,
                    RoomImage = "~/Content/Image/room1.jpg",
                    RoomType = roomTypes.ElementAt(0),
                    RoomStatus = bookingStatusEntities.ElementAt(2)
                },
                new RoomEntity
                {
                    RoomNumber = "2",
                    RoomDescription = "Room have side sea view",
                    RoomPriceForOneNight = 1500,
                    RoomImage = "~/Content/Image/room2.jpg",
                    Sleeps = 1,
                    RoomType = roomTypes.ElementAt(1),
                    RoomStatus = bookingStatusEntities.ElementAt(2)
                },
                new RoomEntity
                {
                    RoomNumber = "3",
                    RoomDescription = "Room with a view of the pool",
                    RoomPriceForOneNight = 2000,
                    RoomImage = "~/Content/Image/room3.jpg",
                    Sleeps = 1,
                    RoomType = roomTypes.ElementAt(1),
                    RoomStatus = bookingStatusEntities.ElementAt(2)
                },
                new RoomEntity
                {
                    RoomNumber = "4",
                    RoomDescription = "Room with a view of the park",
                    RoomPriceForOneNight = 5000,
                    RoomImage = "~/Content/Image/room4.jpg",
                    Sleeps = 3,
                    RoomType = roomTypes.ElementAt(1),
                    RoomStatus = bookingStatusEntities.ElementAt(2)
                },
                new RoomEntity
                {
                    RoomNumber = "5",
                    RoomDescription = "Room with a view of the park",
                    RoomPriceForOneNight = 6000,
                    RoomImage = "~/Content/Image/room5.jpg",
                    Sleeps = 4,
                    RoomType = roomTypes.ElementAt(3),
                    RoomStatus = bookingStatusEntities.ElementAt(2)
                },
                new RoomEntity
                {
                    RoomNumber = "6",
                    RoomDescription = "Room with a view of the park",
                    RoomPriceForOneNight = 8000,
                    RoomImage = "~/Content/Image/room6.jpg",
                    Sleeps = 4,
                    RoomType = roomTypes.ElementAt(5),
                    RoomStatus = bookingStatusEntities.ElementAt(1)
                }
            };
            rooms.ForEach(std => context.Rooms.Add(std));
            context.SaveChanges();
        }
    }
}