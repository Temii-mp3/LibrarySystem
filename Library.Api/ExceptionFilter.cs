using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Library.Api
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case AccountNotFoundException:
                    context.Result = new ObjectResult(new
                    {
                        Error = "Account Not Found"
                    })
                    {
                        StatusCode = 404
                    };
                    context.ExceptionHandled = true;
                    break;

                case RoomNotFoundException:
               
                    context.Result = new ObjectResult(new
                    {
                        Error = "Room Not Found"
                    })
                    {
                        StatusCode = 404
                    };
                    context.ExceptionHandled = true;
                    break;

                case RoomBookedException:
                    context.Result = new ObjectResult(new
                    {
                        Error = "Room Booked"
                    })
                    {
                        StatusCode = 403
                    };
                    context.ExceptionHandled = true;
                    break;

                case BookBorrowedException:
                    context.Result = new ObjectResult(new
                    {
                        Error = "Book Borrowed"
                    })
                    {
                        StatusCode = 403
                    };
                    context.ExceptionHandled = true;
                    break;

                case BookNotFoundException:
                    context.Result = new ObjectResult(new
                    {
                        Error = "Book Not Found"
                    })
                    {
                        StatusCode = 404
                    };
                    context.ExceptionHandled = true;
                    break;

                case AccountExistsException:
                    context.Result = new ObjectResult(new
                    {
                        Error = "Account Already Exists"
                    })
                    {
                        StatusCode = 403
                    };
                    context.ExceptionHandled = true;
                    break;

                case LoginException:
                    context.Result = new ObjectResult(new
                    {
                        Error = "Invalid Email/Password/Username Format"
                    })
                    {
                        StatusCode = 403
                    };
                    context.ExceptionHandled = true;
                    break;

                case RoomLimitReachedException:
                    context.Result = new ObjectResult(new
                    {
                        Error = "Room Limit Reached"
                    })
                    {
                        StatusCode = 404
                    };
                    context.ExceptionHandled = true;
                    break;
                case BookLimitReachedException:
                    context.Result = new ObjectResult(new
                    {
                        Error = "Book Limit Reached"
                    })
                    {
                        StatusCode = 404
                    };
                    context.ExceptionHandled = true;
                    break;

                case NotLoggedInException:
                    context.Result = new ObjectResult(new
                    {
                        Error = "Not Logged in"
                    })
                    {
                        StatusCode = 404
                    };
                    context.ExceptionHandled = true;
                    break;

                default:
                    context.Result = new ObjectResult(new
                    {
                        Error = "Unknown Err Occurred"
                    })
                    {
                        StatusCode = 500
                    };
                    context.ExceptionHandled = true;
                    break;
            }
        }
    }
}
