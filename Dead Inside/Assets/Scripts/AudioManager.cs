using UnityEngine;

[System.Serializable]
public class Sound
{

    public string name;
    public AudioClip clip;    

    private AudioSource src;// source

    [Range(0.5f, 1.5f)]
    public float pitch = 1f;//tom
    [Range(0f, 1f)]
    public float volume = 0.7f;//volume

    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    public bool loop = false;

    public void SetSource(AudioSource _src)
    {
        src = _src;
        src.clip = clip;
        src.loop = loop;
    }

    #region ClipManagement
    //Usado no play sound
    public void PlayClip()
    {
        src.Play();
        src.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        src.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
    }

    //usado no stopSound
    public void StopClip()
    {
        src.Stop();
    }
    #endregion

}

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    [SerializeField]
    Sound[] sounds; //array q contem todos os sons

    #region Awake
    private void Awake()
    {
        if(instance != null)
        {
            if(instance != this)
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
    #endregion

    #region Start
    void Start()
    {   //para cada som é adicionado um Game Object
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            //componente de Audio Source
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }
    #endregion

    #region PlaySound
    public void PlaySound(string _name)
    {//toca o som se for encontrado
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].PlayClip();
                return;
            }
        }
    }
    #endregion

    #region StopSound
    public void StopSound(string _name)
    {//toca o som se for encontrado
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].StopClip();
                return;
            }
        }
    }
    #endregion
}
