using PriceFeed.Domain.Exceptions.Base;
using System.Net;

namespace PriceFeed.Domain.Exceptions;

public class NotFoundException(string message) 
    : BaseException(
        message, 
        HttpStatusCode.NotFound) { }
