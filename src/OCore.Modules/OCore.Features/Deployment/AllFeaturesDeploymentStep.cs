using OCore.Deployment;

namespace OCore.Features.Deployment
{
    /// <summary>
    /// Adds enabled and disabled features to a <see cref="DeploymentPlanResult"/>. 
    /// </summary>
    public class AllFeaturesDeploymentStep : DeploymentStep
    {
        public AllFeaturesDeploymentStep()
        {
            Name = "AllFeatures";
        }
    }
}
