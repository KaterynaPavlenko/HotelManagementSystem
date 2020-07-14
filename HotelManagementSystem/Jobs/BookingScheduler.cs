using System;
using HotelManagementSystem.BLL.Infrastructure;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;
using HotelManagementSystem.BLL.Services;
using HotelManagementSystem.DAL.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using Ninject.Modules;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace HotelManagementSystem.Jobs
{
    public class BookingScheduler
    {
        public static async void Start(DateTime dateStart, int bookingId)
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            var services = new ServiceCollection();
            services.AddTransient<IJobFactory, NinjectJobFactory>();
            services.AddScoped<IJob, RemoveBookingJob>();
            services.AddScoped<IBookingService, BookingService>();


            NinjectModule serviceModule = new ServiceModule("DefaultConnection");
            NinjectModule roomModule = new RepositoryModule("DefaultConnection");
            var kernel = new StandardKernel(serviceModule, roomModule);

            var container = services.BuildServiceProvider();
            var jobFactory = new NinjectJobFactory(kernel);
            scheduler.JobFactory = jobFactory;
            await scheduler.Start();


            var job = JobBuilder.Create<RemoveBookingJob>().Build();
            var triggerKey = new TriggerKey(bookingId.ToString(), dateStart.ToShortDateString() + bookingId);
            var trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .StartAt(dateStart.AddMinutes(1))
                .UsingJobData("bookingId", bookingId)
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}