﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using RapidCMS.Core.Abstractions.Data;
using RapidCMS.Core.Abstractions.Metadata;
using RapidCMS.Core.Abstractions.Validators;
using RapidCMS.Core.Extensions;
using RapidCMS.Core.Forms.Validation;

namespace RapidCMS.Core.Validators
{
    internal class CollectionDataValidator : IRelationValidator
    {
        private readonly IPropertyMetadata _property;

        public CollectionDataValidator(IPropertyMetadata property)
        {
            _property = property;
        }

        public IEnumerable<ValidationResult> Validate(IEntity entity, IEnumerable<IElement> relatedElements, IServiceProvider serviceProvider)
        {
            var validationAttributes = _property.GetAttributes<RelationValidationAttribute>();
            var validationContext = new ValidationContext(entity, serviceProvider, default)
            {
                MemberName = _property.PropertyName
            };

            return validationAttributes
                .Select(attr => attr.IsValid(entity, relatedElements, validationContext))
                .SelectNotNull(result => result as ValidationResult);
        }
    }
}
