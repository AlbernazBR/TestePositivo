﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TestePositivo.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        var member = value.GetType().GetMember(value.ToString());
        var attr = member[0].GetCustomAttribute<DisplayAttribute>();
        return attr?.Name ?? value.ToString();
    }
}
