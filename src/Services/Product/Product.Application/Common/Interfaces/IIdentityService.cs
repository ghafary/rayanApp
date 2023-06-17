using ProductApp.Application.Common.Models;

namespace ProductApp.Application.Common.Interfaces;

public interface IIdentityService
{
    string GetUserIdentity();

    string GetUserName();
}
