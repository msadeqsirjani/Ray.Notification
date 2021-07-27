using Newtonsoft.Json;
using Ray.Notification.Common.Convertors;
using System;

namespace Ray.Notification.Domain
{
    public class Notification
    {
        private string _title;
        private string _fromPrincipalTitle;
        private string _header;

        [JsonProperty("folderId")]
        public Guid FolderId { get; set; }

        [JsonProperty("title")]
        public string Title
        {
            get => _title.FixPersianCharacter();
            set => _title = value;
        }

        [JsonProperty("folderOwnerId")]
        public string FolderOwnerId { get; set; }

        [JsonProperty("nodetype")]
        public FolderType FolderType { get; set; }
        [JsonProperty("doctype")]
        public DocType DocType { get; set; }
        [JsonProperty("folderParentId")]
        public Guid? FolderParentId { get; set; }

        public string FromPrincipalTitle
        {
            get => _fromPrincipalTitle.FixPersianCharacter();
            set => _fromPrincipalTitle = value;
        }

        public string Header
        {
            get => _header.FixPersianCharacter();
            set => _header = value;
        }

        public int Date { get; set; }
        public string Time { get; set; }

        [JsonProperty("fromUserId")]
        public string FromUserId { get; set; }

        [JsonProperty("mainId")]
        public Guid? MainId { get; set; }
        [JsonProperty("instanceId")]
        public Guid? InstanceId { get; set; }
        [JsonProperty("toPrincipalId")]
        public string ToPrincipalId { get; set; }

        public string Action
        {
            get
            {
                switch (DocType)
                {
                    case DocType.Announcement:
                        return "اعلامیه";
                    case DocType.Draft:
                        return "پیشنویس";
                    case DocType.Letter:
                        return "نامه";
                    case DocType.Message:
                        return "پیام";
                    default:
                        return string.Empty;
                }
            }
        }

        public string FolderTypeDsc
        {
            get
            {
                switch (DocType)
                {
                    case DocType.Announcement:
                        return "تابلو";
                    case DocType.Draft:
                        return "پوشه";
                    case DocType.Letter:
                        return "پوشه";
                    case DocType.Message:
                        return "پوشه";
                    default:
                        return string.Empty;
                }
            }
        }

        public string FancyDateTime { get; set; }

        public void NavigateToWeb()
        {
            switch (DocType)
            {
                case DocType.Letter:
                    {
                        System.Diagnostics.Process.Start("http://office.nrayvarz.ir:8080/9911.4.6/#/letterList/{\"id\":\"" +
                                                         InstanceId + "\",\"nodeType:\"" + (byte)FolderType + "}");
                        break;
                    }
                case DocType.Draft:
                    {

                        System.Diagnostics.Process.Start("http://office.nrayvarz.ir:8080/9911.4.6/#/DraftList/{\"id\":\"" +
                                                         InstanceId + "\",\"nodeType:\"" + (byte)FolderType + "}");
                        break;
                    }
                case DocType.Message:
                    {

                        System.Diagnostics.Process.Start(
                            "http://office.nrayvarz.ir:8080/9911.4.6/#/MessageList/{\"id\":\"" +
                            InstanceId + "\",\"nodeType:\"" + (byte)FolderType + "}");
                        break;
                    }
                case DocType.Announcement:
                    {
                        System.Diagnostics.Process.Start("http://office.nrayvarz.ir:8080/0004.0.0");
                        break;
                    }
                default:
                    break;
            }
        }
    }
}