using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyde : MonoBehaviour
{
    public IEnumerator DisableOnTime(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    public IEnumerator DisableOnTimeAndTrail(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
