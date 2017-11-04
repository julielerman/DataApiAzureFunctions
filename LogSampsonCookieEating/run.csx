using System.Net;

public static HttpResponseMessage Run(HttpRequestMessage req, out object logDocument, TraceWriter log)
{

    string message = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "message", true) == 0)
        .Value;

    string date = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "date", true) == 0)
        .Value;

    logDocument = new {
        message = message,
        date = date.ToString(),
        logged=System.DateTime.Now
    };

    if (message != "" && date != "") {
        return req.CreateResponse(HttpStatusCode.OK);
    }
    else {
        return req.CreateResponse(HttpStatusCode.BadRequest);
    }
}