using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource music;
    public void SoundOn(){
        music.Play();
    }
    public void SounOff(){
        music.Stop();
    }
}
