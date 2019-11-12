using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDin.Models
{
    public class DBInitializer
    {
        public static void Initialize(ProjectContext context)
        {
            context.Database.EnsureCreated();

                if(context.Users.Any()){
                    return;   
                     
                }

            context.Users.AddRange(new User { Username = "test", Password = "test", FirstName = "Test", LastName = "Test", Email = "test.test@thomasmore.be"});

            context.Users.AddRange(new User { Username = "din", Password = "root", FirstName = "Din", LastName = "Vanwezemael", Email = "dinvanwezemael@thomasmore.be" });

            context.Poll.AddRange(new Poll { Naam = "test" });

            context.SaveChanges();

            context.Friends.AddRange(new Friend { UserID = 2, UserIDFriend = context.Users.First().UserID, Status = 1 });

            context.PollOptions.AddRange(new PollOption { OptionName = "optie1", PollID = 1 });

            context.PollUsers.Add(new PollUser() { UserID = context.Users.First().UserID, PollID = context.Polls.First().PollID });
            
            context.SaveChanges();

        }
    }
}
