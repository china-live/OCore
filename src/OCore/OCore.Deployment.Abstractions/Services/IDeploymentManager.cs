using System.Collections.Generic;
using System.Threading.Tasks;

namespace OCore.Deployment.Services
{
    public interface IDeploymentManager
    {
        Task ExecuteDeploymentPlanAsync(DeploymentPlan deploymentPlan, DeploymentPlanResult result);

        Task<IEnumerable<DeploymentTarget>> GetDeploymentTargetsAsync();
    }
}
