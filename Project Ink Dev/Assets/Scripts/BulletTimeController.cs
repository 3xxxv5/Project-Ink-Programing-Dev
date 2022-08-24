using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTimeController : Singleton<BulletTimeController>
{
    [Header("子弹时间速度")]
    [SerializeField, Range(0f, 1f)] float bulletTimeScale = 0.1f;

    float defaultFixedDeltaTime;
    float t;

    protected override void Awake()
    {
        base.Awake();
        defaultFixedDeltaTime = Time.fixedDeltaTime;
        Debug.Log(defaultFixedDeltaTime);
    }

    public void StartBulletTime(float duration)
    {
        Debug.Log("Start BulletTime");
        Time.timeScale = bulletTimeScale;
        Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
        StartCoroutine(OutCoroutine(duration));
        Debug.Log(Time.timeScale);
    }

    public void StopBulletTime()
    {
        //Time.timeScale = defaultFixedDeltaTime;
        Time.timeScale = 1;
        Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
        Debug.Log(Time.timeScale);
        StopAllCoroutines();
    }

    IEnumerator OutCoroutine(float duration)
    {
        t = 0f;
        while (t < 1f)
        {
            //避免受到到子弹时间影响
            t += Time.unscaledDeltaTime / duration;
            //线性恢复子弹时间
            //Time.timeScale = Mathf.Lerp(bulletTimeScale, 1f, t);
            //Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
            yield return null;
        }
        Time.timeScale = defaultFixedDeltaTime;
        Time.timeScale = 1;
        Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
    }
}