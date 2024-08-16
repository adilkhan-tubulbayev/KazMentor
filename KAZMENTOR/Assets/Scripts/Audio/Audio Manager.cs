using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    public AudioSource mainMenu;
    public AudioSource dialogue;
    public AudioSource talk;
    public AudioSource groundWalking;
    public AudioSource floorWalking;
    public AudioSource button;
    public AudioSource universeMelody;
    public AudioSource coin;
    public AudioSource outside;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject); // Удалить дублирующийся экземпляр
        }
    }

    public void PlayMainMenuSound() {
        PlayAudioClip(mainMenu);
    }

    public void PlayDialogueSound() {
        PlayAudioClip(dialogue);
    }

    public void PlayTalkSound() {
        PlayAudioClip(talk);
    }

    public void PlayGroundWalkingSound() {
        PlayAudioClip(groundWalking);
    }

    public void PlayFloorWalkingSound() {
        PlayAudioClip(floorWalking);
    }

    public void PlayButtonSound() {
        PlayAudioClip(button);
    }

    public void PlayUniverseMelodySound() {
        PlayAudioClip(universeMelody);
    }

    public void PlayCoinSound() {
        PlayAudioClip(coin);
    }

    public void PlayOutsideSound() {
        PlayAudioClip(outside);
    }

    private void PlayAudioClip(AudioSource audioClip) {
        if (audioClip != null) {
            audioClip.PlayOneShot(audioClip.clip); // Воспроизведение звука
        }
    }

    public void StopAudioClip(AudioSource audioClip) {
        if (audioClip != null && audioClip.isPlaying) {
            audioClip.Stop(); // Остановка звука, если он играет
        }
    }
}
