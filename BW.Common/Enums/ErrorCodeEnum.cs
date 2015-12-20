namespace BW.Common.Enums
{
    /// <summary>
    /// ErrorCodeEnum
    /// </summary>
    public enum ErrorCodeEnum
    {
        SUCCESS = 0,
        FAILED = 1,
        EMAIL_EXIST = 2,
        EMAIL_NONEXIST = 3,
        BAD_REQUEST = 400,
        UNAUTHORIZED = 401,
        FORBIDDEN = 403,
        PAGE_NOT_FOUND = 404,
        REQUEST_TIMEOUT = 408,
        INTERNAL_SERVER_ERROR = 500,
        SERVICE_UNAVAILABE = 503,
    }
}