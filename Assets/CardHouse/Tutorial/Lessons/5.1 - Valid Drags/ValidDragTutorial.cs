using TMPro;
using UnityEngine;

namespace CardHouse.Tutorial
{
    public class ValidDragTutorial : MonoBehaviour
    {
        public PhaseManager PhaseManager;

        public CardGroup GroupA;
        public CardGroup GroupB;
        public CardGroup GroupC;
        public CardGroup GroupD;

        public TMP_Dropdown Dropdown10;
        public TMP_Dropdown Dropdown01;
        public TMP_Dropdown Dropdown11;
        public TMP_Dropdown Dropdown02;
        public TMP_Dropdown Dropdown12;

        public void UpdateDropdown10()
        {
            UpdateDropdown(0, false, Dropdown10.value);
        }

        public void UpdateDropdown01()
        {
            UpdateDropdown(1, true, Dropdown01.value);
        }
        public void UpdateDropdown11()
        {
            UpdateDropdown(1, false, Dropdown11.value);
        }
        public void UpdateDropdown02()
        {
            UpdateDropdown(2, true, Dropdown02.value);
        }
        public void UpdateDropdown12()
        {
            UpdateDropdown(2, false, Dropdown12.value);
        }

        void UpdateDropdown(int element, bool isSource, int groupIndex)
        {
            var drag = PhaseManager.Phases[0].ValidDrags[element];
            if (isSource)
            {
                drag.Source = GetGroup(groupIndex);
            }
            else
            {
                drag.Destination = GetGroup(groupIndex);
            }
        }

        CardGroup GetGroup(int i)
        {
            switch (i)
            {
                case 0:
                    return GroupA;
                case 1:
                    return GroupB;
                case 2:
                    return GroupC;
                case 3:
                    return GroupD;
            }
            return GroupA;
        }
    }
}
