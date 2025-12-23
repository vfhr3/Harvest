using System.Collections.Generic;
using System.Linq;
using Player;

namespace Common
{
    public class StatContainer
    {
        private readonly Dictionary<StatType, float> _baseStats = new();
        private readonly List<StatModifier> _modifiers = new();

        public StatContainer(IEnumerable<Stat> baseStats)
        {
            foreach (var stat in baseStats)
            {
                _baseStats[stat.Type] = stat.Value;
            }
        }

        public float Get(StatType type)
        {
            if (!_baseStats.TryGetValue(type, out var baseValue))
                return 0f;

            float additive = 0f;
            float multiplicative = 0f;

            foreach (var modifier in _modifiers.Where(m => m.Type == type))
            {
                switch (modifier.ModifierType)
                {
                    case ModifierType.Additive:
                        additive += modifier.Value;
                        break;

                    case ModifierType.Multiplicative:
                        multiplicative += modifier.Value;
                        break;
                }
            }

            return (baseValue + additive) * (1f + multiplicative);
        }

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
        }

        public void RemoveModifier(StatModifier modifier)
        {
            _modifiers.Remove(modifier);
        }
    }
}