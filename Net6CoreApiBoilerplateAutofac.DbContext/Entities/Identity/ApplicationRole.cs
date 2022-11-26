using Microsoft.AspNetCore.Identity;
using Net6CoreApiBoilerplateAutofac.Infrastructure.DbUtility;

namespace Net6CoreApiBoilerplateAutofac.DbContext.Entities.Identity
{
    public class ApplicationRole : IdentityRole<long>, IIdentityEntity
    {
        public ApplicationRole() { }
        public ApplicationRole(string name) { Name = name; }
    }
}
