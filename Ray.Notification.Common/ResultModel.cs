using System;

namespace Ray.Notification.Common
{
    public class ResultModel
    {
        private bool? _result;

        public ResultModel()
        {

        }

        public ResultModel(bool result, string message)
        {
            Result = result;
            Message = message;
        }

        public ResultModel(object value)
        {
            Value = value;
            Result = true;
            Message = string.Empty;
        }

        public ResultModel(object value, bool result, string message) : this(result, message)
        {
            Value = value;
        }

        public ResultModel(Exception err)
        {
            Result = false;
            Message = err?.Message;
        }

        public object Value { get; set; }

        public bool Result
        {
            get
            {
                if (_result != null)
                    return (bool)_result;
                if (ResultCode != null)
                    return ResultCode != 0;
                return false;
            }
            set => _result = value;
        }

        public int? ResultCode { get; set; }
        public string Message { get; set; }
    }
}
