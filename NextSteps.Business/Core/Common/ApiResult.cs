using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NextSteps.Business.Core.Enums;

namespace NextSteps.Business.Core.Common
{
    public class ApiResult
    {
        public static readonly ApiResult Empty = new();

        public List<ApiMessage> Messages { get; }

        public ApiResult()
        {
            Messages = new List<ApiMessage>();
        }

        public bool OK => !Messages.Any(x => x.Type == MessageTypeEnum.Error);

        public ApiResult AddSuccess(string message)
        {
            Messages.Add(ApiMessage.Create(message));
            return this;
        }

        public ApiResult AddInfo(string message)
        {
            Messages.Add(ApiMessage.CreateInfo(message));
            return this;
        }

        public ApiResult AddError(string message, string errorCode = null)
        {
            Messages.Add(ApiMessage.CreateError(message, errorCode));
            return this;
        }

        public ApiResult CopyMessages(ApiResult ApiResponse)
        {
            if (ApiResponse is null)
                throw new ArgumentNullException(nameof(ApiResponse));

            Messages.AddRange(ApiResponse.Messages);
            return this;
        }

        public override string ToString()
        {
            if (Messages.Count == 0)
                return string.Empty;

            var sb = new StringBuilder();
            Messages.ForEach(m => sb.AppendLine(m.Text));
            return sb.ToString();
        }
    }

    public class ApiResult<T> : ApiResult
    {
        public T Data { get; set; }

        public ApiResult()
        {
        }

        public ApiResult(T value)
        {
            Data = value;
        }

        public new ApiResult<T> CopyMessages(ApiResult ApiResponse)
        {
            base.CopyMessages(ApiResponse);
            return this;
        }

        public new ApiResult<T> AddSuccess(string message)
        {
            base.AddSuccess(message);
            return this;
        }

        public new ApiResult<T> AddInfo(string message)
        {
            base.AddInfo(message);
            return this;
        }

        public new ApiResult<T> AddError(string message, string errorCode = null)
        {
            base.AddError(message, errorCode);
            return this;
        }
    }
}