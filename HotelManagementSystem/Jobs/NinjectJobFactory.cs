using System;
using System.Data.Entity.Core;
using HotelManagementSystem.Utilities;
using Ninject;
using Ninject.Syntax;
using Quartz;
using Quartz.Spi;

namespace HotelManagementSystem.Jobs
{
    public class NinjectJobFactory : IJobFactory
    {
        private readonly IResolutionRoot _resolutionRoot;

        public NinjectJobFactory(IResolutionRoot resolutionRoot)
        {
            _resolutionRoot = resolutionRoot;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            Logger.InitLogger();
            try
            {
                Logger.Log.Debug(
                    $"Producing instance of Job '{bundle.JobDetail.Key}', class={bundle.JobDetail.JobType.FullName}");
                // this will inject dependencies that the job requires
                return (IJob) _resolutionRoot.Get(bundle.JobDetail.JobType);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error trying update room status", ex);
                throw new UpdateException();
            }
        }

        public void ReturnJob(IJob job)
        {
            _resolutionRoot.Release(job);
        }
    }
}