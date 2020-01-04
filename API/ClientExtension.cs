using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PublicTimeAPI
{
  public static class ClientExtension
  {
    public static string GetClientId(this HttpRequest request)
    {
      var token = request.Headers["AuthenticationToken"];

      if (token.Equals(string.Empty))
      {
        throw new Exception();
      }

      return token;
    }
    public static bool Equalizer<T>(T a, T b)
    {
      return Convert.ToString(a).Equals(Convert.ToString(b));
    }

    public static void SetValuesOf<T>(this T one, T other)
    {
      var properties = typeof(T)
        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Where(p => p.CanWrite && p.CanRead && (p.GetMethod.IsFinal || !p.GetMethod.IsVirtual));

      foreach (var property in properties)
      {
        var value = property.GetValue(other);

        if (!Equals(property.GetValue(one), value))
        {
          property.SetValue(one, value);
        }
      }
    }
  }
}
