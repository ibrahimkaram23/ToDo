using ToDo.Domain.Models;
using ToDo.Domain.Specifications;

namespace ToDo.Application.Specification.RefreshTokenSpecification
{
    internal class RefreshTokenByUserIdSpecification : BaseSpecifications<RefreshToken>
    {
        public RefreshTokenByUserIdSpecification(string userId)
        {
            //AddCriteria(x => x.UserId == userId);
        }
    }
}
