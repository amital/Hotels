using System.Reflection;
using System.Runtime.InteropServices;
using Payoneer.Payoneer.Hotels.Model;
using Payoneer.ServicesInfra.Aspects;
using PostSharp.Extensibility;
using PubComp.Aspects.Monitoring;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Hotels.WebApi")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("241b561d-5a4f-49b4-adf7-2e2c0fcec631")]

// Aspects:
[assembly: SetOperationCorrelationId(AttributeExclude = true, AttributeTargetMembers = @"regex:get_.*|set_.*")]
[assembly: SetOperationCorrelationId(AttributeExclude = true,
    AttributeTargetElements = MulticastTargets.InstanceConstructor)]

[assembly: Log(AttributeExclude = true, AttributeTargetMembers = @"regex:get_.*|set_.*")]
[assembly: Log(AttributeExclude = true,
    AttributeTargetElements = MulticastTargets.InstanceConstructor)]

[assembly: SetOperationCorrelationId(
    AspectPriority = AspectPriorities.Correlation,
    AttributeExclude = false,
    AttributeTargetTypes = "Payoneer.Payoneer.Hotels.WebApi.Controllers.*",
    AttributeTargetElements = MulticastTargets.Method,
    AttributeTargetMemberAttributes =
        MulticastAttributes.Instance | MulticastAttributes.Public)]

[assembly: Log(
    enterExistLogLevel: LogLevelValue.Debug,
    doLogValuesOnEnterExit: true,
    AspectPriority = AspectPriorities.Log,
    AttributeExclude = false,
    AttributeTargetTypes = "Payoneer.Payoneer.Hotels.WebApi.Controllers.*",
    AttributeTargetElements = MulticastTargets.Method,
    AttributeTargetMemberAttributes =
        MulticastAttributes.Instance | MulticastAttributes.Public)]

[assembly: SetOperationCorrelationId(
    AspectPriority = AspectPriorities.Correlation,
    AttributeExclude = false,
    AttributeTargetTypes = "Payoneer.Payoneer.Hotels.WebApi.QueueHandlers.*",
    AttributeTargetElements = MulticastTargets.Method,
    AttributeTargetMemberAttributes =
        MulticastAttributes.Instance | MulticastAttributes.Public)]

[assembly: Log(
    enterExistLogLevel: LogLevelValue.Debug,
    doLogValuesOnEnterExit: true,
    AspectPriority = AspectPriorities.Log,
    AttributeExclude = false,
    AttributeTargetTypes = "Payoneer.Payoneer.Hotels.WebApi.QueueHandlers.*",
    AttributeTargetElements = MulticastTargets.Method,
    AttributeTargetMemberAttributes =
        MulticastAttributes.Instance | MulticastAttributes.Public)]
