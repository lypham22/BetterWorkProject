namespace BW.Common.Enums
{
    /// <summary>
    /// ErrorCodeEnum
    /// </summary>
    public enum ErrorCodeEnum
    {
        SUCCESS = 0,
        ACCEPTED = 1,
        FAILED = 2,
        EMAIL_EXIST = 3,
        EMAIL_NONEXIST = 4,
        BAD_REQUEST = 400,
        UNAUTHORIZED = 401,
        FORBIDDEN = 403,
        PAGE_NOT_FOUND = 404,
        REQUEST_TIMEOUT = 408,
        INTERNAL_SERVER_ERROR = 500,
        SERVICE_UNAVAILABE = 503,
    }
}