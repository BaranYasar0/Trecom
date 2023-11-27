using System.Diagnostics.CodeAnalysis;
using Trecom.Shared.CCS.GlobalException;

namespace Trecom.Shared.Extensions;

public static class ObjectNullCheckExtension
{
    public static bool ValidateNullBool<T>([NotNull] this T obj)
        => obj == null ? true:false;

    public static bool ValidateNullBusinessBoolean<T>([NotNull] this T obj) => obj == null ? throw new BusinessException($"{obj?.GetType().Name} is null!") : true;


    public static T ValidateNull<T>([NotNull] this T obj) => 
        _=obj ?? throw new ArgumentNullException($"{obj?.GetType().Name} is null!");

    public static T ValidateNullBusiness<T>([NotNull] this T obj) =>
        _ = obj ?? throw new BusinessException($"{obj?.GetType().Name} is null!");
}