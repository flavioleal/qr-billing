using Microsoft.AspNetCore.Http;
using QR.Billings.Business.Interfaces.CurrentUser;
using System.Data.SqlTypes;

namespace QR.Billings.CrossCutting.IoC.User
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public Guid? Id
        {
            get
            {
                var idString = _accessor.HttpContext.User?.Claims.FirstOrDefault(x => "id".Equals(x.Type.ToLower()))?.Value;
                if (Guid.TryParse(idString, out Guid guid))
                {
                    return guid;
                }


                return null;
            }
        }


        public string Name => _accessor.HttpContext.User?.Claims.FirstOrDefault(x => x.Type.ToLower().EndsWith("name"))?.Value;

        public string Role => _accessor.HttpContext.User?.Claims.FirstOrDefault(x => x.Type.ToLower().EndsWith("role"))?.Value;
    }
}
