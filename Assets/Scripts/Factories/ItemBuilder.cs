using System;
using System.Collections.Generic;
using Common;
using Player;

namespace Factories
{
    public interface IItemBuilderId
    {
        IItemBuilderName Id(ItemId id);
    }

    public interface IItemBuilderName
    {
        IItemBuilderDescription Name(string name);
    }

    public interface IItemBuilderModifier
    {
        IItemBuilderModifier AddModifier(StatType type, float value, ModifierType modifierType);
        Item Build(); // теперь можно билдить
    }

    public class ItemBuilder : IItemBuilderId, IItemBuilderName, IItemBuilderModifier, IItemBuilderDescription, IItemBuilderStack
    {
        private ItemId _id;
        private string _name;
        private string _description;
        private int _stackSize;
        private List<StatModifier> _statModifiers = new();

        private ItemBuilder() {}
        public static IItemBuilderName Create()
        {
            return new ItemBuilder();
        }
        
        public IItemBuilderName Id(ItemId id)
        {
            _id = id;
            return this;
        }

        public IItemBuilderDescription Name(string name)
        {
            _name = name;
            return this;
        }

        public IItemBuilderModifier AddModifier(StatType type, float value, ModifierType modifierType)
        {
            _statModifiers.Add(new StatModifier(type, value, modifierType));
            return this;
        }

        public Item Build()
        {
            return new Item(_id, _name, _description, _statModifiers, _stackSize);
        }

        public IItemBuilderStack Description(string description)
        {
            _description = description;
            return this;
        }

        public IItemBuilderModifier StackSize(int size)
        {
            _stackSize = size;
            return this;
        }
    }
    
    public interface IItemBuilderStack
    {
        IItemBuilderModifier StackSize(int size);
    }

    public interface IItemBuilderDescription
    {
        public IItemBuilderStack Description(string description);
    }
}