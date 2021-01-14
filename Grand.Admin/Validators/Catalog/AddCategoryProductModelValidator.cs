﻿using FluentValidation;
using Grand.Core;
using Grand.Domain.Customers;
using Grand.Core.Validators;
using Grand.Services.Catalog;
using Grand.Services.Localization;
using Grand.Admin.Extensions;
using Grand.Admin.Models.Catalog;
using System.Collections.Generic;

namespace Grand.Admin.Validators.Catalog
{
    public class AddCategoryProductModelValidator : BaseGrandValidator<CategoryModel.AddCategoryProductModel>
    {
        public AddCategoryProductModelValidator(
            IEnumerable<IValidatorConsumer<CategoryModel.AddCategoryProductModel>> validators,
            ILocalizationService localizationService, ICategoryService categoryService, IWorkContext workContext)
            : base(validators)
        {
            if (workContext.CurrentCustomer.IsStaff())
            {
                RuleFor(x => x).MustAsync(async (x, y, context) =>
                {
                    var category = await categoryService.GetCategoryById(x.CategoryId);
                    if (category != null)
                        if (!category.AccessToEntityByStore(workContext.CurrentCustomer.StaffStoreId))
                            return false;

                    return true;
                }).WithMessage(localizationService.GetResource("Admin.Catalog.Categories.Permisions"));
            }
        }
    }
}