using PriceFeed.Domain.Exceptions.Base;
using System.Net;

namespace PriceFeed.Domain.Exceptions;

public class BadRequestException(string message)
    : BaseException(
        message,
        HttpStatusCode.BadRequest) { }
