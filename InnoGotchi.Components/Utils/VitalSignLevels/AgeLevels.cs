namespace InnoGotchi.Components.Utils.VitalSignLevels
{
    public class AgeLevels : VitalSignLevelValues
    {
        public AgeLevels() : base() { }

        public override Dictionary<int, VitalSignValue> SetLevels()
        {
            var result = new Dictionary<int, VitalSignValue>();
            result.Add(1, new VitalSignValue("NewBorn", "badge-info"));
            return result;
        }

        public override VitalSignValue GetDefault()
        {
            return Levels[1];
        }
    }
}
