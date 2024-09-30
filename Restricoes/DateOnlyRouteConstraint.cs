using Microsoft.AspNetCore.Routing;
using System;
using System.Globalization;

public class DateOnlyRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContext httpContext,
                      IRouter route,
                      string routeKey,
                      RouteValueDictionary values,
                      RouteDirection routeDirection)
    {
        if (values.TryGetValue(routeKey, out var value) && value is string dateValue)
        {
            return DateOnly.TryParse(dateValue, out _);
        }
        return false;
    }
}
