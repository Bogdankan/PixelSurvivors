using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    private void Awake()
    {
        // ѕерев≥р€Їмо, чи вже ≥снуЇ ≥нстанс AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // «бер≥гаЇмо об'Їкт м≥ж сценами
        }
        else
        {
            Destroy(gameObject); // ¬идал€Їмо дубл≥кати
        }
    }
}
