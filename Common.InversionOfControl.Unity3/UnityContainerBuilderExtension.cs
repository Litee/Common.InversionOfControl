using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Common.InversionOfControl.Unity3
{
    public class UnityContainerBuilderExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Strategies.Add(new MyStrategy(), UnityBuildStage.TypeMapping);
        }
    }
}