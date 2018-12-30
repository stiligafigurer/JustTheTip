using System;
using System.Text.RegularExpressions;

namespace JustTheTip.Models {
    public static class Validate {

        public static bool Email(string input) {
            // Matches something@something.something
            var regex = new Regex(@"[a-zA-Z0-9_\.\+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-\.]+");
            return regex.IsMatch(input) && input.Length >= 2 && input.Length <= 50;
        }

        public static bool Name(string input) {
            // Matches a string of 2-15 characters. Supports most common languages, see https://stackoverflow.com/a/45871742
            var regex = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{1,14}$");
            return regex.IsMatch(input);
        }

        public static bool Password(string input) {
            // No regex because password validation rules are bad, see https://stackoverflow.com/a/48346033
            return input.Length >= 8 && input.Length <= 64;
        }

        public static bool ImageUrl(string input) {
            // Matches any jpg, gif, or png url
            var regex = new Regex(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)");
            return regex.IsMatch(input) && input.Length <= 100;
        }

        public static bool Date(string input) {
            return DateTime.TryParse(input, out DateTime output);
        }
    }
}