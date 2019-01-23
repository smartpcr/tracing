using System.Linq;
using Castle.DynamicProxy;
using OpenTracing;

namespace aop
{
    public class OpenTracingUtil : IInterceptor
    {
        private readonly ITracer tracer;

        public OpenTracingUtil(ITracer tracer)
        {
            this.tracer = tracer;
        }

        public void Intercept(IInvocation invocation)
        {
            var method = $"{invocation.Method.DeclaringType}.{invocation.Method.Name}" + 
                $"({string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()))})";
            using (var span = tracer.BuildSpan(method).StartActive(true))
            {
                invocation.Proceed();
            }
        }
    }
}
