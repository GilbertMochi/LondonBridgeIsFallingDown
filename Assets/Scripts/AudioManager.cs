//made after brackeys tutorial https://youtu.be/HhFKtiRd0qI

using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop = false;

    [Range(0f, 1f)]
    public float volume = .7f;

    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 1f)]
    public float pitchVariance = .1f;

    [Range(0f, .5f)]
    public float volumeVariance = .1f;


    private AudioSource source;

    public void SetSource(AudioSource s)
    {
        source = s;
        source.clip = clip;
        source.loop = loop;
    }

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-volumeVariance / 2f, volumeVariance / 2f));//adds some randomnes to the sound to make seem abit different from the actual clip
        source.pitch = pitch * (1 + Random.Range(-pitchVariance / 2f, pitchVariance / 2f));
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    Sound[] sounds;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            go.transform.SetParent(this.transform);
            sounds[i].SetSource(go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        //no sound with the name found
        Debug.LogWarning("Audio Manager: sound with name '" + _name + "' not found in the sounds array.");
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }

        //no sound with the name found
        Debug.LogWarning("Audio Manager: sound with name '" + _name + "' not found in the sounds array.");
    }
}
