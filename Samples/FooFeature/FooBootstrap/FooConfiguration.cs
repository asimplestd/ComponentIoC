namespace Asimple.ComponentIoC.FooFeature
{
    public class FooConfiguration
    {
        public static void ConfigureBuilder(IComponentContextBuilder b, IFooService3 fooService3, IFooService4 fooService4)
        {
            // RegisterType<Type>() - registration by type
            // SingleInstance() - InstantiationStrategy == InstancePerContext. Do not create instance per child context, only per context in which type is registered.
            b.RegisterType<FooService1>().As<IFooService1>().SingleInstance();

            // RegisterType<Type>() - registration by type
            // InstancePerDependency() - InstantiationStrategy == InstancePerDependency. Each 'resolve' will create instance of 'Type'
            b.RegisterType<FooService2>().As<IFooService2>().InstancePerDependency();

            // RegisterInstance<Type>() - registration instance.
            // InstantiationStrategy == InstancePerContext by default.
            b.RegisterInstance(fooService3).As<IFooService3>();

            // RegisterInstance<Type>() - registration instance.
            // .InstancePerDependency() will be ignored, because instance is single by definidion of this registration type
            b.RegisterInstance(fooService4).As<IFooService4>().InstancePerDependency();
        }
    }
}