using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SPSamples;

namespace TimerJob.Features.TimerJobFeature
{


    [Guid("57c56875-f9ce-4b5a-8c26-9786ff3f1627")]
    public class TimerJobFeatureEventReceiver : SPFeatureReceiver
    {
        const string List_JOB_NAME = "ListLogger";
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPSite site = properties.Feature.Parent as SPSite;

            // make sure the job isn't already registered

            foreach (SPJobDefinition job in site.WebApplication.JobDefinitions)
            {

                if (job.Name == List_JOB_NAME)

                    job.Delete();

            }

            // install the job

            ListTimerJob listLoggerJob = new ListTimerJob(List_JOB_NAME, site.WebApplication);

            SPMinuteSchedule schedule = new SPMinuteSchedule {BeginSecond = 0, EndSecond = 59, Interval = 5};

            listLoggerJob.Schedule = schedule;

            listLoggerJob.Update();

        }

        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPSite site = properties.Feature.Parent as SPSite;

            // delete the job

            foreach (SPJobDefinition job in site.WebApplication.JobDefinitions)
            {

                if (job.Name == List_JOB_NAME)

                    job.Delete();

            }

        }

    }
}
