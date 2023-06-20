
using UnityEngine;

namespace CardHouse
{
    public class Scaling : BaseSeekerComponent<float>
    {
        protected override Seeker<float> GetDefaultSeeker()
        {
            return new ExponentialAngleFloatSeeker();
        }

        protected override float GetCurrentValue()
        {
            return UseLocalSpace ? transform.localScale.x : transform.lossyScale.x;
        }

        protected override void SetNewValue(float value)
        {
            if (!UseLocalSpace && transform.parent != null)
            {
                transform.localScale = Vector3.one * value / transform.parent.lossyScale.x;
            }
            else
            {
                transform.localScale = Vector3.one * value;
            }
        }
    }
}
