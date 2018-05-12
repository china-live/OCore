using System.Collections.Generic;
using System.Threading.Tasks;

namespace XCore.Deployment
{
    public interface IDeploymentTargetProvider
    {
        Task<IEnumerable<DeploymentTarget>> GetDeploymentTargetsAsync();
    }
}
