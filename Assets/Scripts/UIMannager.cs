using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Controls")]
    public GameObject panelControls;
    public Button buttonLeft, buttonRight, buttonJump;

    [Header("Interaction")]
    public Button interactButton;

    [Header("Message Box")]
    public GameObject panelMessageBox;
    public TMP_Text textMessage;
    public Button buttonCloseMsg;

    [Header("Door UI")]
    public GameObject panelDoorUI;
    public TMP_InputField inputPassword;
    public Button buttonSubmitPass;
    public TMP_Text textFeedback;

    [Header("Level Complete")]
    public GameObject panelLevelComplete;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        TimerManager.Instance.StartTimer();
        interactButton.gameObject.SetActive(false);
        panelMessageBox.SetActive(false);
        panelDoorUI.SetActive(false);
        panelLevelComplete.SetActive(false);
        
        buttonCloseMsg.onClick.AddListener(() => panelMessageBox.SetActive(false));
    }

    public void ShowInteractButton(bool show) => interactButton.gameObject.SetActive(show);
    public void ShowdoorUI(bool show) => panelDoorUI.SetActive(show);

    public void ShowMessage(string msg)
    {
        textMessage.fontSize = 50f;
        textMessage.text = msg;
        panelMessageBox.SetActive(true);
    }

    public void ShowDoorUI(string encoded)
    {
        panelDoorUI.SetActive(true);
        inputPassword.text = "";
    }

    public void ShowLevelComplete() => panelLevelComplete.SetActive(true);
}
