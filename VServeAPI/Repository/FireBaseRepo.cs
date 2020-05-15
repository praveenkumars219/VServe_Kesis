using FirebaseAdmin.Auth;
using GraphQl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GraphQl.Repository
{
    public class FireBaseRepo
    {
        public FireBaseRepo()
        {

        }

        public async Task<Users> CreateUser(Users user)
        {
            UserRecordArgs args = new UserRecordArgs()
            {
                Email = user.Email,
                EmailVerified = false,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                DisplayName = user.DisplayName,
                PhotoUrl = user.PhotoUrl,
                Disabled = false,
            };
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args).ConfigureAwait(false);
            var link = await FirebaseAuth.DefaultInstance.GenerateEmailVerificationLinkAsync(user.Email).ConfigureAwait(false);     
            user.UserId = userRecord.Uid;
            return user;
        }

        public async Task<Users> UpdateUser(Users user)
        {
            UserRecordArgs args = new UserRecordArgs()
            {
                Uid = user.UserId,
                Email = user.Email,
                EmailVerified = false,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                DisplayName = user.DisplayName,
                PhotoUrl = user.PhotoUrl,
                Disabled = false,
            };
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.UpdateUserAsync(args).ConfigureAwait(false);
            user.UserId = userRecord.Uid;
            return user;
        }

        public async Task<Users> DeleteUser(Users user)
        {
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(user.UserId).ConfigureAwait(false);
            return user;
        }

        public async Task<Users> GetUserByEmail(string emailId)
        {
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(emailId);
            return UserMetadata(userRecord);
        }

        private Users UserMetadata(UserRecord userRecord)
        {
            return new Users
            {
                Email = userRecord.Email,
                DisplayName = userRecord.DisplayName,
                PhotoUrl = userRecord.PhotoUrl,
                PhoneNumber = userRecord.PhoneNumber,
                UserId = userRecord.Uid,
            };
        }

        public async Task<Users> GetUserById(string userId)
        {
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(userId);
            return UserMetadata(userRecord);
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            var users = FirebaseAuth.DefaultInstance.ListUsersAsync(null).GetEnumerator();
            List<Users> listOfUsers = new List<Users>();

            while (await users.MoveNext())
            {
                ExportedUserRecord userRecord = users.Current;
                listOfUsers.Add(new Users
                {
                    Email = userRecord.Email,
                    DisplayName = userRecord.DisplayName,
                    PhotoUrl = userRecord.PhotoUrl,
                    PhoneNumber = userRecord.PhoneNumber,
                    UserId = userRecord.Uid,
                });
            }
            return listOfUsers;
        }

        public async Task GetJobTypes(string searchKey)
        {
        }

        public void SendEmail(string from,string to,string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("noreply@vserve - app.firebaseapp.com");
            mail.To.Add(to);
            mail.Subject = "Verify your email for project-vserveapp";
            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("myadtrevismin@gmail.com", "Pra1k@219");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }

        //public async Task<Users> LoginUser()
        //{
        //   // await FirebaseAuth.DefaultInstance.GenerateSignInWithEmailLinkAsync
        //}

    }
}
