﻿using FluentValidation;
using Grand.Core.Validators;
using Grand.Services.Localization;
using Grand.Admin.Models.Messages;
using System.Collections.Generic;

namespace Grand.Admin.Validators.Messages
{
    public class ContactAttributeValueValidator : BaseGrandValidator<ContactAttributeValueModel>
    {
        public ContactAttributeValueValidator(
            IEnumerable<IValidatorConsumer<ContactAttributeValueModel>> validators,
            ILocalizationService localizationService)
            : base(validators)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Catalog.Attributes.ContactAttributes.Values.Fields.Name.Required"));
        }
    }
}