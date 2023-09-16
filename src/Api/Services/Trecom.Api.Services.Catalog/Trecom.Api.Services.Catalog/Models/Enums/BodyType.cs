using Ardalis.SmartEnum;
using AutoMapper.Configuration;

namespace Trecom.Api.Services.Catalog.Models.Enums;

public sealed class BodyType:SmartEnum<BodyType>
{
    private BodyType() : base(null, 0)
    {
    }

    public BodyType(string name, int value) : base(name, value)
    {
    }
    public static BodyType Small = new BodyType("SMALL", 0);
    public static BodyType Medium = new BodyType("MEDIUM", 1);
    public static BodyType Large = new BodyType("LARGE", 2);
    public static BodyType XLarge = new BodyType("XLARGE", 3);
    public static BodyType XxLarge = new BodyType("XXLARGE", 4);

}