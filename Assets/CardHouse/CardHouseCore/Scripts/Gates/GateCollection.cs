using System;
using System.Collections.Generic;
using System.Linq;

namespace CardHouse
{
    [Serializable]
    public class GateCollection<T>
    {
        public List<Gate<T>> Gates;

        public bool AllUnlocked(T gateParams)
        {
            return Gates.Count == 0 || Gates.All(x => x.IsUnlocked(gateParams));
        }

        public bool AnyUnlocked(T gateParams)
        {
            return Gates.Count == 0 || Gates.Any(x => x.IsUnlocked(gateParams));
        }
    }
}
