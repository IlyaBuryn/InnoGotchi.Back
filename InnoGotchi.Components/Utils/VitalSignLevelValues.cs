namespace InnoGotchi.Components.Utils
{
    public abstract class VitalSignLevelValues
    {
        public class VitalSignValue
        {
            public VitalSignValue(string name, string style)
            {
                Name = name;
                Style = style;
            }

            public string Name { get; set; }
            public string Style { get; set; }
        }

        public readonly Dictionary<int, VitalSignValue> Levels;

        public VitalSignLevelValues()
        {
            Levels = SetLevels();
        }

        public VitalSignValue GetByNumber(int num)
        {
            if (Levels.TryGetValue(num, out VitalSignValue result))
                return result;
            else
                return GetDefault();
        }

        public abstract Dictionary<int, VitalSignValue> SetLevels();

        public virtual VitalSignValue GetDefault()
        {
            return new VitalSignValue("", "");
        }
    }
}
