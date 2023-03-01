using BasePackageModule2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

namespace TomBase.Helpers
{
    public class EmailOrderManager
    {
        #region orders
        public EmailTemplateData GetOrderTemplate(string DomainUrl, EnumOrdersEmailTemplate EnumOrdersEmailTemplate, Order Order, string DomainURL, string PaymentType)
        {
            EmailTemplateData EmailTemplateData = null;

            switch (EnumOrdersEmailTemplate)
            {
                case EnumOrdersEmailTemplate.Consumer:
                    EmailTemplateData = ReadOrdersTemplate(DomainUrl, EnumOrdersEmailTemplate.Consumer);
                    break;
                case EnumOrdersEmailTemplate.Retailer:
                    EmailTemplateData = ReadOrdersTemplate(DomainUrl, EnumOrdersEmailTemplate.Retailer);
                    break;
                default:
                    EmailTemplateData = new EmailTemplateData();
                    break;
            }

            StringBuilder StringBuilder = new StringBuilder(EmailTemplateData.Subject);

            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-NAME}}", Order.User.FullName);
            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-EMAIL}}", Order.User.Email);
            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-NUMBER}}", Order.User.PhoneNumber);

            EmailTemplateData.Subject = StringBuilder.ToString();

            StringBuilder = new StringBuilder(EmailTemplateData.Content);
            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-NAME}}", Order.User.FullName);
            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-EMAIL}}", Order.User.Email);
            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-NUMBER}}", Order.User.PhoneNumber);
            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-PINCODE}}", Order.Address.PinCode.ToString());
            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-AMOUNT}}", Order.Amount.ToString());
            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-LOCALITY}}", Order.Address.Locality);
            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-CITY}}", Order.Address.City);
            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-MOBILENUMBER}}", Order.Address.MobileNumber);
            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-PAYMENTSTATUS}}", Order.PaymentStatus);
            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-ID}}", Order.Id.ToString());
            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-ORDERTYPE}}", PaymentType);




            string OrderTable = @" <table>
                                                    <th>OrderId</th>
                                                    <th>Image</th>
                                                     <th>Product</th>
                                                    <th>Quantity</th>
                                                    {{TABLE-ORDER-DATA}}                                    
                                                 </table>";
            StringBuilder OrderData = new StringBuilder();

            foreach (var item in Order.OrderProducts)
            {
                var uri = new Uri(DomainURL + item.Product.Image);
                OrderData.Append("<tr>");
                OrderData.Append("<td> " + item.OrderId + " </td>");
                OrderData.Append("<td> <img src='" + uri + "'  style='height: 100px; ' />  </td>");
                //OrderData.Append("<td> <img src='https://localhost:5001/img/products/7d8e3226-a1a9-4587-8c69-c6ed088d2d0e.png' />  </td>");
                OrderData.Append("<td> " + item.Product.Name + " </td>");
                OrderData.Append("<td> " + item.Quantity + " </td>");
                OrderData.Append("</tr>");
            }

            OrderTable = OrderTable.Replace("{{TABLE-ORDER-DATA}}", OrderData.ToString());
            StringBuilder = StringBuilder.Replace("{{CONSUMER-ORDER-TABLE}}", OrderTable);

            EmailTemplateData.Content = StringBuilder.ToString();

            return EmailTemplateData;
        }



        private EmailTemplateData ReadOrdersTemplate(string DomainUrl, EnumOrdersEmailTemplate EnumOrdersEmailTemplate)
        {
            string Url = null;
            switch (EnumOrdersEmailTemplate)
            {
                case EnumOrdersEmailTemplate.Consumer:
                    Url = DomainUrl + @"\EmailTemplate\NewOrderPlacedConsumer.json";
                    break;
                case EnumOrdersEmailTemplate.Retailer:
                    Url = DomainUrl + @"\EmailTemplate\NewOrderPlacedRetail.json";
                    break;
                default:
                    Url = "";
                    break;
            }
            string json = FileReader.ReadFile(Url);

            return JsonConvert.DeserializeObject<EmailTemplateData>(json);
        }
        #endregion
    }
}
