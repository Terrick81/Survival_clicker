


using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableText : MonoBehaviour
{
    static Color disable_color  = Color.gray;
    static Color interact_color = Color.white;
    
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    public bool interactable_text = false;

    void Start()
    {
        
        if (_textMeshProUGUI == null)
        {
            if (GetComponent<TextMeshProUGUI>())
            {
                _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            }
            else
            {
                if (transform.GetChild(0).GetComponent<TextMeshProUGUI>())
                {
                    _textMeshProUGUI = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                }
            }
        }        
    }

    void Interactable()
    {

        if (interactable_text)
        {
            _textMeshProUGUI.color = interact_color;
        }
        else
        {
            _textMeshProUGUI.color = disable_color;
        }
    }
}
