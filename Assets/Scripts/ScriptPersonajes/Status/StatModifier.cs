
namespace Mochi.CharacterStats {
    public enum ModType {

        Flat,
        Percent,
        }
    public class StatModifier {
        public readonly float Value;
        public readonly ModType Type;
        public readonly int Order;
        public readonly object Source;

        public StatModifier(float value, ModType type, int order, object source)
            {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
            }

        public StatModifier(float value, ModType type) : this(value, type, (int)type, null) { }
        public StatModifier(float value, ModType type, int order) : this(value, type, order, null) { }
        public StatModifier(float value, ModType type, object source) : this(value, type, (int)type, source) { }
    }
}