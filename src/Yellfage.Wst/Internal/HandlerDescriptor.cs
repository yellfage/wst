using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Yellfage.Wst.Filters;

namespace Yellfage.Wst.Internal
{
    internal class HandlerDescriptor
    {
        public string Name { get; }
        public MethodInfo MethodInfo { get; }
        public MethodExecutor MethodExecutor { get; }
        public Type WorkerType { get; }
        public ParameterDescriptor[] ParameterDescriptors { get; }
        public IList<IInvocationFilter> Filters { get; }

        public HandlerDescriptor(
            string name,
            MethodInfo methodInfo,
            MethodExecutor methodExecutor,
            Type workerType,
            IList<IInvocationFilter> filters)
        {
            Name = name;
            MethodInfo = methodInfo;
            MethodExecutor = methodExecutor;
            WorkerType = workerType;
            ParameterDescriptors = SelectParameterDescriptors(methodInfo);
            Filters = filters;
        }

        private ParameterDescriptor[] SelectParameterDescriptors(MethodInfo info)
        {
            return info.GetParameters().Select(info => new ParameterDescriptor(info)).ToArray();
        }
    }
}
