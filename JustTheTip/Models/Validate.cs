using System.Text.RegularExpressions;

namespace JustTheTip.Models {
    public static class Validate {

        private static bool IsValidLength(string input) {
            if (input.Length >= 2 && input.Length <= 50)
                return true;
            else
                return false;
        }

        public static bool Email(string input) {
            var regex = new Regex(@"[a-zA-Z0-9_\.\+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-\.]+");
            if (regex.IsMatch(input) && IsValidLength(input))
                return true;
            else
                return false;
        }
    }
}