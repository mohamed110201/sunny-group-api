using Microsoft.AspNetCore.Mvc;

namespace API.Common.JSend;

public class JSendResponse: ObjectResult
{
    public JSendResponse(int statusCode,JSendBody? body=null) : base(body)
    {
        StatusCode = statusCode;
    }
    
}