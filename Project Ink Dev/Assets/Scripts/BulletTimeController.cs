using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTimeController : Singleton<BulletTimeController>
{
    [SerializeField, Range(0f, 1f)] float bulletTimeScale = 0.1f;

    float defaultFixedDeltaTime;
    float t;

    protected override void Awake()
    {
        base.Awake();
        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void BulletTime(float duration)
    {
        Time.timeScale = bulletTimeScale;
        StartCoroutine(SlowOutCoroutine(duration));
    }

    IEnumerator SlowOutCoroutine(float duration)
    {
        t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / duration;
            Time.timeScale = Mathf.Lerp(bulletTimeScale, 1f, t);
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;

            yield return null;
        }
    }
}