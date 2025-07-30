using UnityEngine;
using System.Runtime.InteropServices;
[System.Serializable]
public class CipherPassword
{
    public string encoded;
    public int shift;
}
public class DoorInteractable : MonoBehaviour
{
    #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void onGameComplete(int level);
    #endif
    public CipherPassword[] passwordPool;
    private string correctDecodedPassword;

    private CipherPassword chosenPassword;
    private bool playerInRange = false;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        ChooseRandomPassword();
        BoxInteractable.Instance.DisplayEncodedPassword(chosenPassword.encoded);
        Shiftvalue.Instance.DisplayEncodedPassword(chosenPassword.shift);
        UIManager.Instance.buttonSubmitPass.onClick.AddListener(CheckPassword);
    }
    void ChooseRandomPassword()
    {
        int index = Random.Range(0, passwordPool.Length);
        chosenPassword = passwordPool[index];
        correctDecodedPassword = Decode(chosenPassword.encoded, chosenPassword.shift);
    }

    void Update()
    {
        if (playerInRange)
            UIManager.Instance.ShowInteractButton(true);
    }

    public void OpenDoor()
    {
        audioManager.PlaySFX(audioManager.openbox);
        UIManager.Instance.ShowInteractButton(false);
        UIManager.Instance.ShowDoorUI(chosenPassword.encoded);
    }

    void CheckPassword()
    {
        string attempt = UIManager.Instance.inputPassword.text.ToUpper();
        if (attempt == correctDecodedPassword)
        {
            audioManager.PlaySFX(audioManager.finish);
            UIManager.Instance.textFeedback.text = "Access Granted!";
            GetComponent<Collider2D>().enabled = false;
            UIManager.Instance.panelDoorUI.SetActive(false);
            UIManager.Instance.ShowLevelComplete();
            TimerManager.Instance.OnLevelCompleted();
            #if UNITY_WEBGL && !UNITY_EDITOR
                onGameComplete(2);
            #endif
        }
        else
        {
            UIManager.Instance.textFeedback.text = "Access Denied!";
        }
    }

    string Decode(string input, int shift)
    {
        char[] buf = input.ToCharArray();
        for (int i = 0; i < buf.Length; i++)
        {
            char c = buf[i];
            if (char.IsLetter(c))
            {
                char d = (char)((((c - 'A') + shift + 26) % 26) + 'A');
                buf[i] = d;
            }
        }
        return new string(buf);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            UIManager.Instance.ShowInteractButton(false);
            UIManager.Instance.ShowdoorUI(false);
        }
    }
}
