using System;
using SafeCity.GrainInterfaces;

namespace SafeCity.Model
{
    public class UserItem
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}