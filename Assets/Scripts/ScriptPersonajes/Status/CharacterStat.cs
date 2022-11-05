
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Mochi.CharacterStats {

    [Serializable]
    public class CharacterStat 
    {
        public float BaseValue;

        public float Value { 
            get {
                if (isDirty)
                {
                    _value = CalculateTotal();
                    isDirty = false;
                }
                return _value;
            } 
        }

        private bool isDirty = true;
        private float _value;

        private readonly List<StatModifier> statModifiers;
        public readonly IReadOnlyCollection<StatModifier> StatModifiers;

        public CharacterStat()
            {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
            }
        // Inicializa un parametro
        public CharacterStat(float baseValue) :this()
            {
            BaseValue = baseValue;
     
            }

        // Criterio de ordenamiento
        private int CompareModOrder(StatModifier a, StatModifier b)
            {
            if (a.Order < b.Order) return -1;

            else if (a.Order > b.Order) return 1;

            return 0;  // a.Order == b.Order
            }


        // Agrega un modificador de parametro
        public void AddModifier(StatModifier mod) 
            {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModOrder);
            }
        // Quita un modificador de parametro
        public void RemoveModifier(StatModifier mod) 
            {
            isDirty = true;
            statModifiers.Remove(mod);
            statModifiers.Sort(CompareModOrder);
            }

        // Quitar todos los modificadores de una fuente en especifica 
        public bool RemoveAllSourceMods(object source)
            {
            bool didRemove = false;

            for (int i = statModifiers.Count - 1; i>= 0; i--)
                {
                if (statModifiers[i].Source == source)
                    {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                    }
                }
            return didRemove;
            }

        // Calcula el monto final del parametro despues de los modificadores
        private float CalculateTotal()
            {

            float finalValue = BaseValue;

            for(int i = 0; i < statModifiers.Count; i++)
                {

                StatModifier mod = statModifiers[i];

                if (mod.Type == ModType.Flat)
                    {
                    finalValue += mod.Value;
                    }
                else if (mod.Type == ModType.Percent)
                    {
                    finalValue *= 1 + mod.Value;
                    }


                }
            return (float)Math.Round(finalValue,4);
            }

    }
}