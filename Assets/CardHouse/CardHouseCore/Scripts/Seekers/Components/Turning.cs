
using UnityEngine;

namespace CardHouse
{
    public class Turning : BaseSeekerComponent<float>
    {
        protected override Seeker<float> GetDefaultSeeker()
        {
            return new ExponentialAngleFloatSeeker();
        }

        protected override float GetCurrentValue()
        {
            return UseLocalSpace ? transform.localRotation.eulerAngles.z : transform.rotation.eulerAngles.z;
        }

        protected override void SetNewValue(float value)
        {
            if (UseLocalSpace)
            {
                transform.localRotation = Quaternion.Euler(0, 0, value);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, value);
            }
        }
    }
}
