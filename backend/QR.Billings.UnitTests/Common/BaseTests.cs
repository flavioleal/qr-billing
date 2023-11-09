using Moq.AutoMock;
using QR.Billings.Business.Interfaces.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.UnitTests.Common
{
    public class BaseTests
    {
        public void MockCurrentUser(AutoMocker mocker)
        {
            var usuario = mocker.GetMock<ICurrentUser>();
            usuario.Setup(x => x.Id).Returns(Guid.NewGuid());
            usuario.Setup(x => x.Name).Returns("Unit test");
            usuario.Setup(x => x.Role).Returns("admin");
        }
    }
}
