using System.ComponentModel;

namespace Ray.Notification.Domain
{
    public enum FolderType : byte
    {
        [Description("دریافتی")]
        InboxFolder,
        [Description("ارسالی")]
        SentItemsFolder,
        [Description("حذف شده")]
        DeletedItemsFolder,
        SubFolder,
        PursueFolder,
        [Description("نامه های شخصی")]
        PersonalLetters,
        ArchiveFolder,
        DocumentFolder,
        [Description("پوشه اعلامیه")]
        AnnouncementFolder,
        DraftFolder,
        ProcessFolder,
        NotArchivedLetter,
        VirtualFolder,
        ReadyToSign,
        ReadyToSend,
        Offline,
        [Description("نامه های سینکی دریافتی")]
        SyncLetterReceived,
        [Description("نامه های سینکی ارسال موفق")]
        SyncLetterSendSuccessful,
        [Description("نامه های سینکی صف انتظار")]
        SyncLetterWaitingQueue,
        [Description("نامه های سینکی ارسال ناموفق")]
        SyncLetterSendUnSuccessful,
        ReadyToSendLetterEce,
        SentLetterEce,
        DeletedSentLetterEce,
        SystemLetterEce,
        ReceiptSystemEce,
        DeletedReceivedEce,
        RegistrationTemplate
    }
}