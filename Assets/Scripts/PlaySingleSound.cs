﻿using UnityEngine;
using System.Collections.Generic;
using System;

public class Sound : MonoBehaviour {

	private static Dictionary<AudioClip, OneSound> Sounds = new Dictionary<AudioClip, OneSound>();
	private static Dictionary<string, AudioClip> Cache = new Dictionary<string, AudioClip>();

	private float SoundStart = 0f;
	public OneSound MySound;

	public static void Play( AudioClip clip, float volume=1f){
		if (clip != null){
			if (!Sounds.ContainsKey(clip)) {
				Sounds.Add(clip, new OneSound(clip, volume));
			}
			Sounds[clip].PlayAnother(0);
		}
	}

	internal static void Play(string path, float volume=1f) {
		if (!Cache.ContainsKey(path)) {
			Cache[path] = Resources.Load<AudioClip>(path);
		}
		Play(Cache[path], volume);
	}

	void Update() {
		if (SoundStart == 0f && MySound != null) {
			SoundStart = Time.realtimeSinceStartup;
		}
		if (GetComponent<AudioSource>() != null && Time.realtimeSinceStartup - SoundStart > GetComponent<AudioSource>().clip.length) {
			MySound.ActuallyPlaying--;
			GameObject.Destroy(gameObject);
		}
	}
}

public class OneSound {

	private AudioClip AudioClip;
	private float Volume;
	public int ActuallyPlaying;
	private int MaxSimult = 5;
	private float MinDelay = 0.1f;
	private float LastPlay;

	public OneSound(AudioClip ac, float volume) {
		AudioClip = ac;
		Volume = volume;
	}

	public void PlayAnother(float pan) {
		if (ActuallyPlaying < MaxSimult && Time.time - MinDelay > LastPlay) {
			LastPlay = Time.time;
			ActuallyPlaying++;
			GameObject go = new GameObject("sound clip: " + AudioClip.name);
			AudioSource audio = go.AddComponent<AudioSource>();
			audio.volume = Volume;// -(Volume / MaxSimult * ActuallyPlaying);
			audio.panStereo = pan;// pan == 0 ? Random.Range(-1, 1) : pan;
			audio.clip = AudioClip;
			audio.spatialBlend = -1f;
			audio.Play();
			Sound playSound = go.AddComponent<Sound>();
			playSound.MySound = this;
		}
	}


}
