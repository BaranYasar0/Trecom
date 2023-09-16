using Ardalis.SmartEnum;

namespace Trecom.Api.Services.Catalog.Models.Enums;

public class ColorType:SmartEnum<ColorType>
{
    public static ColorType Red = new ColorType(nameof(Red), 0);
    public static ColorType Blue = new ColorType(nameof(Blue), 1);
    public static ColorType Yellow = new ColorType(nameof(Yellow), 2);
    public static ColorType Green = new ColorType(nameof(Green), 3);
    public static ColorType Orange = new ColorType(nameof(Orange), 4);
    public static ColorType Pink = new ColorType(nameof(Pink), 5);
        
    public ColorType(string name, int value) : base(name, value)
    {
    }

    public ColorType():base(null,0)
    {
            
    }
}