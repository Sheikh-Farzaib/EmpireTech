using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Middleware
{
    public class Auth
    {
        public RequestDelegate _next;
        public Auth(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            var token = context.Session.GetString("JwToken");

                if (string.IsNullOrEmpty(token))
                {
                    context.Response.Redirect("/Login/Index");
                    return; // Stop further request processing
                }
            
        }
    }
}