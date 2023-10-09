using System.Text;

namespace ToDo.Application.Abstractions
{
    internal interface IValidation<T> where T : class
    {
        public StringBuilder Error { get; set; }

        Task Validate(T DTO);
    }
}
