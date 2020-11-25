// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Reflection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable enable

namespace Microsoft.EntityFrameworkCore.Metadata.Internal
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class InternalServicePropertyBuilder : InternalPropertyBaseBuilder<ServiceProperty>, IConventionServicePropertyBuilder
    {
        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public InternalServicePropertyBuilder([NotNull] ServiceProperty property, [NotNull] InternalModelBuilder modelBuilder)
            : base(property, modelBuilder)
        {
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public new virtual InternalServicePropertyBuilder? HasField([CanBeNull] string? fieldName, ConfigurationSource configurationSource)
            => (InternalServicePropertyBuilder?)base.HasField(fieldName, configurationSource);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public new virtual InternalServicePropertyBuilder? HasField(
            [CanBeNull] FieldInfo? fieldInfo,
            ConfigurationSource configurationSource)
            => (InternalServicePropertyBuilder?)base.HasField(fieldInfo, configurationSource);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public new virtual InternalServicePropertyBuilder? UsePropertyAccessMode(
            PropertyAccessMode? propertyAccessMode,
            ConfigurationSource configurationSource)
            => (InternalServicePropertyBuilder?)base.UsePropertyAccessMode(propertyAccessMode, configurationSource);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual InternalServicePropertyBuilder? HasParameterBinding(
            [CanBeNull] ServiceParameterBinding? parameterBinding,
            ConfigurationSource configurationSource)
        {
            if (CanSetParameterBinding(parameterBinding, configurationSource))
            {
                Metadata.SetParameterBinding(parameterBinding, configurationSource);
                return this;
            }

            return null;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual bool CanSetParameterBinding(
            [CanBeNull] ServiceParameterBinding? parameterBinding,
            ConfigurationSource? configurationSource)
            => configurationSource.Overrides(Metadata.GetParameterBindingConfigurationSource())
                || (Metadata.ParameterBinding == parameterBinding);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual InternalServicePropertyBuilder? Attach([NotNull] InternalEntityTypeBuilder entityTypeBuilder)
        {
            var newPropertyBuilder = entityTypeBuilder.ServiceProperty(
                Metadata.GetIdentifyingMemberInfo(), Metadata.GetConfigurationSource());
            if (newPropertyBuilder == null)
            {
                return null;
            }

            newPropertyBuilder.MergeAnnotationsFrom(Metadata);

            var oldParameterBindingConfigurationSource = Metadata.GetParameterBindingConfigurationSource();
            if (oldParameterBindingConfigurationSource.HasValue)
            {
                newPropertyBuilder.HasParameterBinding(Metadata.ParameterBinding, oldParameterBindingConfigurationSource.Value);
            }

            var oldFieldInfoConfigurationSource = Metadata.GetFieldInfoConfigurationSource();
            if (oldFieldInfoConfigurationSource.HasValue
                && newPropertyBuilder.CanSetField(Metadata.FieldInfo, oldFieldInfoConfigurationSource))
            {
                newPropertyBuilder.HasField(Metadata.FieldInfo, oldFieldInfoConfigurationSource.Value);
            }

            return newPropertyBuilder;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        IConventionPropertyBase IConventionPropertyBaseBuilder.Metadata
            => Metadata;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        IConventionServiceProperty IConventionServicePropertyBuilder.Metadata
            => Metadata;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        IConventionPropertyBaseBuilder? IConventionPropertyBaseBuilder.HasField(string? fieldName, bool fromDataAnnotation)
            => HasField(fieldName, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        IConventionPropertyBaseBuilder? IConventionPropertyBaseBuilder.HasField(FieldInfo? fieldInfo, bool fromDataAnnotation)
            => HasField(fieldInfo, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        IConventionServicePropertyBuilder? IConventionServicePropertyBuilder.HasField(string? fieldName, bool fromDataAnnotation)
            => HasField(fieldName, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        IConventionServicePropertyBuilder? IConventionServicePropertyBuilder.HasField(FieldInfo? fieldInfo, bool fromDataAnnotation)
            => HasField(fieldInfo, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        bool IConventionPropertyBaseBuilder.CanSetField(string? fieldName, bool fromDataAnnotation)
            => CanSetField(fieldName, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        bool IConventionPropertyBaseBuilder.CanSetField(FieldInfo? fieldInfo, bool fromDataAnnotation)
            => CanSetField(fieldInfo, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        IConventionPropertyBaseBuilder? IConventionPropertyBaseBuilder.UsePropertyAccessMode(
            PropertyAccessMode? propertyAccessMode,
            bool fromDataAnnotation)
            => UsePropertyAccessMode(
                propertyAccessMode, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        IConventionServicePropertyBuilder? IConventionServicePropertyBuilder.UsePropertyAccessMode(
            PropertyAccessMode? propertyAccessMode,
            bool fromDataAnnotation)
            => UsePropertyAccessMode(
                propertyAccessMode, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        bool IConventionPropertyBaseBuilder.CanSetPropertyAccessMode(PropertyAccessMode? propertyAccessMode, bool fromDataAnnotation)
            => CanSetPropertyAccessMode(
                propertyAccessMode, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        IConventionServicePropertyBuilder? IConventionServicePropertyBuilder.HasParameterBinding(
            ServiceParameterBinding? parameterBinding,
            bool fromDataAnnotation)
            => HasParameterBinding(
                parameterBinding, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        bool IConventionServicePropertyBuilder.CanSetParameterBinding(ServiceParameterBinding? parameterBinding, bool fromDataAnnotation)
            => CanSetParameterBinding(
                parameterBinding, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);
    }
}
