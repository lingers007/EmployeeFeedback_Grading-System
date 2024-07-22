using DLFeedback360.Abstract;
using DLFeedback360.Model;
using System.Net.Mail;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DLFeedback360.Repository
{
    public class DLManagementRepo : IDLManagementRepo
    {
        FeedDBContext _context;
        public DLManagementRepo(string conString)
        {
            _context = new FeedDBContext(conString);
        }
        /// <summary>
        /// Gets User details.
        /// </summary>
        /// <param name="userID">userID object.</param>
        /// <param name="Password">Password object.</param>
        /// <returns>Single UserDetail object.</returns>
        public UserDetail GetLogin(string userID, string Password)
        {
            UserDetail usr = new UserDetail();
            // Gets userdetail object based on userid and password.
            usr = _context.UserDetails.FirstOrDefault(u => u.UserID == userID && u.Password == Password);
            return usr;
        }

        /*public UserDetail GetLogin2(string userID, string Password)
        {
            UserDetail usr = new UserDetail();
            // Gets userdetail object based on userid and password.
            usr = _context.UserDetails.FirstOrDefault(u => u.UserID == userID && u.Password == Password);
            return usr;
        }*/

        /// <summary>
        /// Updates password.
        /// </summary>
        /// <param name="usr">UserDetail object.</param>
        /// <returns>Success or Failure message.</returns>
        public string UpdatePassword(UserDetail usr)
        {
            string msg = string.Empty;
            try
            {
                // Referencing the UserDetail object based on Id.
                UserDetail existing = _context.UserDetails.FirstOrDefault(u => u.Id == usr.Id);
                // Setting the updated password and LastLoggedDate with current DateTime.
                existing.Password = usr.Password;
                existing.LastLoggedDate = DateTime.Now;
                // Update user password.
                _context.SaveChanges();

                msg = "success";
            }
            catch (Exception ex)
            {
                msg = "failed";
            }
            return msg;
        }

        /// <summary>
        /// Gets List of Roles.
        /// </summary>
        /// <returns>List of Roles.</returns>
        public List<Role> GetVendorDropDownList()
        {
            /*List<Role> lst = new List<Role>();
            lst = _context.Roles.ToList();
            return lst; */
            // Gets Roles from database.
            return _context.Roles.ToList();
        }

        /// <summary>
        /// Gets List of Designations.
        /// </summary>
        /// <returns>List of Designations.</returns>
        public List<Designation> GetVendorDropDownList1()
        {
            /*List<Designation> lst1 = new List<Designation>();
            lst1 = _context.Designations.ToList();
            return lst1;*/
            // Gets Designations from database.
            return _context.Designations.ToList();
        }

        /// <summary>
        /// Gets List of AddQuestions.
        /// </summary>
        /// <returns>List of AddQuestions.</returns>
        public List<AddQuestion> GetDetail()
        {
            /*List<AddQuestion> lad = new List<AddQuestion>();
            lad = _context.AddQuestions.ToList();
            return lad;*/
            // Gets AddQuestions from database.
            return _context.AddQuestions.ToList();
        }

        /// <summary>
        /// Gets List of UserDetails.
        /// </summary>
        /// <returns>List of UserDetails.</returns>
        public List<UserDetail> GetVendorDropDownList2()
        {
            /*List<UserDetail> lst2 = new List<UserDetail>();
            lst2 = _context.UserDetails.ToList();
            return lst2;*/
            // Gets UserDetails from database.
            return _context.UserDetails.ToList();
        }

        /// <summary>
        /// Gets List of Categories.
        /// </summary>
        /// <returns>List of Categories.</returns>
        public List<Category> GetCatDropDownList1()
        {
            /*List<Category> lst3 = new List<Category>();
            lst3 = _context.Categories.ToList();
            return lst3;*/
            // Gets Categories from database.
            return _context.Categories.ToList();
        }

        /// <summary>
        /// Gets List of Roles.
        /// </summary>
        /// <returns>List of Roles.</returns>
        public List<Role> GetRoleDropDownList1()
        {
            /*List<Role> lst2 = new List<Role>();
            lst2 = _context.Roles.ToList();
            return lst2;*/
            // Gets Roles from database.
            return _context.Roles.ToList();
        }

        /// <summary>
        /// Inserts UserDetail.
        /// </summary>
        /// <param name="usr">UserDetail object.</param>
        /// <returns>Success message.</returns>
        public string InsertProdData(UserDetail usr)
        {
            string msg = string.Empty;
            bool isRecordExists = false;
            // Gets UserDetail based on UserID.
            UserDetail userIdExists = _context.UserDetails.FirstOrDefault(u => u.UserID == usr.UserID);
            // Gets UserDetail based on EmailID.
            UserDetail emailExists = _context.UserDetails.FirstOrDefault(u => u.EmailID == usr.EmailID);
            // Gets UserDetail based on EmpID.
            UserDetail empIdExists = _context.UserDetails.FirstOrDefault(u => u.EmpID == usr.EmpID);

            // Based on user existance, sets the error message.
            if (userIdExists != null && emailExists != null && empIdExists != null)
            {
                isRecordExists = true;
                msg = "UserID, EmailID, EmpID already registered with us. Record is not inserted.";
            }
            else if (userIdExists != null && emailExists != null)
            {
                isRecordExists = true;
                msg = "UserID, EmailID already registered with us. Record is not inserted.";
            }
            else if (userIdExists != null && empIdExists != null)
            {
                isRecordExists = true;
                msg = "UserID, EmpID already registered with us. Record is not inserted.";
            }
            else if (emailExists != null && empIdExists != null)
            {
                isRecordExists = true;
                msg = "EmailID, EmpID already registered with us. Record is not inserted.";
            }
            else if (userIdExists != null)
            {
                isRecordExists = true;
                msg = "UserID already registered with us. Record is not inserted.";
            }
            else if (emailExists != null)
            {
                isRecordExists = true;
                msg = "EmailID already registered with us. Record is not inserted.";
            }
            else if (empIdExists != null)
            {
                isRecordExists = true;
                msg = "EmpID already registered with us. Record is not inserted.";
            }

            // Insert if user does not exists.
            if (!isRecordExists)
            {
                // Sets CreatedBy with Name.
                usr.CreatedBy = usr.Name;
                // Add the UserDetail in UserDetails table. 
                _context.UserDetails.Add(usr);
                _context.SaveChanges();
                msg = "success";
            }
            return msg;
        }

        /// <summary>
        /// Inserts FeedbackSchedule.
        /// </summary>
        /// <param name="fdb">FeedbackSchedule object.</param>
        /// <returns>Success message.</returns>
        public string InsertFeedbackDetails(FeedbackSchedule fdb)
        {
            // Sets IsActive to false i.e. 0.
            fdb.IsActive = false;
            // Add the FeedbackSchedule in FeedbackSchedule table. 
            _context.FeedbackSchedules.Add(fdb);
            _context.SaveChanges();
            return "success";
        }

        /// <summary>
        /// Inserts AddQuestion.
        /// </summary>
        /// <param name="adq">AddQuestion object.</param>
        /// <returns>Success message.</returns>
        public string InsertQuestion(AddQuestion adq)
        {
            // Add the AddQuestion in AddQuestion table. 
            _context.AddQuestions.Add(adq);
            _context.SaveChanges();

            return "success";
        }

        /// <summary>
        /// Gets List of FeedbackSchedule.
        /// </summary>
        /// <returns>List of FeedbackSchedule object.</returns>
        public List<FeedbackSchedule> GetFeedbackDetails()
        {
            /*List<FeedbackSchedule> feedbackSchedules = new List<FeedbackSchedule>();
            feedbackSchedules = _context.FeedbackSchedules.ToList();
            return feedbackSchedules;*/
            return _context.FeedbackSchedules.ToList();
        }

        /// <summary>
        /// Sends Email.
        /// </summary>
        /// <param name="email">email object.</param>
        /// <param name="subject">subject object.</param>
        /// <param name="body">body object.</param>
        /// <param name="host">host object.</param>
        /// <param name="port">emportail object.</param>
        /// <param name="enableSsl">enableSsl object.</param>
        /// <param name="defaultCredentials">defaultCredentials object.</param>
        /// <param name="from">from object.</param>
        /// <param name="userName">userName object.</param>
        /// <param name="password">password object.</param>
        /// <returns>Success Message.</returns>
        public string SendEmail(string email, string subject, string body, string host, int port, bool enableSsl, bool defaultCredentials, string from, string userName, string password)
        {
            string msg = string.Empty;
            using (MailMessage mm = new MailMessage(new MailAddress(from), new MailAddress(email)))
            {
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = host;
                    smtp.EnableSsl = enableSsl;
                    NetworkCredential networkCred = new NetworkCredential(userName, password);
                    smtp.UseDefaultCredentials = defaultCredentials;
                    smtp.Credentials = networkCred;
                    smtp.Port = port;
                    // Sends Email.
                    smtp.Send(mm);
                    msg = "success";
                }
            }
            return msg;
        }

        /// <summary>
        /// Gets Password based on Email Address.
        /// </summary>
        /// <param name="email">email object.</param>
        /// <returns>Password of user.</returns>
        public string GetPasswordByEmail(string email)
        {
            string password = string.Empty;
            // Gets use based on EmailId.
            UserDetail user = _context.UserDetails.FirstOrDefault(u => u.EmailID == email);
            // Check if user exists or not.
            if (user != null)
            {
                password = user.Password;
            }
            return password;
        }

        /// <summary>
        /// Inserts FeedbackRating.
        /// </summary>
        /// <param name="fdbRating">List of FeedbackRating object.</param>
        /// <param name="byId">ById object.</param>
        /// <returns>Success message.</returns>
        public string InsertFeedbackRatings(List<FeedbackRating> fdbRating, string byId)
        {
            string msg = string.Empty;
            //Looping through fdbRating.
            foreach (FeedbackRating rating in fdbRating)
            {
                // Sets ById.
                rating.ByID = byId;
                // Sets CreatedBy with current DataTime.
                rating.CreatedBy = DateTime.Now;
                _context.FeedbackRatings.Add(rating);
            }
            // Saving to database.
            _context.SaveChanges();

            msg = "success";
            return msg;
        }

        /// <summary>
        /// Update Feedback Schedules.
        /// </summary>
        /// <param name="emp">emp object.</param>
        /// <param name="provider">provider object.</param>
        /// <returns>Success message.</returns>
        public string UpdateFeedbackFeedbackSchedules(string emp, string provider)
        {
            // Gets FeedbackSchedule based on Employee and FeedbackProvider.
            FeedbackSchedule feedbackSchedule = _context.FeedbackSchedules.Where(x => x.Employee == emp && x.FeedbackProvider == provider).FirstOrDefault();
            if (feedbackSchedule != null)
            {
                // Sets IsActive to True i.e. 1.
                feedbackSchedule.IsActive = true;
                _context.SaveChanges();
            }
            return "success";
        }

        /// <summary>
        /// Gets List of FeedbackRating.
        /// </summary>
        /// <returns>List of FeedbackRating object.</returns>
        public List<FeedbackRating> GetFeedbackRatings()
        {
            /*List<FeedbackRating> feedbackRatings = new List<FeedbackRating>();
            feedbackRatings = _context.FeedbackRatings.ToList();
            return feedbackRatings;*/
            return _context.FeedbackRatings.ToList();
        }

        /// <summary>
        /// Gets List of RatingCriteria.
        /// </summary>
        /// <returns>List of RatingCriteria object.</returns>
        public List<RatingCriteria> GetRatingCriterias()
        {
            /*List<RatingCriteria> ratingCriterias = new List<RatingCriteria>();
            ratingCriterias = _context.RatingCriteria.ToList();
            return ratingCriterias;*/
            return _context.RatingCriteria.ToList();
        }
    }
}
