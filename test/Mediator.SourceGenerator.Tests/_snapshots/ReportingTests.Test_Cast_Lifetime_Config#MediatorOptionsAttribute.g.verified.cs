﻿//HintName: MediatorOptionsAttribute.g.cs
// <auto-generated>
//     Generated by the Mediator source generator.
// </auto-generated>

namespace Mediator
{
    /// <summary>
    /// Provide options for the Mediator source generator.
    /// </summary>
    [global::System.AttributeUsage(global::System.AttributeTargets.Assembly, AllowMultiple = false)]
    [global::System.CodeDom.Compiler.GeneratedCode("Mediator.SourceGenerator", "3.0.0.0")]
    public sealed class MediatorOptionsAttribute : global::System.Attribute
    {
        /// <summary>
        /// The namespace in which the Mediator implementation is generated.
        /// By default, the namespace is "Mediator".
        /// </summary>
        public string Namespace { get; set; } = "Mediator";

        /// <summary>
        /// The <see cref="global::Mediator.INotificationPublisher" /> type to use when publishing notifications.
        /// By default, the type is <see cref="global::Mediator.ForeachAwaitPublisher" />.
        /// </summary>
        public global::System.Type NotificationPublisherType { get; set; } = typeof(global::Mediator.ForeachAwaitPublisher);

        /// <summary>
        /// The default lifetime of the services registered in the DI container by the Mediator source generator.
        /// By default, the lifetime is <see cref="global::Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton" />.
        /// </summary>
        public global::Microsoft.Extensions.DependencyInjection.ServiceLifetime ServiceLifetime { get; set; } =
            global::Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton;
    }
}