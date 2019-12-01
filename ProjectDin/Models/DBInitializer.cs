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

            context.Users.AddRange(new User { Username = "johndoe", Password = "root123", FirstName = "John", LastName = "Doe", Email = "johndoe@thomasmore.be"});

            context.Users.AddRange(new User { Username = "din", Password = "root123", FirstName = "Din", LastName = "Vanwezemael", Email = "r0708466@student.thomasmore.be" });

            context.Poll.AddRange(new Poll { Naam = "Test van de poll" });

            context.SaveChanges();

            context.Opties.AddRange(new Optie { PollID = context.Polls.First().PollID, Naam = "Werkt" });

            context.Opties.AddRange(new Optie { PollID = context.Polls.First().PollID,  Naam = "Werkt niet" });

            context.SaveChanges();

            context.Uitnodigingen.AddRange(new Uitnodiging { PollID = context.Polls.First().PollID, UserID = context.Users.First().UserID });

            context.Antwoorden.AddRange(new Antwoord { OptieID = context.Opties.First().OptieID, UserID = context.Users.First().UserID });

            context.Friends.AddRange(new Friend { UserID = 2, UserFriendID = context.Users.First().UserID, Status = 1 });


            context.PollUsers.Add(new PollUser() { UserID = context.Users.First().UserID, PollID = context.Polls.First().PollID });
            
            context.SaveChanges();

        }
    }
}
