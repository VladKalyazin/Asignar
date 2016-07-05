using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using AsignarDBEntities;
using AsignarServices.AzureStorage;

namespace ScheduleWorker
{
    class Program
    {

        static void Main(string[] args)
        {
            JobHost host = new JobHost();
            host.Call(typeof(Program).GetMethod("RefreshDatabaseLinks"));
        }

        [NoAutomaticTrigger]
        public static void RefreshDatabaseLinks()
        {
            using (var databaseModel = new AsignarDatabaseModel())
            {
                var blobHelper = new BlobStorageHelper();

                var priorityPhotos = blobHelper.GetDefectPriorityPhotos();
                foreach (var photo in priorityPhotos)
                {
                    databaseModel.DefectPriorities.Where((dp) => dp.DefectPriorityID == photo.Key).Single().PhotoLink = photo.Value;
                }

                var userPhotos = blobHelper.GetUserPhotos();
                foreach (var photo in userPhotos)
                {
                    databaseModel.Users.Where((u) => u.UserID == photo.Key).Single().PhotoLink = photo.Value;
                }

                databaseModel.SaveChanges();
            }
        }
    }
}
