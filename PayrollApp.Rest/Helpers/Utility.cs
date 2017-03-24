using System;

namespace PayrollApp.Rest.Helpers
{
    public class Utility
    {
        public static DateTime NullDateValue
        {
            get { return new DateTime(1900, 1, 1); }
        }

        /// <summary>
        /// time becomes 12:00:00.000 am (Midnight)
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime ToStartOfDay(DateTime d)
        {
            DateTime dt = NullDateValue;
            if (d != NullDateValue)
            {
                try
                {
                    dt = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
                }
                catch (System.Exception)
                {
                    dt = NullDateValue;
                }
            }
            return dt;
        }

        /// <summary>
        /// time becomes 11:59:00.000 pm (a minute before midnight)
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime ToEndOfDay(DateTime d)
        {
            DateTime dt = NullDateValue;
            if (d != NullDateValue)
            {
                try
                {
                    dt = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
                    dt = dt.AddDays(1.0).AddMinutes(-1.0);
                }
                catch (System.Exception)
                {
                    dt = NullDateValue;
                }
            }
            return dt;
        }

        /// <summary>
        /// midnight (the start of the day) of the first day of the invoice week
        /// </summary>
        /// <param name="currentDay"></param>
        /// <returns></returns>
        public static DateTime StartOfWeek(DateTime currentDay)
        {
            DateTime startOfWeek = ToStartOfDay(currentDay);

            String lastDayOfWeek = TimeslipStatus.DayOfWeek.ToUpper();

            if (startOfWeek.DayOfWeek.ToString().ToUpper() == lastDayOfWeek)
                startOfWeek = startOfWeek.AddDays(-6.0);
            else
            {
                while (startOfWeek.DayOfWeek.ToString().ToUpper() != lastDayOfWeek)
                    startOfWeek = startOfWeek.AddDays(-1.0);

                startOfWeek = startOfWeek.AddDays(1.0);
            }

            return startOfWeek;
        }

        /// one second before midnight of the last day of the invoice week
        /// </summary>
        /// <param name="currentDay"></param>
        /// <returns></returns>
        public static DateTime EndOfWeek(DateTime currentDay)
        {
            return Utility.ToEndOfDay(StartOfWeek(currentDay).AddDays(7.0).AddSeconds(-1.0));
        }


        /// <summary>
        /// Get specific tag data.
        /// </summary>
        /// <param name="xmlSnippet"></param>
        /// <param name="elementID"></param>
        /// <returns></returns>
        public static String GetElement(String xmlSnippet, String elementID) // parses the XML aggregate returning the first instance of the element data in string format. usage: String lastname = getElement(customer,"LastName");
        {
            if (xmlSnippet == null)
                return String.Empty;
            else
                xmlSnippet = xmlSnippet.Trim();

            if (elementID == null)
                return String.Empty;
            else
                elementID = elementID.Trim();

            if (elementID == String.Empty)
                return String.Empty;

            String data = "";
            if (xmlSnippet.Length > 0)
                if (xmlSnippet.IndexOf("<" + elementID + ">") != -1) // found our elementID
                    data = xmlSnippet.Substring(xmlSnippet.IndexOf("<" + elementID + ">") + elementID.Length + 2,
                        xmlSnippet.IndexOf("</" + elementID + ">") - (xmlSnippet.IndexOf("<" + elementID + ">") + elementID.Length + 2));
            return data;
        }


        public static String SetElement(String xmlSnippet, String elementID, String newInnerText)
        {
            if (xmlSnippet == null)
                return String.Empty;
            else
                xmlSnippet = xmlSnippet.Trim();
            if (elementID == null)
                return xmlSnippet;
            else
                elementID = elementID.Trim();
            if (elementID == String.Empty)
                return xmlSnippet;

            if (newInnerText == null)
                newInnerText = String.Empty;
            else
                newInnerText = newInnerText.Trim();

            String currentContent = String.Empty;
            if (xmlSnippet != String.Empty)
                currentContent = GetElement(xmlSnippet, elementID);

            String newElement = "<" + elementID + ">" + newInnerText + "</" + elementID + ">";
            String blankElement = "<" + elementID + "></" + elementID + ">";

            if (currentContent == String.Empty)
            { // append new element
                if (xmlSnippet != String.Empty)
                    xmlSnippet = xmlSnippet + "\n";

                if (xmlSnippet.IndexOf(blankElement) != -1)
                    xmlSnippet = xmlSnippet.Replace(blankElement, newElement);
                else
                    xmlSnippet = xmlSnippet + newElement;
            }
            else
            { // replace existing element
                String current = "<" + elementID + ">" + currentContent + "</" + elementID + ">";
                xmlSnippet = xmlSnippet.Replace(current, newElement);
            }

            return xmlSnippet;
        }

        public static DateTime ToDateQB(String d)
        {
            if ((d.Length > 10) || (d[4] == '-') || (d[7] == '-'))
            {

                int yr = Convert.ToInt32(Utility.ToInt(d.Substring(0, 4)));
                int mo = Convert.ToInt32(Utility.ToInt(d.Substring(5, 2)));
                int dy = Convert.ToInt32(Utility.ToInt(d.Substring(8, 2)));

                return new DateTime(yr, mo, dy);
            }
            else
                return NullDateValue;
        }

        public static String ToDateQB(System.DateTime d)
        {
            return d.ToString("yyyy-MM-dd");
        }

        public static bool ToBoolean(String strBool)
        {
            if (strBool.ToUpper().IndexOf("T") != -1)
                return true;
            if (strBool.ToUpper().IndexOf("F") != -1)
                return false;
            if (strBool.IndexOf("0") != -1)
                return false;
            else
                return true;
        }

        public static String ToInt(String num)
        {
            if ((num == null) || (num.Length < 1)) return "0";

            num = num.Trim();
            if (num.Length < 1) return "0";
            bool neg = false;
            if (num[0] == '-')
            {
                neg = true;
                num = num.Remove(0, 1);
            }
            while ((num.Length > 1) && (num[0] == '0')) num = num.Remove(0, 1);

            for (int i = 0; i < num.Length; i++)
            {
                if (!Char.IsDigit(num[i]))
                {
                    num = num.Remove(i, 1);
                    i = -1;
                }
            }
            if (neg) num = "-" + num;
            if (num == "") num = "0";
            return num;
        }
    }
}