using System;
using HoldTheAllergen.Data.Models;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace HoldTheAllergen.Backend.Core.Dependency
{
    public class StructureMapCommandMessageConvention : IRegistrationConvention
    {
        #region IRegistrationConvention Members

        public void Process(Type type, Registry registry)
        {
            if (!type.ImplementsInterfaceTemplate(typeof (IFormHandler<>))) return;

            var interfaceType = type.FindFirstInterfaceThatCloses(typeof (IFormHandler<>));
            var commandMessageType = interfaceType.GetGenericArguments()[0];

            var openCommandMethodResultType = typeof (FormActionResult<>);
            var closedCommandMethodResultType = openCommandMethodResultType.MakeGenericType(commandMessageType);

            var openActionMethodInvokerType = typeof (IActionMethodResultInvoker<>);
            var closedActionMethodInvokerType =
                openActionMethodInvokerType.MakeGenericType(closedCommandMethodResultType);

            var openCommandMethodResultInvokerType = typeof (CommandMethodResultInvoker<>);
            var closedCommandMethodResultInvokerType =
                openCommandMethodResultInvokerType.MakeGenericType(commandMessageType);

            registry.For(closedActionMethodInvokerType).Use(closedCommandMethodResultInvokerType);
        }

        #endregion
    }
}