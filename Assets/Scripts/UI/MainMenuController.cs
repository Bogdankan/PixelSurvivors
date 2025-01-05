using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private AudioSource musicAudioSource; // ����'���� �� ������� ������
    private void Awake()
    {
        musicAudioSource = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        // ������������ UXML-�����
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        // ��������� ������ � ������ �������� ��䳿
        var playButton = root.Q<Button>("Play"); // ������������ �� ������
        playButton.clicked += OnPlayButtonClicked;

        // ��������� ������� � ������ �������� ��䳿
        var musicSlider = root.Q<Slider>("Music"); // ������������ �� ������
        musicSlider.RegisterValueChangedCallback(evt => OnMusicSliderValueChanged(evt.newValue));
    }

    private void OnPlayButtonClicked()
    {
        // ������������ ����� � ����
        SceneManager.LoadScene("Level1"); // ������ "GameScene" �� ��'� ���� �����
    }

    private void OnMusicSliderValueChanged(float newValue)
    {
        // ���� ������� ������
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = newValue / 100f; // ����������� �� �������� [0, 1]
        }
    }
}
