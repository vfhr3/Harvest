using Player;

namespace Common
{
    public struct Stat
    {
        public Stat(StatType type, float value)
        {
            Type = type;
            Value = value;
        }

        public StatType Type { get; private set; }
        public float Value { get; private set; }
    }
}