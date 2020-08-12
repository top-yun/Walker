using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource music;
    private Slider slider;
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        slider = this.GetComponent<Slider>();
        slider.value = music.volume;
    }
    private void Update()
    {
        music.volume = slider.value;
    }
}
