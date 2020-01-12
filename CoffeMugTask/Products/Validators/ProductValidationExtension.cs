using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMugTask.Model;

namespace CoffeeMugTask.Products.Validators
{
    public static class ProductValidationExtension
    {
        public static bool IsProductToAddValid(this Product product)
        {
            if (!IsIdEmpty(product))
                return false;

            if (!IsNameValid(product))
                return false;

            if (!IsPriceValid(product))
                return false;

            return true;

        }

        public static bool IsProductToUpdateValid(this Product product)
        {
            if (IsIdEmpty(product))
                return false;

            if (!IsNameValid(product))
                return false;

            if (!IsPriceValid(product))
                return false;

            return true;
        }

        public static bool IsProductToDeleteValid(this Product product)
        {
            if (IsIdEmpty(product))
                return false;

            return true;
        }

        private static bool IsNameValid(Product product)
        {
            if (product.Name is null || product.Name.Length < 1 || product.Name.Length > 100)
                return false;

            return true;
        }

        private static bool IsPriceValid(Product product)
        {
            if (product.Price == decimal.Zero)
                return false;

            return true;
        }

        private static bool IsIdEmpty(Product product)
        {
            if (product.Id != Guid.Empty)
                return false;

            return true;
        }
    }
}
