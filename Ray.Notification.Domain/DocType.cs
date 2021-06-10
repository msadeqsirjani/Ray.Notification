using System.ComponentModel;

namespace Ray.Notification.Domain
{
    public enum DocType : byte
    {
        [Description("پیام")]
        Message,
        [Description("نامه")]
        Letter,
        [Description("فایل")]
        File,
        [Description("بخشنامه ها، دستورالعمل ها و اعلانات")]
        Announcement,
        [Description("پیش نویس")]
        Draft,
        [Description("درخواست")]
        Request,
        [Description("جستجوی پیش نویس")]
        SearchDraft,
        [Description("پلاگین")]
        Plugged,
        [Description("فکس های دریافتی")]
        ReceivedFax,
        [Description("جستجوی نامه")]
        SearchLetter,
        [Description("ایمیل")]
        Email,
        [Description("نامه های سینکی")]
        SyncLetter
    }
}