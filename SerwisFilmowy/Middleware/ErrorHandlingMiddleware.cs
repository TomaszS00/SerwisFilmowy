﻿using SerwisFilmowy.Exceptions;

namespace SerwisFilmowy.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }

            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
            }

            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (System.Exception exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong!");
            }
        }
    }
}
