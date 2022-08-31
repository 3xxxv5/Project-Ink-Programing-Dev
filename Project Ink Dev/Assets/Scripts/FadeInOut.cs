using UnityEngine;
using UnityEngine.UI;
public class FadeInOut : MonoBehaviour
{
  void Start()
  {
    Image fade = gameObject.GetComponent<Image>();
    fade.CrossFadeAlpha(0, 1, false);
  }
}