using System.Reflection;

namespace AI_Project.Helpers
{
    public static class CopyHelper
    {
        public static void DeepCopyTo(this object source, object destination)
        {
            if (source == null || destination == null) return;

            var typeSrc = source.GetType();
            var typeDest = destination.GetType();

            foreach (var prop in typeSrc.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!prop.CanRead) continue;
                var destProp = typeDest.GetProperty(prop.Name);
                if (destProp == null || !destProp.CanWrite) continue;

                var val = prop.GetValue(source);
                if (val == null)
                {
                    destProp.SetValue(destination, null);
                }
                else if (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
                {
                    // primitives and string – just copy
                    destProp.SetValue(destination, val);
                }
                else
                {
                    // nested reference – create a new instance and recurse
                    var nestedDest = Activator.CreateInstance(prop.PropertyType)!;
                    val.DeepCopyTo(nestedDest);
                    destProp.SetValue(destination, nestedDest);
                }
            }
        }
    }
}
