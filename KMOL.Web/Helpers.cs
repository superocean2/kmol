using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace System
{
    public static class Helpers
    {
        public static DateTime ToDate(this string date)
        {
            //date dd-mm-yyyy
            try
            {
                string[] arr = date.Split('-');
                return new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]));
            }
            catch (Exception)
            {

                return DateTime.Now;
            }

        }
        public static string FormatPrice(this decimal price)
        {
            return Regex.Replace(price.ToString(), "(\\d)(?=(\\d{3})+(?!\\d))",m=>m.Groups[0].Value +",");
        }
    }
}