using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider soundSlider;
    public Slider musicSlider;

    private GameManager gameManager;

    private void Start()
    {
        if (soundSlider == null || musicSlider == null)
        {
            Debug.LogError("Не все ссылки установлены в VolumeControl скрипте!");
            return;
        }

        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager не найден!");
            return;
        }

        soundSlider.onValueChanged.AddListener(SetSoundVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);

        // Обновляем позицию ползунков после загрузки настроек
        UpdateSlidersAfterLoad();
    }

    public void SetSoundVolume(float volume)
    {
        gameManager.SetSoundVolume(volume);
        gameManager.SaveVolume(); // Сохраняем сразу после изменения
    }

    public void SetMusicVolume(float volume)
    {
        gameManager.SetMusicVolume(volume);
        gameManager.SaveVolume(); // Сохраняем сразу после изменения
    }

    private void UpdateSlidersAfterLoad()
    {
        if (soundSlider != null && musicSlider != null)
        {
            float soundVolumeLinear = PlayerPrefs.GetFloat(GameManager.SoundVolumeKey, 0.5f);
            float musicVolumeLinear = PlayerPrefs.GetFloat(GameManager.MusicVolumeKey, 0.5f);

            soundSlider.value = soundVolumeLinear;
            musicSlider.value = musicVolumeLinear;
        }
    }
}