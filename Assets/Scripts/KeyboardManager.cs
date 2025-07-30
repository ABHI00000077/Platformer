using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class KeyboardManager : MonoBehaviour
{
    public GameObject virtualKeyboardPanel;
    public TMP_InputField passwordInput;

    private bool inputFocused;

    void Start()
    {
        // Initially hide the keyboard
        virtualKeyboardPanel.SetActive(false);

        // Hook into input field events
        passwordInput.onSelect.AddListener(OnInputSelected);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clicked = EventSystem.current.currentSelectedGameObject;

            // If clicked outside input field and keyboard
            if (clicked != passwordInput.gameObject && !IsChildOf(clicked, virtualKeyboardPanel))
            {
                virtualKeyboardPanel.SetActive(false);
                inputFocused = false;
            }
        }
    }

    void OnInputSelected(string _) // TMP_InputField passes string
    {
        inputFocused = true;
        virtualKeyboardPanel.SetActive(true);
    }

    private bool IsChildOf(GameObject obj, GameObject parent)
    {
        if (obj == null || parent == null) return false;
        return obj.transform.IsChildOf(parent.transform);
    }
}
