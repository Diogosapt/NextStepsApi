using NextSteps.Business.Core.Enums;
using System;

namespace NextSteps.Business.Core.Common
{
    public class ApiMessage : IEquatable<ApiMessage>
    {
        public string Code { get; set; }
        public string Text { get; set; }
        public MessageTypeEnum Type { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return obj is ApiMessage msg && Equals(msg);
        }

        public static ApiMessage Create(string message)
        {
            return Create(message, MessageTypeEnum.Success);
        }

        public static ApiMessage CreateError(string message, string errorCode = null)
        {
            return Create(message, MessageTypeEnum.Error, errorCode);
        }

        public static ApiMessage CreateInfo(string message)
        {
            return Create(message, MessageTypeEnum.Info);
        }

        public static ApiMessage Create(string message, MessageTypeEnum type, string code = null)
        {
            return new ApiMessage()
            {
                Text = message ?? throw new ArgumentNullException(nameof(message)),
                Type = type,
                Code = code
            };
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Text, Type).GetHashCode();
        }

        public static bool operator ==(ApiMessage left, ApiMessage right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ApiMessage left, ApiMessage right)
        {
            return !(left == right);
        }

        public bool Equals(ApiMessage other)
        {
            if (other == null)
                return false;

            return
                other.Text == Text &&
                other.Type == Type;
        }
    }
}