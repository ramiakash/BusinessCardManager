using System.Reflection;
public abstract class Enumeration : IComparable
{
    public string Name { get; private set; }
    public int Id { get; private set; } 

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        var fields = typeof(T).GetFields(BindingFlags.Public |
                                        BindingFlags.Static |
                                        BindingFlags.DeclaredOnly);

        return fields.Select(f => f.GetValue(null)).Cast<T>();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public int CompareTo(object? other) => Id.CompareTo(((Enumeration?)other)?.Id);

    public override int GetHashCode() => Id.GetHashCode();

    public static T FromValue<T>(int value) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(item => item.Id == value);
        if (matchingItem == null)
        {
            throw new InvalidOperationException($"'{value}' is not a valid ID for {typeof(T).Name}");
        }
        return matchingItem;
    }

    public static T FromName<T>(string name) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(item =>
            string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));

        if (matchingItem == null)
        {
            throw new InvalidOperationException($"'{name}' is not a valid Name for {typeof(T).Name}");
        }
        return matchingItem;
    }
}