using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Size : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Image sp;

    Vector3 start;
    Vector3 end;    
    public bool isSizeUp = false;

    private void Start() {
        sp = GetComponent<Image>();
        start = (transform as RectTransform).localScale;
        end = (transform as RectTransform).localScale + new Vector3(0.5f, 0.5f);
    }

    private void Update() {
        Size();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sp.color = new Color(255, 255, 255, 255);
        isSizeUp = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sp.color = new Color(255, 255, 255, 0.6f);
        isSizeUp = false;    
    }

    void Size()
    {
        if(isSizeUp){
            (transform as RectTransform).localScale = 
            Vector3.Lerp((transform as RectTransform).localScale, end, Time.deltaTime * 5);
        }else{
            (transform as RectTransform).localScale = 
            Vector3.Lerp((transform as RectTransform).localScale, start, Time.deltaTime * 5);
        }
    }
}

