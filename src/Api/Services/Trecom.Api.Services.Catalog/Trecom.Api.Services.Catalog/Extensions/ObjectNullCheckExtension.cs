using System.Diagnostics.CodeAnalysis;
using Trecom.Shared.CCS.GlobalException;

namespace Trecom.Api.Services.Catalog.Extensions
{
    public static class ObjectNullCheckExtension
    {
        public static bool NullValidation<T>([NotNull] this T obj)
        {
            if (obj == null)
                throw new ArgumentNullException($"{obj?.GetType().Name} is null!");

            return true;
        }

        public static bool NullBusinessValidation<T>([NotNull] this T obj) => obj == null ? throw new BusinessException($"{obj?.GetType().Name} is null!") : true;

    }
}
