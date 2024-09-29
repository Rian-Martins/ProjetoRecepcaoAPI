namespace ProjetoRecepcao.Roteamento
{
    public class CustomRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values[routeKey] is string valueString && int.TryParse(valueString, out int value))
            {
                return value > 0;
            }
            return false;
        }
    }
}
