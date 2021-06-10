using Ray.Notification.Client;
using Ray.Notification.Common.Convertors;
using Ray.Notification.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ray.Notification.Console
{
    public class Processor
    {
        public NotificationHubConnection HubConnection { get; set; }

        public void Start()
        {
            const int sleep = 3500;

            const string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjA2RDNFNDZFOTEwNzNDNUQ0QkMyQzk5ODNCRTlGRjQ0OENGNjQwRDQiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJCdFBrYnBFSFBGMUx3c21ZTy1uX1JJejJRTlEifQ.eyJuYmYiOjE2MTgyMjI0ODEsImV4cCI6MTYxODI1NTI4MSwiaXNzIjoiaHR0cDovL29mZmljZS5ucmF5dmFyei5pci9pZHBfc3NvIiwiYXVkIjpbImh0dHA6Ly9vZmZpY2UubnJheXZhcnouaXIvaWRwX3Nzby9yZXNvdXJjZXMiLCJJRFBDbGllbnRzIl0sImNsaWVudF9pZCI6IklEUENsaWVudHMiLCJzdWIiOiJJRFAuRG9tYWluLktleU9iamVjdCIsImF1dGhfdGltZSI6MTYxODIyMjQ4MSwiaWRwIjoibG9jYWwiLCJVc2VyTmFtZSI6InNtbSIsIkVtYWlsIjoiYWxpcmF5dmFyekBnbWFpbC5jb20iLCJBZG1pblVzZXIiOiIiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiSURQQ2xpZW50cyIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJjdXN0b20iXX0.rggk-OdFpPt6vl_1nVDwLGzuFUCOS6EcWN552fuFHeOi1J8E1-DhhWSX4-M2cCap-roG4-Zbq1FpmjXzv5O8grUgc8c_U__A0iz-2yPSd0nvIVy223e7QZ9qnMNM2LBTftHQJ1lTW_-iCq3P6SPSA8ZwCkveDFgEGejqlYDAv6QTo2XTPhaIxOsHRxYblyeV_5zufGJimInEWGOvvsWaer6d0HbmSq-LAxxAV1ozihsfJ4tAOhAxArfIYTVlJtceamNR0Ey96HfaaK0WtgWjnMMAnUmgFyPMV9_-76VMpPZFJ-3kRdIlp1NIKlqVHANsoFx6_zTMR9abAXpad5h8Qw";
            HubConnection = NotificationHubConnectionBuilder.CreateConnection("http://localhost:11111", token);

            Thread.Sleep(sleep);

            var notification = new Domain.Notification
            {
                Header = "آزمایش صحت ارسال اعلان",
                FromPrincipalTitle = "کارشناس اتوماسیون",
                Date = DateTime.Now.AddHours(-5).GetIntegerPersianDate(),
                Time = DateTime.Now.AddHours(-5).ConvertDateToPersianTime(),
                FolderId = Guid.NewGuid(),
                Title = "دریافتی",
                ToPrincipalId = "کارشناس اتوماسیون",
                FolderType = FolderType.AnnouncementFolder,
                DocType = DocType.Announcement
            };

            Task.WaitAll(HubConnection.SendAnnouncementNotifications(notification));

            Thread.Sleep(sleep);

            notification = new Domain.Notification
            {
                Header = "آزمایش صحت ارسال اعلان",
                FromPrincipalTitle = "کارشناس اتوماسیون",
                Date = DateTime.Now.AddHours(-5).GetIntegerPersianDate(),
                Time = DateTime.Now.AddHours(-5).ConvertDateToPersianTime(),
                FolderId = Guid.NewGuid(),
                Title = "دریافتی",
                ToPrincipalId = "کارشناس اتوماسیون",
                FolderType = FolderType.DraftFolder,
                DocType = DocType.Draft
            };

            Task.WaitAll(HubConnection.SendDraftNotifications(notification));

            Thread.Sleep(sleep);

            notification = new Domain.Notification
            {
                Header = "آزمایش صحت ارسال اعلان",
                FromPrincipalTitle = "کارشناس اتوماسیون",
                Date = DateTime.Now.AddHours(-5).GetIntegerPersianDate(),
                Time = DateTime.Now.AddHours(-5).ConvertDateToPersianTime(),
                FolderId = Guid.NewGuid(),
                Title = "دریافتی",
                ToPrincipalId = "کارشناس اتوماسیون",
                FolderType = FolderType.PersonalLetters,
                DocType = DocType.Letter
            };

            Task.WaitAll(HubConnection.SendLetterNotifications(notification));

            Thread.Sleep(sleep);

            notification = new Domain.Notification
            {
                Header = "آزمایش صحت ارسال اعلان",
                FromPrincipalTitle = "کارشناس اتوماسیون",
                Date = DateTime.Now.AddHours(-5).GetIntegerPersianDate(),
                Time = DateTime.Now.AddHours(-5).ConvertDateToPersianTime(),
                FolderId = Guid.NewGuid(),
                Title = "دریافتی",
                ToPrincipalId = "کارشناس اتوماسیون",
                FolderType = FolderType.PersonalLetters,
                DocType = DocType.Message
            };

            Task.WaitAll(HubConnection.SendMessageNotifications(notification));

            Thread.Sleep(sleep);

            notification = new Domain.Notification
            {
                Header = "آزمایش صحت ارسال اعلان",
                FromPrincipalTitle = "کارشناس اتوماسیون",
                Date = DateTime.Now.AddHours(-5).GetIntegerPersianDate(),
                Time = DateTime.Now.AddHours(-5).ConvertDateToPersianTime(),
                FolderId = Guid.NewGuid(),
                Title = "دریافتی",
                ToPrincipalId = "کارشناس اتوماسیون",
                FolderType = FolderType.DeletedItemsFolder,
                DocType = DocType.Letter
            };

            Task.WaitAll(HubConnection.DeleteLetterInstanceNotifications(notification));

            Thread.Sleep(sleep);

            HubConnection.Dispose();
        }
    }
}