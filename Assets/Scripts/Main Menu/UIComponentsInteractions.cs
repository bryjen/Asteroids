using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
     * <param name="code">0 MEANS ON POINTER/MOUSE ENTER,
     *                    1 MEANS ON POINTER/MOUSE EXIT
     * </param>
     *
     *<remarks>
     * The holder of this script should have its first child be a UI Text block witha valid text component
     * </remarks>
     * 
     */
public class UIComponentsInteractions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject targetGameobject;

    [Header("On Hover Colors")] 
    [SerializeField] private bool isColorChangeEnabled;
    [SerializeField] private float colorChangeAnimationDuration;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color onHoverColor;
    
    [Header("Mover Move Distance (Rect Transform Pos X Value)")]
    [SerializeField] private bool isMoveEnabled;
    [SerializeField] private float moveAnimationDuration;
    [SerializeField] private float X;
    [SerializeField] private float initialX;

    private void Start()
    {
        //Get the first child of this gameobject
        targetGameobject = transform.GetChild(0).gameObject;
        
        initialX = targetGameobject.GetComponent<RectTransform>().position.x;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.cancel(targetGameobject);
        
        if (isColorChangeEnabled)
            LeanTween.textColor(targetGameobject.GetComponent<RectTransform>(), onHoverColor, colorChangeAnimationDuration).setEaseInOutQuad();

        if (isMoveEnabled)
            LeanTween.moveX(targetGameobject, X, moveAnimationDuration).setEaseOutCubic();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(targetGameobject);
        
        if (isColorChangeEnabled)
            LeanTween.textColor(targetGameobject.GetComponent<RectTransform>(), defaultColor, colorChangeAnimationDuration).setEaseInOutQuad();

        if (isMoveEnabled)
            LeanTween.moveX(targetGameobject, initialX, moveAnimationDuration).setEaseOutCubic();
    }
}
