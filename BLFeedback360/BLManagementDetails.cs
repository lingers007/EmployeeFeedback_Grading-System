using DLFeedback360.Model;
using DLFeedback360.Repository;

namespace BLFeedback360
{
    public class BLManagementDetails
    {
        public static string Connection { get; set; }
        public BLManagementDetails(string conString) 
        {
            Connection = conString;
        }    

        DLManagementRepo repo = new DLManagementRepo(Connection);
        /// <summary>
        /// Gets User details.
        /// </summary>
        /// <param name="userID">userID object.</param>
        /// <param name="Password">Password object.</param>
        /// <returns>Single UserDetail object.</returns>
        public UserDetail GetLogin(string userID, string Password)
        {
            string msg = string.Empty;
            UserDetail usr = new UserDetail();
            try
            {
                // Gets userdetail object based on userid and password.
                usr = repo.GetLogin(userID, Password);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return usr;

        }

        /// <summary>
        /// Gets User details.
        /// </summary>
        /// <param name="userID">userID object.</param>
        /// <param name="Password">Password object.</param>
        /// <returns>Single UserDetail object.</returns>
        public UserDetail GetLogin2(string userID, string Password)
        {
            string msg = string.Empty;
            UserDetail usr = new UserDetail();
            try
            {
                // Gets userdetail object based on userid and password.
                usr = repo.GetLogin(userID, Password);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return usr;

        }

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
                // Update user password.
                msg = repo.UpdatePassword(usr);
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
            List<Role> lst = new List<Role>();
            try
            {
                // Gets Roles.
                lst = repo.GetVendorDropDownList();
            }
            catch (Exception ex)
            {
            }

            return lst;
        }

        /// <summary>
        /// Gets List of AddQuestion.
        /// </summary>
        /// <returns>List of AddQuestions.</returns>
        public List<AddQuestion> GetDetail()
        {
            List<AddQuestion> lstq = new List<AddQuestion>();
            try
            {
                // Gets AddQuestions.
                lstq = repo.GetDetail();
            }
            catch (Exception ex)
            {
            }

            return lstq;
        }

        /// <summary>
        /// Gets List of Designations.
        /// </summary>
        /// <returns>List of Designations.</returns>
        public List<Designation> GetVendorDropDownList1()
        {
            List<Designation> lst1 = new List<Designation>();
            try
            {
                // Gets Designations.
                lst1 = repo.GetVendorDropDownList1();
            }
            catch (Exception ex)
            {
            }

            return lst1;
        }

        /// <summary>
        /// Gets List of UserDetails.
        /// </summary>
        /// <returns>List of UserDetails.</returns>
        public List<UserDetail> GetVendorDropDownList2()
        {
            List<UserDetail> lst2 = new List<UserDetail>();
            try
            {
                // Gets UserDetails.
                lst2 = repo.GetVendorDropDownList2();
            }
            catch (Exception ex)
            {
            }

            return lst2;
        }

        /// <summary>
        /// Gets List of Categories.
        /// </summary>
        /// <returns>List of Categories.</returns>
        public List<Category> GetCatDropDownList1()
        {
            List<Category> lst3 = new List<Category>();

            try
            {
                // Gets Categoryies.
                lst3 = repo.GetCatDropDownList1();
            }
            catch (Exception ex)
            {
            }

            return lst3;
        }

        /// <summary>
        /// Gets List of Roles.
        /// </summary>
        /// <returns>List of Roles.</returns>
        public List<Role> GetRoleDropDownList1()
        {
            List<Role> lst1 = new List<Role>();
            try
            {
                // Gets Roles.
                lst1 = repo.GetRoleDropDownList1();
            }
            catch (Exception ex)
            {
            }

            return lst1;
        }

        /// <summary>
        /// Inserts UserDetail.
        /// </summary>
        /// <param name="usr">UserDetail object.</param>
        /// <returns>Success message.</returns>
        public string InsertProdData(UserDetail usr)
        {
            string msg = string.Empty;
            try
            {
                // In case before passing the prod object to DL in line 40, you want to do any data manipulation
                msg = repo.InsertProdData(usr);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
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
            string msg = string.Empty;
            try
            {
                // Insert FeedbackSchedule in FeedbackSchedule table. 
                msg = repo.InsertFeedbackDetails(fdb);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        /// <summary>
        /// Inserts AddQuestion.
        /// </summary>
        /// <param name="adq">AddQuestion object.</param>
        /// <returns>Success message.</returns>
        public string InsertQuestion(AddQuestion adq)
        {
            string msg = string.Empty;
            try
            {
                // Insert the AddQuestion in AddQuestion table. 
                msg = repo.InsertQuestion(adq);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        /// <summary>
        /// Gets List of FeedbackSchedule.
        /// </summary>
        /// <returns>List of FeedbackSchedule object.</returns>
        public List<FeedbackSchedule> GetFeedbackDetails()
        {
            List<FeedbackSchedule> lst1 = new List<FeedbackSchedule>();
            try
            {
                lst1 = repo.GetFeedbackDetails();
            }
            catch (Exception ex)
            {
            }

            return lst1;
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
            try
            {
                // Sends Email.
                msg = repo.SendEmail(email, subject, body, host, port, enableSsl, defaultCredentials, from, userName, password);
            }
            catch (Exception ex)
            {
                msg = "failed";
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
            try
            {
                // Gets Password based on Email.
                password = repo.GetPasswordByEmail(email);
            }
            catch (Exception ex)
            {

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
            try
            {
                // Inserts FeedbackRating to batabase.
                msg = repo.InsertFeedbackRatings(fdbRating, byId);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
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
            string msg = string.Empty;
            try
            {
                // Update FeedbackSchedules table based on Employee and FeedbackProvider.
                msg = repo.UpdateFeedbackFeedbackSchedules(emp, provider);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        /// <summary>
        /// Gets List of FeedbackRating.
        /// </summary>
        /// <returns>List of FeedbackRating object.</returns>
        public List<FeedbackRating> GetFeedbackRatings()
        {
            List<FeedbackRating> lst2 = new List<FeedbackRating>();
            try
            {
                // List of FeedbackRating from database.
                lst2 = repo.GetFeedbackRatings();
            }
            catch (Exception ex)
            {
            }

            return lst2;
        }

        /// <summary>
        /// Gets List of RatingCriteria.
        /// </summary>
        /// <returns>List of RatingCriteria object.</returns>
        public List<RatingCriteria> GetRatingCriterias()
        {
            List<RatingCriteria> lst2 = new List<RatingCriteria>();
            try
            {
                // List of RatingCriteria from database.
                lst2 = repo.GetRatingCriterias();
            }
            catch (Exception ex)
            {
            }

            return lst2;
        }
    }
}
