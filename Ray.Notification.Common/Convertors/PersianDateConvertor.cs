using System;
using System.Globalization;

namespace Ray.Notification.Common.Convertors
{
    public static class PersianDateConvertor
    {
        static PersianDateConvertor()
        {
            PersianCalendar = new PersianCalendar();
        }

        public static PersianCalendar PersianCalendar { get; set; }

        public static string ConvertDateToPersianDateTime(this DateTime date)
        {
            return PersianCalendar.GetYear(date) + "/" + PersianCalendar.GetMonth(date).ToString("00") + "/" +
                   PersianCalendar.GetDayOfMonth(date).ToString("00") + " "
                   + PersianCalendar.GetHour(date) + ":" + PersianCalendar.GetMinute(date) + ":" + PersianCalendar.GetSecond(date);
        }
        public static string TryConvertDateToPersianDateTime(this DateTime date)
        {
            try
            {
                return PersianCalendar.GetYear(date) + "/" + PersianCalendar.GetMonth(date).ToString("00") + "/" +
                       PersianCalendar.GetDayOfMonth(date).ToString("00") + " "
                       + PersianCalendar.GetHour(date) + ":" + PersianCalendar.GetMinute(date) + ":" + PersianCalendar.GetSecond(date);
            }
            catch
            {
                var dateTimeNow = DateTime.Now;
                return dateTimeNow.ConvertDateToPersianDateTime(); ;
            }
        }
        public static string ConvertDateToPersianDateTime(this DateTime? date)
        {
            var result = "";
            if (date != null)
                result = TryConvertDateToPersianDateTime(date.Value);
            return result;
        }
        public static string ConvertDateToPersian(this DateTime date)
        {
            return PersianCalendar.GetYear(date) + "" + PersianCalendar.GetMonth(date).ToString("00") + "" +
                   PersianCalendar.GetDayOfMonth(date).ToString("00");
        }
        public static string ConvertDateToPersianSeparated(this DateTime date)
        {
            var stringDate = PersianCalendar.GetYear(date) + "" + PersianCalendar.GetMonth(date).ToString("00") + "" +
                             PersianCalendar.GetDayOfMonth(date).ToString("00");
            return stringDate.Substring(0, 4) + "/" + stringDate.Substring(4, 2) + "/" + stringDate.Substring(6, 2);
        }
        public static string ConvertDateToPersianSeparated(this int? @this)
        {
            if (!@this.HasValue || @this.Value == default)
                return string.Empty;

            var date = @this.ToString();

            return $"{date.Substring(0, 4)}/{date.Substring(4, 2)}/{date.Substring(6, 2)}";
        }
        public static string ConvertDateToFullPersian(this DateTime date)
        {
            string[] days = { "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه", "شنبه" };
            string[] months =
            {
                "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی",
                "بهمن", "اسفند"
            };
            return days[(int)PersianCalendar.GetDayOfWeek(date)] + " " + PersianCalendar.GetDayOfMonth(date) + " " +
                   months[PersianCalendar.GetMonth(date) - 1] + " " + PersianCalendar.GetYear(date);
        }
        public static string ConvertDateToPersianTime(this DateTime date)
        {
            return PersianCalendar.GetHour(date).ToString("00") + ":" + PersianCalendar.GetMinute(date).ToString("00") +
                   ":" + PersianCalendar.GetSecond(date).ToString("00");
        }
        public static string ConvertDateToPersianTime(this DateTime? date)
        {
            var result = "";
            if (date != null)
                result = ConvertDateToPersianTime(date);
            return result;
        }
        public static int GetPersianYear(this DateTime date)
        {
            return PersianCalendar.GetYear(date);
        }
        public static int GetPersianMonth(this DateTime date)
        {
            return PersianCalendar.GetMonth(date);
        }
        public static int GetPersianDayOfMonth(this DateTime date)
        {
            return PersianCalendar.GetDayOfMonth(date);
        }
        public static int GetIntegerPersianDate(this DateTime date)
        {
            var str = PersianCalendar.GetYear(date) + PersianCalendar.GetMonth(date).ToString("00") +
                      PersianCalendar.GetDayOfMonth(date).ToString("00");
            return int.Parse(str);
        }
        public static int GetIntegerGeorgianDate(this DateTime date)
        {
            var str = date.Year + date.Month.ToString("00") + date.Day.ToString("00");
            return int.Parse(str);
        }
        public static string GetSeparatedDate(int date)
        {
            if (date == 0) return string.Empty;
            var stringDate = date.ToString();
            return stringDate.Substring(0, 4) + "/" + stringDate.Substring(4, 2) + "/" + stringDate.Substring(6, 2);
        }
        public static string GetPersianDate(this string date)
        {
            if (date == null) throw new ArgumentNullException(nameof(date));
            var tempDate = DateTime.Now;
            var persianCalendar = new PersianCalendar();

            var str = persianCalendar.GetYear(tempDate) + "/" + persianCalendar.GetMonth(tempDate) + "/" +
                      persianCalendar.GetDayOfMonth(tempDate);

            return str;
        }
        public static DateTime ConvertDateTimeToPersianDateTime(DateTime dateTime)
        {
            var persianCalendar = new PersianCalendar();
            var cultureInfo = new CultureInfo("fa-IR");
            var str = persianCalendar.GetYear(dateTime) + " / " + persianCalendar.GetMonth(dateTime) + " / " +
                      persianCalendar.GetDayOfMonth(dateTime) + " " + persianCalendar.GetHour(dateTime) + ":" +
                      persianCalendar.GetMinute(dateTime) + ":" + persianCalendar.GetSecond(dateTime);
            return DateTime.Parse(str, cultureInfo);
        }
        public static DateTime PersianToGregorianDate(string date)
        {
            var dateParts = date.Split('/');

            var year = Convert.ToInt32(dateParts[0]);
            var month = Convert.ToInt32(dateParts[1]);
            var day = Convert.ToInt32(dateParts[2]);

            return new DateTime(year, month, day, new PersianCalendar());
        }
        private static string AddZero(int partOfDate)
        {
            var result = partOfDate.ToString();
            if (partOfDate < 10)
                result = '0' + partOfDate.ToString();
            return result;
        }
        ///  <summary>
        /// تبدیل تاریخ شمسی با اسکش به نوع عددی
        ///  </summary>
        ///  <param name="strDate"></param>
        ///  <param name="date"></param>
        ///  <param name="setEmptyPartMaxValue"></param>
        ///  <returns></returns>
        public static bool TryParse(string strDate, ref int date, bool setEmptyPartMaxValue = false)
        {
            var result = false;
            var dateParts = strDate.Split('/');
            var year = 0;
            var month = setEmptyPartMaxValue ? 12 : 1;
            var day = setEmptyPartMaxValue ? 31 : 1;
            try
            {
                if (dateParts.Length > 0)
                    year = Convert.ToInt32(dateParts[0]);

                if (dateParts.Length > 1)
                    month = Convert.ToInt32(dateParts[1]);

                if (dateParts.Length > 2)
                    day = Convert.ToInt32(dateParts[2]);


                date = Convert.ToInt32(year + AddZero(month) + AddZero(day));
                result = true;
            }
            catch
            {
                return result;
            }

            return true;
        }
        public static DateTime? TryPersianDateTimeToGregorianDateTime(string date, bool setEmptyPartMaxValue = false)
        {
            DateTime? result = null;
            var dateParts = date.Replace(" ", "/").Replace(":", "/").Split('/');
            var year = 0;
            var month = setEmptyPartMaxValue ? 12 : 1;
            var day = setEmptyPartMaxValue ? 30 : 1;
            var hour = setEmptyPartMaxValue ? 11 : 0; var minute = setEmptyPartMaxValue ? 59 : 0; var sec = setEmptyPartMaxValue ? 59 : 0;
            try
            {
                if (dateParts.Length > 0)
                    year = Convert.ToInt32(dateParts[0]);

                if (dateParts.Length > 1)
                    month = Convert.ToInt32(dateParts[1]);

                if (dateParts.Length > 2)
                    day = Convert.ToInt32(dateParts[2]);

                if (dateParts.Length > 3)
                    hour = Convert.ToInt32(dateParts[3]);

                if (dateParts.Length > 4)
                    minute = Convert.ToInt32(dateParts[4]);

                if (dateParts.Length > 5)
                    sec = Convert.ToInt32(dateParts[5]);

                result = new DateTime(year, month, day, hour, minute, sec, new PersianCalendar());
            }
            catch
            {
                // ignore
            }
            return result;
        }
        public static DateTime PersianToGregorianDate(int date, string time)
        {
            if ((int)(Math.Log10(date) + 1) != 4 + 2 + 2)
                date = Convert.ToInt32(DateTime.Now.ConvertDateToPersian().Replace(":", ""));

            if (time.Length != 2 + 1 + 2 + 1 + 2 || !(time[2] == time[5] && time[5] == ':'))
                throw new ArgumentException("Expected 'time' to have length of 8 (HH:MM:SS).");

            var year = date / 10000;
            var month = date / 100 - year * 100;
            var day = date - year * 10000 - month * 100;

            var hour = int.Parse($"{time[0]}{time[1]}");
            var minute = int.Parse($"{time[3]}{time[4]}");
            var second = int.Parse($"{time[6]}{time[7]}");

            var jc = new PersianCalendar();
            return jc.ToDateTime(year, month, day, hour, minute, second, 0);
        }
        public static DateTime? TryPersianToGregorianDate(int date, string time)
        {
            DateTime? result = null;
            try
            {
                result = PersianToGregorianDate(date, time);
            }
            catch
            {
                // ignore
            }

            return result;
        }
        public static string FancyDateDiff(int date, string time)
        {
            return FancyDateDiff(PersianToGregorianDate(date, time));
        }
        public static string FancyDateDiff(DateTime d)
        {
            string result;

            var diff = DateTime.Now - d;

            if (diff.Ticks <= 0)
                result = "به آینده خوش آمدید";
            else if (diff.Days <= 0)
                if (diff.Hours <= 0)
                    if (diff.Minutes <= 0)
                        result = "کمتر از یک دقیقه";
                    else if (diff.Minutes == 59)
                        result = "یک ساعت قبل";
                    else
                        result = diff.Minutes == 1 ? "یک دقیقه قبل" : diff.Minutes + " دقیقه قبل";
                else
                    result = diff.Hours == 1 || diff.Hours == 0 && diff.Minutes == 59
                        ? "یک ساعت قبل"
                        : diff.Hours + " ساعت قبل";
            else if (diff.Days < 7)
                result = diff.Days + " روز قبل";
            else if (diff.Days == 7)
                result = "هفته قبل";
            else if (diff.Days / 30 == 0)
                result = diff.Days / 7 + 1 + " هفته قبل";
            else
                result = diff.Days / 366 == 0 ? diff.Days / 30 + " ماه قبل" : diff.Days / 366 + " سال قبل";

            return result;
        }
        public static string ConvertToCorrectDateTime(string datetime, string serverDate)
        {
            string result;
            if (string.IsNullOrEmpty(datetime))
                result = serverDate;
            else
            {
                DateTime mailDateTime;
                var parseResult = DateTime.TryParse(datetime.Replace("(IRDT)", string.Empty), out mailDateTime);

                result = parseResult ? GetDateMailFormat(mailDateTime) : serverDate;
            }
            return result;
        }
        public static string GetDateMailFormat(DateTime date)
        {
            var t = date.ConvertDateToPersianDateTime().Split(' ');
            var res = $"{t[1]} {t[0]}";
            return res;
        }
        /// <summary>
        /// تبدیل تاریخ میلادی رشته ای به نوع تاریخ
        /// </summary>
        /// <param name="dateString">Format Is  yyyymmdd</param>
        /// <returns></returns>
        public static DateTime ToDateTime(string dateString)
        {
            var day = dateString.Substring(6, 2);
            var month = dateString.Substring(4, 2);
            var year = dateString.Substring(0, 4);
            try
            {
                return new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
            }
            catch (ArgumentOutOfRangeException)
            {
                return CorrectDate(year, month, day);
            }
        }
        private static DateTime CorrectDate(string year, string month, string day)
        {
            switch (month + day)
            {
                case "0229":
                case "0230":
                case "0231":
                    return new DateTime(int.Parse(year), int.Parse(month), int.Parse("28"));

                case "0431":
                    return new DateTime(int.Parse(year), int.Parse(month), int.Parse("30"));

                case "0631":
                    return new DateTime(int.Parse(year), int.Parse(month), int.Parse("30"));
            }
            return new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
        }
    }
}
