using XCore.Deployment;
using XCore.DisplayManagement.Handlers;
using XCore.DisplayManagement.Views;

namespace XCore.Features.Deployment
{
    public class AllFeaturesDeploymentStepDriver : DisplayDriver<DeploymentStep, AllFeaturesDeploymentStep>
    {
        public override IDisplayResult Display(AllFeaturesDeploymentStep step)
        {
            return
                Combine(
                    View("AllFeaturesDeploymentStep_Summary", step).Location("Summary", "Content"),
                    View("AllFeaturesDeploymentStep_Thumbnail", step).Location("Thumbnail", "Content")
                );
        }

        public override IDisplayResult Edit(AllFeaturesDeploymentStep step)
        {
            return View("AllFeaturesDeploymentStep_Edit", step).Location("Content");
        }
    }
}
