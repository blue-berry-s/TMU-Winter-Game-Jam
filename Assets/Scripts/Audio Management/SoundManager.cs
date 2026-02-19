using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour
{

    public Sound[] sounds;

    public static SoundManager instance;


    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void playSound(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("ERROR FINDING SOUND: " + name);
        }
        else {
            s.source.Play();
        }
            
    }

    public void playSound(string name,float volume, float pitch )
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = volume;
        s.source.pitch = pitch;
        s.source.Play();
    }

    public void playRandomizePitchSound(string name)
    {
       
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("ERROR FINDING SOUND: " + name);
        }
        else { 
            s.source.pitch = (UnityEngine.Random.Range(1f, 1.5f));
            s.source.Play();
        }
    }

    public void playRandomizePitchSound(string name, float min, float max)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("ERROR FINDING SOUND: " + name);
        }
        else {
            s.source.pitch = (UnityEngine.Random.Range(min, max));
            s.source.Play();
        }
            
    }

    public void playUINotAllowed() {
       
        playSound("UINotAllowed");
        
    }

    public void playPlayerHurt() {
        
        playSound("KnifeStab");
        playSound("UIHurt");

    }

    public void playPlayerHeal() {
        playSound("GutSquish1");
        playSound("UIHeal");

    }

    public void playBuyOrgan()
    {
        playRandomizePitchSound("OrganSquish2", 1f, 1.5f);
        
    }

    public void playBetOrgan() {
        playRandomizePitchSound("OrganSquish2", 1f, 1.5f);
        playRandomizePitchSound("OrganSquish3", 1.5f, 2f);
    }

    public void playUnbetOragan()
    {
        playRandomizePitchSound("OrganSquish2", 0.75f, 1f);
        playRandomizePitchSound("OrganSquish3", 0.75f, 1f);
    }

    public void playUIButton() {
        playRandomizePitchSound("UIButton", 0.75f, 0.90f);
    }

    public void playConfirmButton() {
        playRandomizePitchSound("LockSound", 0.75f, 0.90f);
        playUIButton();
    }

    public void playPlayerGetMoney() {
        playRandomizePitchSound("PlayerGetMoney", 1f, 1.5f);
    }

    public void playPlayerDie() {
        playSound("PlayerDie");
    }

    public void playInsureBet() {
        playSound("BetShield");
    }

    public void playIncreaseOrganValue() {
        playSound("IncreaseOrganValue");
    }
}




