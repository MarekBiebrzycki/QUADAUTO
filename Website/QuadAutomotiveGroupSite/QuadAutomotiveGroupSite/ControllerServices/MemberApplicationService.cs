using System;
using MassTransit;
using QuadAuto.Web.Controllers.Committees.Commands;
using QuadAuto.Web.Controllers.Members.Commands;
using Xamarin.Payments.Stripe;

namespace QuadAutomotiveGroupSite.ControllerServices
{
    public class MemberApplicationService
    {
        private IServiceBus _bus;

        public MemberApplicationService(IServiceBus bus)
        {
            _bus = bus;
        }

        public string ProcessPayment(string FirstName, string LastName, string EmailAddress, string CardNumber, string ExpMonth, string ExpYear, string Cvv)
        {
            StripePayment payment = new StripePayment("OxGcTunKYwFuBr6JPDpX1mehWlXHIJ7k");
            StripeCustomerInfo customer = new StripeCustomerInfo
                                              {
                                                  Email = EmailAddress,
                                                  Card =
                                                      new StripeCreditCardInfo
                                                          {
                                                              Number = CardNumber,
                                                              ExpirationMonth = Convert.ToInt32(ExpMonth),
                                                              ExpirationYear = Convert.ToInt32(ExpYear),
                                                              FullName = FirstName + " " + LastName
                                                          }
                                              };
            StripeCustomer response = payment.CreateCustomer(customer);
            string customerId = response.ID;
            return payment.Charge(2500, "usd", customerId, "QuadAutomotive Group Application Fee").ID;            
        }
        public void SubmitApplication(string FirstName, string LastName, string EmailAddress, string BirthDate, string HowWillIContributeToTheProject, string SkillsBackground, string transactionId)
        {
            var m = new SubmitMemberApplication(Guid.NewGuid(), transactionId, DateTime.Parse(BirthDate), EmailAddress, FirstName,
                                                HowWillIContributeToTheProject, LastName, SkillsBackground);
            _bus.Publish(m);
        }
    }
}