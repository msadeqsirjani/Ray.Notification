namespace Ray.Notification.Common.Convertors
{
    public static class PersianCharacterConvertor
    {
        public static string FixPersianCharacter(this string val)
        {
            return string.IsNullOrEmpty(val)
                ? null
                : val
                    .Replace("۰", "0")
                    .Replace("۱", "1")
                    .Replace("۲", "2")
                    .Replace("۳", "3")
                    .Replace("۴", "4")
                    .Replace("۵", "5")
                    .Replace("۶", "6")
                    .Replace("۷", "7")
                    .Replace("۸", "8")
                    .Replace("۹", "9")
                    .Replace("ي", "ی")
                    .Replace("ك", "ک")
                    .Replace("ک", "ک"); ;
        }
    }
}