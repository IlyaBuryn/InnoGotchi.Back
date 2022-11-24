namespace InnoGotchi.Components.Utils.VitalSignLevels
{
    public class HungerLevels : VitalSignLevelValues
    {
        public HungerLevels() : base() { }

        public override Dictionary<int, VitalSignValue> SetLevels()
        {
            var result = new Dictionary<int, VitalSignValue>();
            result.Add(0, new VitalSignValue("Full", "badge-success"));
            result.Add(1, new VitalSignValue("Normal", "badge-warning"));
            result.Add(2, new VitalSignValue("Hunger", "badge-danger"));
            result.Add(3, new VitalSignValue("Dead", "badge-secondary"));
            return result;
        }

        public override VitalSignValue GetDefault()
        {
            return Levels[3];
        }
    }
}
