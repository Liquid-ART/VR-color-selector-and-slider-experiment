using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name = "NoName";
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume = 1;
    [Range(0.1f,3f)]
    public float pitch = 1;
    public bool loop;
    [Range(0f, 1f)]
    public float spatial = 1;

    [HideInInspector]
    public AudioSource source;
}

public class SoundPlayer
{
    int lastIndex;

    public void Play(int index, Sound[] sounds, GameObject gameObj)
    {
        sounds[index].source = gameObj.GetComponent<AudioSource>();
        sounds[index].source.clip = sounds[index].clip;
        sounds[index].source.volume = sounds[index].volume;
        sounds[index].source.pitch = sounds[index].pitch;
        sounds[index].source.loop = sounds[index].loop;
        sounds[index].source.spatialBlend = sounds[index].spatial;
        sounds[index].source.Play();
    }



    public void PlayRandom(Sound[] sounds, GameObject gameObj)
    {
        int index = Random.Range(0, sounds.Length);
        if (index == lastIndex)
        {
            index = Random.Range(0, sounds.Length);
        }
        lastIndex = index;

        Play(index, sounds, gameObj);

    }
}