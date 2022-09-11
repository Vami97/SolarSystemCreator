using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    private Camera cam;
    private GraphicRaycaster graphicRaycaster;
    private EventSystem eventSystem;
    private PointerEventData pointerData;

    public static bool overUI;

    private void Awake()
    {
        pointerData = new PointerEventData(null);
    }

    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        graphicRaycaster = GameObject.Find("UI Canvas").GetComponent<GraphicRaycaster>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    public static event Action CloseAllPanels = delegate { };

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerData, results);

            if (results.Count > 0) {
                overUI = true;
                return; 
            }
            else {
                overUI = false;
            }

            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if(hit)
            {
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                clickable?.Click();
            }
            else
            {
                CloseAllPanels();
            }
        }
    }

    public static void CallCloseAllPanels()
    {
        CloseAllPanels();
    }
}
