using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomBase.Models;

namespace TomBase.Helpers
{
    public class EmailContactUsManager
    {
        #region  ContactUs
        private EmailTemplateData ReadContactUsTemplate(string DomainUrl, EnumContactUsEmailTemplate EnumEmailTemplate)
        {
            string Url = null;
            switch (EnumEmailTemplate)
            {
                case EnumContactUsEmailTemplate.ContactUs:
                    Url = DomainUrl + @"\EmailTemplate\ContactUs.json";
                    break;
                case EnumContactUsEmailTemplate.ContactUsRetail:
                    Url = DomainUrl + @"\EmailTemplate\ContactUsRetail.json";
                    break;
                case EnumContactUsEmailTemplate.ContactUsWholesale:
                    Url = DomainUrl + @"\EmailTemplate\ContactUsWholesale.json";
                    break;
                default:
                    Url = "";
                    break;
            }
            string json = FileReader.ReadFile(Url);

            return JsonConvert.DeserializeObject<EmailTemplateData>(json);
        }
        public EmailTemplateData GetContactUsTemplate(string DomainUrl, int FormType, ContactUs ContactUs)
        {
            EmailTemplateData EmailTemplateData = null;
            switch (FormType)
            {
                case 1:
                    EmailTemplateData = ReadContactUsTemplate(DomainUrl, EnumContactUsEmailTemplate.ContactUs);
                    break;
                case 2:
                    EmailTemplateData = ReadContactUsTemplate(DomainUrl, EnumContactUsEmailTemplate.ContactUsRetail);
                    break;
                case 3:
                    EmailTemplateData = ReadContactUsTemplate(DomainUrl, EnumContactUsEmailTemplate.ContactUsWholesale);
                    break;
                case 4:
                    EmailTemplateData = ReadContactUsTemplate(DomainUrl, EnumContactUsEmailTemplate.ContactUsWholesale);
                    break;

                default:
                    EmailTemplateData = new EmailTemplateData();
                    break;
            }

            EmailTemplateData.Subject = EmailTemplateData.Subject.Replace("{{FROM-NAME}}", ContactUs.FullName);
            EmailTemplateData.Subject = EmailTemplateData.Subject.Replace("{{FROM-EMAIL}}", ContactUs.Email);
            EmailTemplateData.Subject = EmailTemplateData.Subject.Replace("{{FROM-NUMBER}}", ContactUs.Number);
            EmailTemplateData.Content = EmailTemplateData.Content.Replace("{{FROM-MESSAGE}}", ContactUs.Message);
            EmailTemplateData.Content = EmailTemplateData.Content.Replace("{{FROM-NAME}}", ContactUs.FullName);
            EmailTemplateData.Content = EmailTemplateData.Content.Replace("{{FROM-EMAIL}}", ContactUs.Email);
            EmailTemplateData.Content = EmailTemplateData.Content.Replace("{{FROM-NUMBER}}", ContactUs.Number);

            return EmailTemplateData;
        }
        #endregion
    }
}
