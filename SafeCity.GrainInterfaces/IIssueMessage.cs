using System;
using System.Threading.Tasks;
using SafeCity.Model;

namespace SafeCity.GrainInterfaces
{
    public interface IIssueMessage : IGrainWithItem<IssueMessageItem>
    {
    }
}
