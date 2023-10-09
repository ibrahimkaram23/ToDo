namespace ToDo.Application.DTOs
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; private set; }
        public string Error { get;  set; }
        public int TotalCount { get; set; }
        public object Data { get; set; }
        public ResponseModel(bool isSuccess)
        {
            IsSuccess = isSuccess;
            Message = isSuccess ? "success" : "an error occurred";
        }
    }
}
