using UnityEngine;
using UnityEngine.UI;
public class FadeInOut : MonoBehaviour
{
  [SerializeField]
  private float fadeDuration = 1;
  void Start()
  {
    Image fade = gameObject.GetComponent<Image>();
    fade.CrossFadeAlpha(0, fadeDuration, true);
  }
}