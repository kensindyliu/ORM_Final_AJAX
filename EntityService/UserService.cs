using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;

namespace EntityService
{
    public class UserService
    {
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            SchoolSystemContext context = new SchoolSystemContext();
            users = context.Users.ToList();
            return users;
           
        }

        public bool UpdateUser(User user)
        {
            SchoolSystemContext context = new SchoolSystemContext();
            var entityToUpdate = context.Users.FirstOrDefault(e => e.UserID == user.UserID);
            if(entityToUpdate != null)
            {
                entityToUpdate.UserName = user.UserName;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteAUser(int userId)
        {
            SchoolSystemContext context = new SchoolSystemContext();
            var entityToDelete = context.Users.FirstOrDefault(e => e.UserID == userId);
            if(entityToDelete != null )
            {
                //delete users
                context.Users.Remove(entityToDelete);
                //delete the testresult as well
                List<TestResult> results = context.TestResults.ToList();
                context.TestResults.RemoveRange(results);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

           
        }

        public bool AddAUser(User user)
        {
            SchoolSystemContext context = new SchoolSystemContext();
            context.Users.Add(user);
            context.SaveChanges();
            return true;

        }
    }
}
