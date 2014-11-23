using Microsoft.Practices.ObjectBuilder2;

namespace Common.InversionOfControl.Unity2
{
    internal class MyStrategy : BuilderStrategy
    {
        public override void PreBuildUp(IBuilderContext context)
        {
            var policyList = context.Policies;
            context.PersistentPolicies.ClearDefault(typeof(MyDefaultConstructorSelectorPolicy));
            policyList.ClearDefault(typeof(IConstructorSelectorPolicy));
            policyList.SetDefault<IConstructorSelectorPolicy>(new MyDefaultConstructorSelectorPolicy());
        }

        public void PostBuildUp(IBuilderContext context)
        {
            //throw new NotImplementedException();
        }

        public void PreTearDown(IBuilderContext context)
        {
            //throw new NotImplementedException();
        }

        public void PostTearDown(IBuilderContext context)
        {
            //throw new NotImplementedException();
        }
    }
}