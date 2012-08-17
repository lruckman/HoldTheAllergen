using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HoldTheAllergen.Backend.Core.Helpers;

namespace HoldTheAllergen.Backend.Core.Providers
{
    public class MetadataConventionProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(
            IEnumerable<Attribute> attributes, Type containerType,
            Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes,
                                               containerType,
                                               modelAccessor,
                                               modelType,
                                               propertyName);

            if (metadata.DisplayName == null)
                metadata.DisplayName =
                    metadata.PropertyName.ToSeparatedWords();

            return metadata;
        }
    }
}