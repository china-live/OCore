using System.Collections.Generic;
using System.Threading.Tasks;

namespace XCore.Deployment.Services
{
    public interface IDeploymentManager
    {
        Task ExecuteDeploymentPlanAsync(DeploymentPlan deploymentPlan, DeploymentPlanResult result);

        Task<IEnumerable<DeploymentTarget>> GetDeploymentTargetsAsync();
    }
}
