using UnityEngine;

public class SFXPlayer : SingletonBehaviour<SFXPlayer>
{

    public AudioClip clip;

    private AudioSource player;

    private void Start() {
        player = gameObject.AddComponent<AudioSource>();
        player.clip = clip;
    }

    public void Play() {
        player.Play();
    }

}
