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
public static void Run(IReadOnlyList<Document> input, TraceWriter log)
{
     var accountSid = Environment.GetEnvironmentVariable("TwilioSID");
    var authToken = Environment.GetEnvironmentVariable("TwilioToken");
    var twilio = new TwilioRestClient(accountSid, authToken);
    var callTo=Environment.GetEnvironmentVariable("Phone_to_call");
    var callFrom=Environment.GetEnvironmentVariable("Outgoing_phone_number");
    if (input != null && input.Count > 0)
    {
        log.Verbose("Documents modified " + input.Count);
        log.Verbose("First document Id " + input[0].Id);
       var phonelist=GetPhones(log);
       log.Verbose(phonelist.Count.ToString());
       foreach(var phonenumber in phonelist){
          var message = twilio.SendMessage(
             callFrom,
             phonenumber.phone,
            "Sampson just ate another cookie"
            );
          log.Verbose(phonenumber.phone.ToString());
        Task.Delay(1000).Wait();
       }
       log.Verbose(phonelist.Count().ToString());
}}
    public class PhoneNumber{
        public string phone {get;set;}
    }

public static List<PhoneNumber> GetPhones(TraceWriter log)
{                         
     string EndpointUrl = Environment.GetEnvironmentVariable("DbURL");
     string PrimaryKey = Environment.GetEnvironmentVariable("DbPK");
   log.Verbose("end point and pk set");
    var  client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
   log.Verbose("client set up");
    FeedOptions  queryOptions = new FeedOptions { MaxItemCount = -1 };
    IQueryable<PhoneNumber> phonelist = client.CreateDocumentQuery<PhoneNumber>(
                UriFactory.CreateDocumentCollectionUri("dataapidemo", "subscribers"),
                "select c.phone from c",
                queryOptions);
      return phonelist.ToList();
}


 
  

  
  
