using BW.Common.Enums;
using System.Runtime.Serialization;

namespace BW.Data.Contract
{
    public abstract class ResponeMessageBase<TEntity> where TEntity : class
    {
        public ErrorCodeEnum Code { get; set; }

        public string Message { get; set; }

        public TEntity Data { get; set; }
    }

    public class ResponeMessage<TEntity>
    {
        public ErrorCodeEnum Code { get; set; }

        public string Message { get; set; }

        public TEntity Data { get; set; }
    }

    public class ResponeMessageBaseType<TLoad>
    {
        public ErrorCodeEnum Code { get; set; }

        public string Message { get; set; }

        public TLoad Data { get; set; }
    }

    public class ResponeMessage
    {
        public ErrorCodeEnum Code { get; set; }

        public string Message { get; set; }
    }
}