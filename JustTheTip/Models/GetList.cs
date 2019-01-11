using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace JustTheTip.Models {
    public static class GetList {

        public static List<string> Countries() {
            var countries = new List<string>();
            CultureInfo[] cInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (var cInfo in cInfoList) {
                var r = new RegionInfo(cInfo.LCID);
                if (!(countries.Contains(r.EnglishName))) {
                    countries.Add(r.EnglishName);
                }
            }
            countries.Sort();
            return countries;
        }

        public static List<string> ZodiacSigns() {
            return new List<string> {
                "Aries",
                "Taurus",
                "Gemini",
                "Cancer",
                "Leo",
                "Virgo",
                "Libra",
                "Scorpio",
                "Sagittarius",
                "Capricorn",
                "Aquarius",
                "Pisces"
            };
        }

        public static List<string> Genders() {
            return new List<string> {
                "Female",
                "Male",
                "Other"
            };
        }

        public static List<string> SexualOrientations() {
            return new List<string> {
                "Bisexual",
                "Heterosexual",
                "Homosexual",
                "Other"
            };
        }
    }
}