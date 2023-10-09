using ToDo.Domain.Models;
using ToDo.Domain.Specifications;

namespace ToDo.Application.Specification.RefreshTokenSpecification
{
    internal class RefreshTokenByTokenSpecification : BaseSpecifications<RefreshToken>
    {

        public RefreshTokenByTokenSpecification(string token)
        {
            AddCriteria(x => x.Token == token);

            //AddInclude(x => x.User);
        }
    }
}
