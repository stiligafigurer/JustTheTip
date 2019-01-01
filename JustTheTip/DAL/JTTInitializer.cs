using JustTheTip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustTheTip.DAL {
    public class JTTInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<JTTContext> {
        protected override Seed(JTTContext context) {
            var users = new List<User> {
                new User {
                    Username ="racar",
                    Password ="racar",
                    FirstName ="Rasmus",
                    LastName ="Carlsson",
                    Email ="racar@jtt.com",
                    Gender ="Male",
                    SexualOrientation ="Straight",
                    BirthDate =new DateTime(1995,09,23),
                    ProfilePicUrl ="https://i.kym-cdn.com/entries/icons/square/000/021/807/4d7.png",
                    ZodiacSign ="Libra",
                    Country ="Sweden",
                    District ="Örebro",
                    ActiveUser = 1
                }, new User {
                    Username ="ivahl",
                    Password ="ivahl",
                    FirstName ="Ivan",
                    LastName ="Ahlblom",
                    Email ="ivahl@jtt.com",
                    Gender ="Male",
                    SexualOrientation ="Straight",
                    BirthDate =new DateTime(1990,01,01),
                    ProfilePicUrl ="https://i.kym-cdn.com/entries/icons/square/000/021/807/4d7.png",
                    ZodiacSign ="Unknown",
                    Country ="Sweden",
                    District ="Örebro",
                    ActiveUser = 1
                }, new User {
                    Username ="johol",
                    Password ="johol",
                    FirstName ="Josef",
                    LastName ="Holmberg",
                    Email ="johol@jtt.com",
                    Gender ="Male",
                    SexualOrientation ="Straight",
                    BirthDate =new DateTime(1990,01,01),
                    ProfilePicUrl ="https://i.kym-cdn.com/entries/icons/square/000/021/807/4d7.png",
                    ZodiacSign ="Unknown",
                    Country ="Sweden",
                    District ="Örebro",
                    ActiveUser = 1
                }
            };

            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            // TODO: Add more sample data
        }
    }
}