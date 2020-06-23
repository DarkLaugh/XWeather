﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Helpers
{
    public static class CapitalizeFirstLetter
    {
        public static string Capitalize(this string s)
        {
            if (String.IsNullOrEmpty(s)) return s;
            if (s.Length == 1) return s.ToUpper();
            return s.Remove(1).ToUpper() + s.Substring(1);
        }
    }
}
