using System.Collections;
using UnityEngine;

namespace CardHouse
{
    public class LifetimeDestructor : MonoBehaviour
    {
        public float Lifetime = 1f;

        void Start()
        {
            StartCoroutine(DieAfterTime(Lifetime));
        }

        IEnumerator DieAfterTime(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
}
