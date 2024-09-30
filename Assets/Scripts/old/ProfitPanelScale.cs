using UnityEngine;
using UnityEngine.UI;

public class ProfitPanelScale : MonoBehaviour
{
    int count = 0;
    private void Update()
    {
        switch (count)
        {
            case 0:
                RectTransform rt = transform.GetChild(0).transform.GetComponent<RectTransform>();
                Vector2 size_delta = rt.sizeDelta;
                Vector2 local_scale = rt.localScale;
                break;
            case 1:
                Destroy(GetComponent<HorizontalLayoutGroup>());
                break;
            case 2:
                Destroy(this);
                break;
        }
        count++;
    }
    

}
