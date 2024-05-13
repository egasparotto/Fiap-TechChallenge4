using FiapReservas.Domain.Interfaces.Services.SMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace FiapReservas.Domain.Services.SMS
{
    public class SmsService : ISmsService
    {

        public void EnviarMensagem(string telefone, string mensagem)
        {
            const string accountSid = "AC794e8d984b16774de5340d74c8703836";
            const string authToken = "d967ea563e1382e3f0da1433bb0d5ad6";

            TwilioClient.Init(accountSid, authToken);

            MessageResource.Create(
                from: new PhoneNumber("+12693906071"),
                to: new PhoneNumber(telefone),
                body: mensagem);
        }
    }
}
