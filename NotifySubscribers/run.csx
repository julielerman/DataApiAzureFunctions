#r "Microsoft.Azure.Documents.Client"
#r "Newtonsoft.Json"
using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Twilio;
using System.Net;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

//twilio.com/blog/2017/01/how-to-send-daily-sms-reminders-using-c-azure-functions-and-twilio.html
public static void Run(IReadOnlyList<Document> input, TraceWriter log, IEnumerable<dynamic> subscriberList)
{
    var accountSid = Environment.GetEnvironmentVariable("TwilioSID");
    var authToken = Environment.GetEnvironmentVariable("TwilioToken");
    var twilio = new TwilioRestClient(accountSid, authToken);
    var callTo=Environment.GetEnvironmentVariable("Phone_to_call");
    var callFrom=Environment.GetEnvironmentVariable("Outgoing_phone_number");
    if (input != null && input.Count > 0)
     {
    //     log.Verbose("Documents modified " + input.Count);
    //     log.Verbose("First document Id " + input[0].Id);
    //     log.Verbose(subscriberList.Count().ToString());
       foreach(var phonenumber in subscriberList){
           log.Verbose(phonenumber.phone.ToString());
           log.Verbose(callFrom);
           
          var message = twilio.SendMessage(
             callFrom,
             phonenumber.phone.ToString(),
            "Sampson just ate yet another cookie"
            );
          Task.Delay(1000).Wait();
       }
       //log.Verbose(subscriberList.Count().ToString());
}}
    public class PhoneNumber{
        public string phone {get;set;}
    }




 
  

  
  
