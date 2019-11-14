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

            context.Opties.AddRange(new Optie { PollID = context.Polls.First().PollID, Naam = "optie1" });

            context.Opties.AddRange(new Optie { PollID = context.Polls.First().PollID,  Naam = "optie2" });

            context.SaveChanges();

            context.Antwoorden.AddRange(new Antwoord { OptieID = context.Opties.First().OptieID, UserID = context.Users.First().UserID });

            context.Friends.AddRange(new Friend { UserID = 2, UserFriendID = context.Users.First().UserID, Status = 1 });

            //context.PollOpties.AddRange(new PollOptie { OptieID = context.Opties.First().OptieID, PollID = context.Polls.First().PollID });

            context.PollUsers.Add(new PollUser() { UserID = context.Users.First().UserID, PollID = context.Polls.First().PollID });
            
            context.SaveChanges();

        }
    }
}
