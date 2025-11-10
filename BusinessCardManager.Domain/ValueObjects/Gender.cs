namespace BusinessCardManager.Domain.ValueObjects
{
    public class Gender : Enumeration
    {
        public static readonly Gender Male = new(1, "Male");
        public static readonly Gender Female = new(2, "Female");

        private Gender(int id, string name) : base(id, name) { }

        public static Gender FromString(string name)
        {
            return FromName<Gender>(name);
        }
    }
}
