using System.ComponentModel;

namespace ECL.Utility
{
    public class Enums
    {
        internal static Enum ToObject(Type type, int value)
        {
            throw new NotImplementedException();
        }

        public enum Status
        {
            Active = 1,
            Inactive = 0,
        }

        public enum TrueFalse
        {
            True = 1,
            False = 0
        }

        public enum CreatedBy
        {
            [Description("Joy Sarkar")]
            Joy = 1,
            [Description("Kuntal Pramanik")]
            Kuntal = 2,
            [Description("Anurag Chatterjee")]
            Anurag = 3,
        }
        public enum ModifiedBy
        {
            [Description("Joy Sarkar")]
            Joy = 1,
            [Description("Kuntal Pramanik")]
            Kuntal = 2,
            [Description("Anurag Chatterjee")]
            Anurag = 3,
        }
        public enum Source
        {
            [Description("API Upload")]
            Api = 1,
            [Description("Manual Upload")]
            Manual = 2
        }
    }
}
