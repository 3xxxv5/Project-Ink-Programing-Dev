using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMusicPlay : MonoBehaviour
{
    static string musicName;
    static AudioSource music;
    static AudioClip waitPlay;
    static int musicNum;
    // Start is called before the first frame update
    void Start()
    {
        musicNum = Random.Range(1, 13);
        musicName = string.Format("{0}{1}{2}", "music/", "cut", musicNum);
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        waitPlay = Resources.Load<AudioClip>(musicName);
    }

    public static void PlayMusic()
	{
        musicNum = Random.Range(1, 13);
        musicName = string.Format("{0}{1}{2}", "music/", "cut", musicNum);
        Debug.Log(musicName);
        waitPlay = Resources.Load<AudioClip>(musicName);
        music.clip = waitPlay;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnDestroy()
	{
        music = null;
        waitPlay = null;
	}
}
