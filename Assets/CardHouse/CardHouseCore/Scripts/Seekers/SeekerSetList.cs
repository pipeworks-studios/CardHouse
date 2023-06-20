using System.Collections.Generic;
using System.Linq;

namespace CardHouse
{
    public class SeekerSetList : List<SeekerSet>
    {
        public SeekerSet GetSeekerSetFor(Card card)
        {
            foreach (var seekerSet in this)
            {
                if (seekerSet == null)
                    continue;

                if (seekerSet.Card == card)
                {
                    return seekerSet;
                }
            }

            return this.FirstOrDefault(x => x != null && x.Card == null);
        }
    }
}
