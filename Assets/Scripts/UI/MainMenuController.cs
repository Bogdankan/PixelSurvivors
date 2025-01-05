using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private AudioSource musicAudioSource; // Прив'язка до джерела музики
    private void Awake()
    {
        musicAudioSource = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        // Завантаження UXML-файлу
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        // Знаходимо кнопку і додаємо обробник події
        var playButton = root.Q<Button>("Play"); // Ідентифікуємо за іменем
        playButton.clicked += OnPlayButtonClicked;

        // Знаходимо слайдер і додаємо обробник події
        var musicSlider = root.Q<Slider>("Music"); // Ідентифікуємо за іменем
        musicSlider.RegisterValueChangedCallback(evt => OnMusicSliderValueChanged(evt.newValue));
    }

    private void OnPlayButtonClicked()
    {
        // Завантаження сцени з грою
        SceneManager.LoadScene("Level1"); // Замініть "GameScene" на ім'я вашої сцени
    }

    private void OnMusicSliderValueChanged(float newValue)
    {
        // Зміна гучності музики
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = newValue / 100f; // Нормалізація до діапазону [0, 1]
        }
    }
}
