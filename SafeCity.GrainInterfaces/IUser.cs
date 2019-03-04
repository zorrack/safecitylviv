using System;
using SafeCity.Model;

namespace SafeCity.GrainInterfaces
{
    public interface IUser : IGrainWithItem<UserItem>
    {
    }
}
