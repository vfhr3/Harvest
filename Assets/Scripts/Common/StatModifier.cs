using Player;

namespace Common
{
    public class StatModifier
    {
        public StatType Type { get; private set; }
        public float Value { get; private set; }
        public ModifierType ModifierType { get; private set; }
        
        public StatModifier(StatType type, float value, ModifierType modifierType)
        {
            Type = type;
            Value = value;
            ModifierType = modifierType;
        }

    }
}