using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Jaeger;
using Jaeger.Samplers;

namespace aop
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Bootstrap();

            IWorker worker = container.Resolve<IWorker>();
            for(var i = 0; i < 10; i++)
            {
                worker.DoWork();
            }

            Console.WriteLine("Done!");

            Console.Read();
        }

        private static IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            var tracer = new Tracer.Builder("helloworld")
                .WithSampler(new ConstSampler(true))
                .Build();

            builder.Register(i => new OpenTracingUtil(tracer));
            builder.RegisterType<Worker>().As<IWorker>().EnableInterfaceInterceptors();
            var container = builder.Build();
            return container;
        }
    }
}
