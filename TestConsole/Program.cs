// Console Application that can be used for testing logic in the main project.
// Right-click the project (TestConsole) in the Solution Explorer and select Debug to run this.

using JustTheTip.Models;
using System;

namespace TestConsole {
    public class Program {

        static void Main(string[] args) {
            while (true) {
                var input = Console.ReadLine();
                if (Validate.Email(input))
                    Console.WriteLine("Valid");
                else
                    Console.WriteLine("Invalid");
            }
        }

    }
}
