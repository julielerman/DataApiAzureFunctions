using System.Net;
using System.Text.RegularExpressions;
using  System.Net.Http.Headers;
public static HttpResponseMessage Run(HttpRequestMessage req, out object logDocument, TraceWriter log)
{
    string message = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "message", true) == 0)
        .Value;
     Regex rgx = new Regex(@"^\d{10}$");
    if (rgx.IsMatch(message))
    {   
       logDocument = new {
        phone = message,
        logged=System.DateTime.Now
      };
     var resp= new HttpResponseMessage(HttpStatusCode.OK);
     resp.Content=new StringContent("<html><H1>You're subscribed!</H1></html>");
     resp.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
    return resp;
    }
    else {
        logDocument=null;
       var resp= new HttpResponseMessage(HttpStatusCode.BadRequest);
      resp.Content=new StringContent("<html><H1>Bad data. Try again. 10 digits only</H1></html>");
    // resp.Content=new StringContent("<p>Bad data. Try again. 10 digits only</p>");
     resp.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
     return resp;
     }
}