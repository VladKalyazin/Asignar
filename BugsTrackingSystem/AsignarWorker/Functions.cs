using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Azure.WebJobs;
using AsignarDBEntities;
using AsignarServices.AzureStorage;
using BugsTrackingSystem.Models;
using System.Net.Mail;
using System.Web.Script.Serialization;
using SendGrid;

namespace AsignarWorker
{
    public class Functions
    {
        private const string _senderEmail = "asignar@gmail.com";
        private const string _senderName = "Vlad Kalyazin";

        private const string _userQueueName = "usersqueue";
        private const string _refreshLinkTimerInterval = "24:00:00";

        public static void UserAdded([QueueTrigger(_userQueueName) ] UserQueueModel user, TextWriter log)
        {
            string templateId = CloudConfigurationManager.GetSetting("AsignarSendGridTemplateId");
            string apiKey = CloudConfigurationManager.GetSetting("AsignarSendGridKey");
            var transportWeb = new Web(apiKey);

            var myMessage = new SendGridMessage();
            myMessage.AddTo(user.Email);
            myMessage.From = new MailAddress(_senderEmail, _senderName);
            myMessage.Subject = "Asignar registration";
            myMessage.AddSubstitution("%name%", new List<string> { user.FirstName + " " + user.Surname });
            myMessage.EnableTemplateEngine(templateId);
            myMessage.Text = "Congratulations!";
            myMessage.Html = "Congratulations!";
            transportWeb.DeliverAsync(myMessage).Wait();

            log.WriteLine($"Email to {user.Email} was successfully sent.");
        }

        public static void RefreshDatabasePhotoLinks([TimerTrigger(_refreshLinkTimerInterval, RunOnStartup = true)] TimerInfo timer, TextWriter log)
        {
            using (var databaseModel = new AsignarDatabaseModel())
            {
                using (var dbContextTransaction = databaseModel.Database.BeginTransaction())
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

                    dbContextTransaction.Commit();

                    log.WriteLine("Links to photos was successfully refreshed in SQL database.");
                }
            }
        }

        public static void RefreshAttachmentsLinks([TimerTrigger(_refreshLinkTimerInterval, RunOnStartup = true)] TimerInfo timer, TextWriter log)
        {
            using (var databaseModel = new AsignarDatabaseModel())
            {
                using (var dbContextTransaction = databaseModel.Database.BeginTransaction())
                {
                    var blobHelper = new BlobStorageHelper();

                    var attachments = blobHelper.GetDefectPriorityPhotos();
                    foreach (var attachment in attachments)
                    {
                        databaseModel.DefectAttachments.Where((d) => d.AttachmentID == attachment.Key).Single().Link = attachment.Value;
                    }

                    databaseModel.SaveChanges();

                    dbContextTransaction.Commit();
                }
            }

            log.WriteLine("Links to attachments was successfully refreshed in SQL database.");
        }
    }
}
