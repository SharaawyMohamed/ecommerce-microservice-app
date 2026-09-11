using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Features.Product.Queries.GetProductByName
{
    public class GetProductByNameValidator:AbstractValidator<GetProductByNameQuery>
    {
        public GetProductByNameValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
