global using Rave.Models.MobileMoney;
global using Rave.Models.VirtualCard;
global using Rave.Models.Subaccount;
global using Rave.Models.Tokens;
global using Rave.Models;
global using Rave.Models.Charge;
global using Rave.Models.Account;
global using Rave.Models.Card;
global using Rave.Models.Validation;
using Rave;
using System.Diagnostics;
using ResponseData = Rave.Models.Account.ResponseData;

namespace ODS.Services.Domain
{
    public class PaymentService
    {
        private static string PbKey = "FLWPUBK_TEST-ac089c74f98cae65498954abcd767b3f-X";
        private static string SCKey = "";
        readonly RaveConfig raveConfig = new(PbKey, SCKey, false);
        readonly SystemDbContext context;
        public PaymentService(SystemDbContext context)
        {
            this.context = context;
        }
        public async Task<IResult> ProcessPayment(Payment request)
        {
            try
            {
                var mobileMoneyPayment = new ChargeMobileMoney(raveConfig);
                var payload = new MobileMoneyParams(PbKey, SCKey, request.FirstName, request.LastName, request.Email, 1055, "ZMW", request.Phone, request.PaymentMethod.ToDescriptionString(), "NG", "mobilemoneyzambia", request.TransactionRef);
                var chargeResponse = await mobileMoneyPayment.Charge(payload);
                Trace.WriteLine(chargeResponse.Data.ValidateInstructions.Instruction);
                Trace.WriteLine(chargeResponse.Data.ValidateInstructions.Valparams);
                Trace.WriteLine(chargeResponse.Data.ValidateInstruction);
                if(chargeResponse.Data.ChargedAmount>0)
                {
                    context.Set<Payment>().Add(request);
                    return Result.Success(chargeResponse.Message);
                }
                else
                {
                    return Result.Fail(chargeResponse.Message);
                }
                
            }
            catch (Exception)
            {
                return Result.Fail("An error occured, try again");
            }

        }

    }

}
