using BW.Common.Enums;
using System;

namespace BW.Website.Common.Helpers
{
    /// <summary>
    /// ExceptionHelper
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// RaiseError
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        /// <param name="fromAPI"></param>
        /// <returns></returns>
        public static ErrorCodeEnum RaiseError(ErrorCodeEnum errorCode, string message)
        {
            switch (errorCode)
            {
                case ErrorCodeEnum.BAD_REQUEST:
                    throw new BWException(message, errorCode);
                case ErrorCodeEnum.FORBIDDEN:
                    throw new BWException();
                case ErrorCodeEnum.UNAUTHORIZED:
                    throw new BWException(message, errorCode, new UnauthorizedAccessException(message));             
                default:
                   break;
            }

            return errorCode;
        }
    }
}