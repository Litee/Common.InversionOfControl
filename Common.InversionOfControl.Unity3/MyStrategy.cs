using Microsoft.Practices.ObjectBuilder2;

namespace Common.InversionOfControl.Unity3
{
    internal class MyStrategy : IBuilderStrategy
    {
        public void PreBuildUp(IBuilderContext context)
        {
            context.Policies.ClearDefault(typeof (IConstructorSelectorPolicy));
            context.Policies.SetDefault<IConstructorSelectorPolicy>(new MyDefaultConstructorSelectorPolicy());
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