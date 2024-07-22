using BLFeedback360;
using DLFeedback360.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
//using NuGet.Protocol.Core.Types;

namespace Feedback360.Controllers
{
    [UserPermissionAttribute]
    public class UserController : Controller
    {
        BLManagementDetails objBL;
        // IConfiguration Interface to read connection from appsetting.json.
        public IConfiguration Configuration { get; set; }
        public string ConnectionString { get; set; }
        public UserController(IConfiguration configuration)
        {
            // Sets the IConfiguration interface.
            this.Configuration = configuration;
            // Sets the Connection from appsettings.json.
            this.ConnectionString = this.Configuration.GetSection("ConnectionStrings")["EMMAConnection"];
            objBL = new BLManagementDetails(this.ConnectionString);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Fdpage()
        {
            // Gets List of FeedbackSchedule.
            List<FeedbackSchedule> feedbackSchedules = objBL.GetFeedbackDetails();
            // Gets List of UserDetail.
            List<UserDetail> userDetails = objBL.GetVendorDropDownList2();
            // Gets current loggedin user id.
            int logedInId = Convert.ToInt32(HttpContext.Session.GetString("Curr"));
            // Gets UserDetail based on loggedin user id.
            UserDetail usr = objBL.GetVendorDropDownList2().FirstOrDefault(x => x.Id == logedInId);
            List<FeedbackDetail> feedbackDetails = (from fs in feedbackSchedules
                                                    join ud in userDetails on fs.Employee equals ud.UserID
                                                    select new FeedbackDetail
                                                    {
                                                        EmployeeId = ud.UserID,
                                                        EmployeeName = ud.Name,
                                                        IsActive = fs.IsActive,
                                                        FeedbackProvider = fs.FeedbackProvider,
                                                        FeedbackCategory = fs.FeedbackCategory,
                                                        Id = fs.Id
                                                    }).ToList();
            // Filter and get FeedbackSchedule based on UserID.
            feedbackDetails = feedbackDetails.Where(x => x.FeedbackProvider.Equals(usr.UserID)).ToList();
            return View(feedbackDetails);
        }

        [HttpGet]
        public IActionResult GetLogin()
        {
            HttpContext.Session.Remove("Curr");
            TempData["RegMessage"] = null;
            return View();
        }

        [HttpPost]
        public IActionResult GetLogin(string UserID, string Password)
        {
            // Validate Login.
            UserDetail usr = new UserDetail();
            usr = objBL.GetLogin(UserID, Password);

            if (usr != null)
            {
                // Sets userId in Session.
                HttpContext.Session.SetString("Curr", usr.Id.ToString());
                //usr.Id = Id.ToString();
                // usr.Id = Id;

                // ViewBag.nahim =usr.Id;


                //if( string.IsNullOrEmpty(usr.LastLoggedDate))
                if (usr.LastLoggedDate == null)
                {
                    // Redirects to ChangePassword page.
                    return RedirectToAction("ChangePassword");
                }
                else
                {
                    // Redirects to Register page.
                    return RedirectToAction("Register");
                }

                //return RedirectToAction("Create");
            }
            else
            {
                // Invalid UserId Or Password.
                ViewBag.Error = "Login failed";
            }
            return View();
        }

        public IActionResult GiveFeedback(int id)
        {
            // Gets current loggedin user id.
            int userId = Convert.ToInt32(HttpContext.Session.GetString("Curr"));
            // Gets DesignID based on loggedin user id.
            string designID = objBL.GetVendorDropDownList2().FirstOrDefault(x => x.Id == userId).DesignID;
            // Sets Employee name in ViewBag object.
            ViewBag.Id = objBL.GetFeedbackDetails().Where(x => x.Id == id).FirstOrDefault().Employee;
            // Gets List of AddQuestion based on DesignID.
            List<AddQuestion> questions = objBL.GetDetail().Where(x => x.DesignID.Equals(designID)).ToList();
            return View(questions);
        }
        public IActionResult GetLogin2()
        {
            return View();
        }
        public IActionResult FeedbackQuestion()
        {
            // Gets List of Designations.
            List<Designation> vendorDisplays = objBL.GetVendorDropDownList1();
            ViewBag.VendorDisplay = vendorDisplays;
            return View();
        }

        [HttpPost]
        public IActionResult FeedbackQuestion(AddQuestion addQuestion)
        {
            // Gets AddQuestion based on DesignID.
            AddQuestion question = objBL.GetDetail().LastOrDefault(x => x.DesignID.Equals(addQuestion.DesignID));
            int qid = 1;
            if (question != null)
            {
                // Generate new Question Id.
                qid = question.Qid + 1;
            }
            addQuestion.Qid = qid;
            // Inserts Question.
            string msg = objBL.InsertQuestion(addQuestion);

            // Sets Questions based on DesignID in ViewBag object.
            ViewBag.FeedbackQuestions = objBL.GetDetail().Where(x => x.DesignID.Equals(addQuestion.DesignID)).ToList();
            ViewBag.VendorDisplay = objBL.GetVendorDropDownList1();
            return View("FeedbackQuestion");
        }


        [HttpPost]
        public IActionResult GetQuestionsByDesign(string DesignID)
        {
            // Sets Questions based on DesignID in ViewBag object.
            ViewBag.FeedbackQuestions = objBL.GetDetail().Where(x => x.DesignID.Equals(DesignID)).ToList();
            ViewBag.VendorDisplay = objBL.GetVendorDropDownList1();
            return View("FeedbackQuestion");
        }

        public IActionResult GetQuestion()
        {
            // Gets AddQuestion.
            List<AddQuestion> LSR = objBL.GetDetail();

            return View(LSR);
        }

        [HttpPost]
        public IActionResult GetLogin2(string UserID, string Password)
        {
            // Validate Login.
            UserDetail usr = new UserDetail();
            usr = objBL.GetLogin(UserID, Password);

            if (usr != null)
            {
                // Sets userId in Session.
                HttpContext.Session.SetString("FeedbackUser", usr.Id.ToString());
                //HttpContext.Session.SetString("FeedbackUser", usr.DesignID);
                if (usr.LastLoggedDate == null)
                {
                    // Redirects to ChangePassword page.
                    return RedirectToAction("ChangePassword");
                }
                else
                {
                    // Redirects to Fdpage page.
                    return RedirectToAction("Fdpage");
                }

                //return RedirectToAction("Create");
            }
            else
            {
                // Invalid UserId Or Password.
                ViewBag.Error = "Login failed";
            }
            return View();
        }

        public IActionResult Register()
        {
            // Gets Roles.
            List<Role> vendorDisplays = objBL.GetVendorDropDownList();
            ViewBag.VendorDisplay = vendorDisplays;
            // Gets Designations.
            List<Designation> vendorDisplays1 = objBL.GetVendorDropDownList1();
            ViewBag.VendorDisplay1 = vendorDisplays1;
            UserDetail usr = new UserDetail();
            // Sets CreatedBy with current LoggedIn user Id.
            usr.CreatedBy = HttpContext.Session.GetString("Curr");
            ViewBag.Info = usr.CreatedBy;
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserDetail usr)
        {
            // Inserts UserDetail.
            string msg = objBL.InsertProdData(usr);
            if (msg == "success")
            {
                // Code for sending email.
                // Reading SMTP section from appsettings.json.
                string host = this.Configuration.GetValue<string>("Smtp:Server");
                int port = this.Configuration.GetValue<int>("Smtp:Port");
                bool enableSsl = this.Configuration.GetValue<bool>("Smtp:EnableSsl");
                bool defaultCredentials = this.Configuration.GetValue<bool>("Smtp:DefaultCredentials");
                string from = this.Configuration.GetValue<string>("Smtp:From");
                string userName = this.Configuration.GetValue<string>("Smtp:Username");
                string password = this.Configuration.GetValue<string>("Smtp:Password");

                string subject = "Registration";
                string body = string.Format("Registration Success.<br /><br />Your Username: {0}<br />Password: {1}", usr.UserID, usr.Password);

                // Send Email.
                msg = objBL.SendEmail(usr.EmailID, subject, body, host, port, enableSsl, defaultCredentials, from, userName, password);

                TempData["RegMessage"] = msg == "failed" ? "Data inserted successufully. Sending email failed." : "Data inserted successufully. Email Sent.";
                return RedirectToAction("Register");
            }
            else
            {
                TempData["RegMessage"] = msg;
                return RedirectToAction("Register");
            }
        }

        public IActionResult RegisterFeedback()
        {
            // Gets Categories.
            List<Category> catDisplays1 = objBL.GetCatDropDownList1();
            ViewBag.VendorDisplay1 = catDisplays1;
            // Gets UserDetail except DesignID 16 (External).
            List<UserDetail> vendorDisplays2 = objBL.GetVendorDropDownList2().Where(x => x.DesignID != "16").ToList();
            ViewBag.VendorDisplay2 = vendorDisplays2;
            return View();
        }

        [HttpPost]
        public IActionResult RegisterFeedback(FeedbackSchedule fdb, string UserID)
        {
            // Inserts FeedbackDetails.
            fdb.Employee = UserID;
            string msg = objBL.InsertFeedbackDetails(fdb);
            if (msg == "success")
            {
                /*UserDetail ud = objBL.GetVendorDropDownList2().FirstOrDefault(x => x.UserID == fdb.Employee.ToString());
                string emailId = ud.EmailID;
                string userId = ud.UserID;*/

                // Gets DesignationName based on DesignID.
                string designationName = objBL.GetVendorDropDownList1().FirstOrDefault(x => x.DesignID == fdb.FeedbackCategory).Desig;
                string date = fdb.LastDate.ToShortDateString();
                // Gets Email Id of FeedbackProvider.
                string emailId = objBL.GetVendorDropDownList2().FirstOrDefault(x => x.UserID == fdb.Employee).EmailID;
                // Code for sending email.
                // Reading SMTP section from appsettings.json.
                string host = this.Configuration.GetValue<string>("Smtp:Server");
                int port = this.Configuration.GetValue<int>("Smtp:Port");
                bool enableSsl = this.Configuration.GetValue<bool>("Smtp:EnableSsl");
                bool defaultCredentials = this.Configuration.GetValue<bool>("Smtp:DefaultCredentials");
                string from = this.Configuration.GetValue<string>("Smtp:From");
                string userName = this.Configuration.GetValue<string>("Smtp:Username");
                string password = this.Configuration.GetValue<string>("Smtp:Password");

                string subject = "Feedback";
                string body = string.Format("A feed back review from <b>{0}</b> category has been scheduled to give feedback on your performance on the Date <b>{1}</b>.<br />Thank you so much.", designationName, date);
                // Sending Email.
                msg = objBL.SendEmail(emailId, subject, body, host, port, enableSsl, defaultCredentials, from, userName, password);

                TempData["RegisterFeedbackMessage"] = msg == "failed" ? "Data inserted successufully. Sending email failed." : "Data inserted successufully. Feedback Email Sent.";

                return RedirectToAction("RegisterFeedback");
            }
            else
            {
                return RedirectToAction("RegisterFeedback");
            }
        }

        [HttpPost]
        public JsonResult PopulateNames(string designId, string categoryId, string userId)
        {
            // Gets UserDetail.
            List<UserDetail> userDetails = objBL.GetVendorDropDownList2();
            // Get all User based on CatId.
            switch (categoryId)
            {
                case "1":
                    // Gets userDetails less than specified designId.
                    userDetails = userDetails.Where(x => Convert.ToInt32(x.DesignID) <= Convert.ToInt32(designId)).ToList();
                    break;
                case "2":
                    // Gets userDetails having specified designId.
                    userDetails = userDetails.Where(x => x.DesignID.Equals(designId)).ToList();
                    break;
                case "3":
                    // Gets userDetails greater than specified designId.
                    userDetails = userDetails.Where(x => Convert.ToInt32(x.DesignID) > Convert.ToInt32(designId)).ToList();
                    break;
                case "4":
                    // Gets External userDetails.
                    userDetails = userDetails.Where(x => x.DesignID.Equals("16")).ToList();
                    break;
                default:
                    break;
            }

            // Get single User based on UserID.
            UserDetail udToBeRemove = objBL.GetVendorDropDownList2().FirstOrDefault(x => x.UserID == userId);
            if (udToBeRemove != null)
            {
                // Remove the single user from the List<UserDetail>.
                userDetails.Remove(udToBeRemove);
            }

            return Json(userDetails);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            TempData["RegMessage"] = string.Empty;
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string emailId)
        {
            // Gets Password based on EmailId.
            string userPassword = objBL.GetPasswordByEmail(emailId);
            // Email not exists.
            if (string.IsNullOrEmpty(userPassword))
            {
                TempData["ForgotMessage"] = "Email not registered with us.";
                return View();
            }
            else
            {
                // Reading SMTP section from appsettings.json.
                string host = this.Configuration.GetValue<string>("Smtp:Server");
                int port = this.Configuration.GetValue<int>("Smtp:Port");
                bool enableSsl = this.Configuration.GetValue<bool>("Smtp:EnableSsl");
                bool defaultCredentials = this.Configuration.GetValue<bool>("Smtp:DefaultCredentials");
                string from = this.Configuration.GetValue<string>("Smtp:From");
                string userName = this.Configuration.GetValue<string>("Smtp:Username");
                string password = this.Configuration.GetValue<string>("Smtp:Password");

                string subject = "Recovery password";
                string body = string.Format("Your password is : <b>{0}</b>", userPassword);
                // Send Recovery password Email.
                objBL.SendEmail(emailId, subject, body, host, port, enableSsl, defaultCredentials, from, userName, password);

                TempData["RegMessage"] = "Password sent in your registered email.";
                return RedirectToAction("GetLogin");
            }
        }

        public IActionResult GetSingleEmpFeedback()
        {
            // Gets FeedbackRatings.
            List<FeedbackRating> feedbackRatings = objBL.GetFeedbackRatings();
            // Gets current LoggedIn user id.
            int id = Convert.ToInt32(HttpContext.Session.GetString("Curr"));
            // Gets Userid based on LoggedIn userId.
            string usr = objBL.GetVendorDropDownList2().FirstOrDefault(x => x.Id == id).UserID;
            // Count no of feed back based on LoggedIn userId.
            int noOfUserFeedBack = feedbackRatings.Where(x => x.ToID == usr).Select(x => x.ByID).Distinct().Count();
            if (noOfUserFeedBack == 0)
            {
                ViewBag.InCompleteMessage = "No record found.";
            }
            else if (noOfUserFeedBack >= 4)
            {
                // Calculate Feedback average.
                double average = feedbackRatings.Where(x => x.ToID == usr).Select(x => x.Rating).Average();
                ViewBag.Rating = average;
            }
            else
            {
                ViewBag.InCompleteMessage = string.Format("Feedback rating is not yet completed. No of pending feedback provider are {0}.", (4 - noOfUserFeedBack));
            }
            return View();
        }

        public IActionResult GetAllEmpFeedback()
        {
            // Gets FeedbackRatings.
            List<FeedbackRating> feedbackRatings = objBL.GetFeedbackRatings();
            // Gets lists of ToId.
            List<string> toIds = feedbackRatings.Select(x => x.ToID).Distinct().ToList();
            List<SelectListItem> users = new List<SelectListItem>();
            foreach (string toId in toIds)
            {
                // Count no of feedback provided.
                int noOfUserFeedBack = feedbackRatings.Where(x => x.ToID == toId).Select(x => x.ByID).Distinct().Count();
                if (noOfUserFeedBack >= 4)
                {
                    //decimal rating = feedbackRatings.Where(x => x.ToID == toId).Select(x => x.Rating).Sum();
                    //decimal totalRating = feedbackRatings.Where(x => x.ToID == toId).Select(x => x.Rating).Count();
                    //decimal average = Math.Round((rating / totalRating), 1);
                    // Calculate Feedback Average.
                    double average = feedbackRatings.Where(x => x.ToID == toId).Select(x => x.Rating).Average();
                    // Setting average value.
                    users.Add(new SelectListItem { Text = toId, Value = average.ToString() });
                }
                else if (noOfUserFeedBack > 0 && noOfUserFeedBack < 4)
                {
                    // Sets Average Status to Pending.
                    users.Add(new SelectListItem { Text = toId, Value = "Pending" });
                }
            }

            ViewBag.Users = users;
            return View();
        }

        public IActionResult GetCriteria()
        {
            // Gets userdetails except External.
            List<UserDetail> vendorDisplays2 = objBL.GetVendorDropDownList2().Where(x => x.DesignID != "16").ToList();
            ViewBag.VendorDisplay2 = vendorDisplays2;
            return View();
        }

        [HttpPost]
        public JsonResult PopulateHigherDesignations(int designId)
        {
            // gets Designation less than DesignId.
            List<Designation> designations = objBL.GetVendorDropDownList1().Where(x => x.DesignID < designId).ToList();
            return Json(designations);
        }

        [HttpPost]
        public JsonResult GetRatings(string userID)
        {
            //List<UserDetail> vendorDisplays2 = objBL.GetVendorDropDownList2().Where(x => x.DesignID != "16").ToList();
            //ViewBag.VendorDisplay2 = vendorDisplays2;
            List<RatingModel> ratings = new List<RatingModel>();
            // Gets feedbackRatings based on UserId.
            List<FeedbackRating> feedbackRatings = objBL.GetFeedbackRatings().Where(x => x.ToID == userID).ToList();
            if (feedbackRatings.Select(x => x.ByID).Distinct().Count() >= 4)
            {
                // Gets Distint FeedbackRating.
                List<int> qIds = feedbackRatings.Select(x => x.QID).Distinct().ToList();
                foreach (int qId in qIds)
                {
                    // Gets Question Id.
                    AddQuestion question = objBL.GetDetail().FirstOrDefault(x => x.Qid == qId);
                    // Calculate average.
                    double average = feedbackRatings.Where(x => x.QID == qId).Select(x => x.Rating).Average();
                    // Adding rating to Generic List collection.
                    ratings.Add(new RatingModel
                    {
                        Id = qId,
                        Description = question.QDescription,
                        Rating = average.ToString()
                    });
                }
            }
            return Json(ratings);
        }

        [HttpPost]
        public IActionResult GetCriteria(string UserID, int DesignID)
        {
            // Gets UserDetail except External.
            List<UserDetail> vendorDisplays2 = objBL.GetVendorDropDownList2().Where(x => x.DesignID != "16").ToList();
            ViewBag.VendorDisplay2 = vendorDisplays2;
            List<RatingModel> ratings = new List<RatingModel>();
            List<int> qualified = new List<int>();
            // Gets FeedbackRatings based on UserId.
            List<FeedbackRating> feedbackRatings = objBL.GetFeedbackRatings().Where(x => x.ToID == UserID).ToList();
            if (feedbackRatings.Select(x => x.ByID).Distinct().Count() >= 4)
            {
                // Gets Distinct QuestionId.
                List<int> qIds = feedbackRatings.Select(x => x.QID).Distinct().ToList();
                // Gets RatingCriteria based on DesignID.
                List<RatingCriteria> ratingCriterias = objBL.GetRatingCriterias().Where(x => x.DesignationID == DesignID).ToList();

                foreach (int qId in qIds)
                {
                    // Gets AddQuestion based on Question Id.
                    AddQuestion question = objBL.GetDetail().FirstOrDefault(x => x.Qid == qId);
                    // Gets Rating.
                    double rating = Convert.ToDouble(ratingCriterias.Where(x => x.QId == qId).FirstOrDefault().Rating);
                    // Calculate Average.
                    double average = feedbackRatings.Where(x => x.QID == qId).Select(x => x.Rating).Average();
                    qualified.Add(average >= rating ? 1 : 0);
                    // Adding rating to Generic List collection.
                    ratings.Add(new RatingModel
                    {
                        Id = qId,
                        Description = question.QDescription,
                        Rating = average.ToString()
                    });
                }
            }
            ViewBag.IsQualified = qualified.Contains(0) ? false : true;
            return View(ratings);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePassword cp)
        {
            // Gets current logged in user.
            int id = Convert.ToInt32(HttpContext.Session.GetString("Curr"));
            // Gets UserDetail based on Logged in user Id.
            UserDetail usr = objBL.GetVendorDropDownList2().FirstOrDefault(x => x.Id == id);
            // Old password does not match
            if (usr.Password != cp.OldPassword)
            {
                ViewBag.ErrorInfo = "Old password does not match with our record.";
            }
            else
            {
                // Update new password.
                usr.Password = cp.Password;
                string msg = objBL.UpdatePassword(usr);
                if (msg == "success")
                {
                    return RedirectToAction("GetLogin");
                }

                ViewBag.ErrorInfo = "Data not saved, try again latter";
            }
            return View();
        }

        [HttpPost]
        public ActionResult SaveFeedback(List<FeedbackRating> questions)
        {
            // Gets current Logged In userId from Session.
            int userId = Convert.ToInt32(HttpContext.Session.GetString("Curr"));
            // Gets UserID based on current Logged In userId.
            string byID = objBL.GetVendorDropDownList2().FirstOrDefault(x => x.Id == userId).UserID;
            // Inserts FeedbackRatings.
            string messageInsert = objBL.InsertFeedbackRatings(questions, byID);
            if (messageInsert == "success")
            {
                // Updated FeedbackSchedules.
                objBL.UpdateFeedbackFeedbackSchedules(questions[0].ToID, byID);
            }
            return Json(messageInsert == "success" ? "Feedback rating has been saved." : "Error while saving Feedback rating.");
        }
    }
}
