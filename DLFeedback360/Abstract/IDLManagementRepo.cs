using DLFeedback360.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedback360.Abstract
{
    public interface IDLManagementRepo
    {
        UserDetail GetLogin(string userID, string password);
        string UpdatePassword(UserDetail usr);
        string InsertProdData(UserDetail usr);
        string SendEmail(string email, string subject, string body, string host, int port, bool enableSsl, bool defaultCredentials, string from, string userName, string password);
        string GetPasswordByEmail(string email);

        List<Role> GetRoleDropDownList1();


        List<AddQuestion> GetDetail();

        List<Category> GetCatDropDownList1();
        List<Role> GetVendorDropDownList();
        List<UserDetail> GetVendorDropDownList2();
        //UserDetail GetLogin2(string userID, string password);
        string InsertFeedbackDetails(FeedbackSchedule fdb);

        List<FeedbackSchedule> GetFeedbackDetails();

        string InsertQuestion(AddQuestion adq);

        string InsertFeedbackRatings(List<FeedbackRating> fdbRating, string byId);

        string UpdateFeedbackFeedbackSchedules(string emp, string provider);

        List<FeedbackRating> GetFeedbackRatings();

        List<RatingCriteria> GetRatingCriterias();
    }
}
