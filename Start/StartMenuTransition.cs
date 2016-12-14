using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(CanvasScaler))]
public class StartMenuTransition : MonoBehaviour {
    private class UIElement {
        public RectTransform RT { get; set; }
        public bool Left { get; set; }
        public Vector2 TargetAnchoredPosition { get; set; }

        private Vector2 originalAnchoredPosition;
        public Vector2 OriginalAnchoredPosition { get { return originalAnchoredPosition; } }

        public UIElement(RectTransform rT, bool left) {
            RT = rT;
            Left = left;
            TargetAnchoredPosition = originalAnchoredPosition = rT.anchoredPosition;
        }
    }

    [SerializeField]
    private float lerpRate = .1f;

    [SerializeField]
    private RectTransform[] menus;

    private List<List<UIElement>> UIElements = new List<List<UIElement>>();
    /*
    Each List of Lists contains the root elements of the menu in "menus" Array. Indexes correspond.
    For example, UIElements[i] would be a list all of the RectTransform components that are direct children of menus[i].
    */

    private int currentMenu;
    public int CurrentMenu { get { return currentMenu; } }

    private void Awake() {
        //gameobject resets with the network.

        foreach (RectTransform t in menus) {
            t.gameObject.SetActive(true);
        }

        foreach (RectTransform menu in menus) {
            RectTransform[] rTList = menu.GetComponentsInChildren<RectTransform>();
            List<UIElement> elements = new List<UIElement>();

            foreach (RectTransform rT in rTList)
                if (rT.parent.name == menu.name)
                    elements.Add(new UIElement(rT, rT.position.x - transform.position.x <= 0));//True (Left) if 0 (exact middle).

            UIElements.Add(elements);
        }
        EnableMenu(0);
        SnapAllToTarget();
    }

    private void FixedUpdate() {
        foreach (List<UIElement> list in UIElements)
            foreach (UIElement element in list)
                element.RT.anchoredPosition = Vector2.Lerp(element.RT.anchoredPosition, element.TargetAnchoredPosition, lerpRate);
    }

    private void SnapAllToTarget() {
        foreach (List<UIElement> list in UIElements)
            foreach (UIElement element in list)
                element.RT.anchoredPosition = element.TargetAnchoredPosition;
    }

    //int menu corresponds to the index of the menus list.
    public void EnableMenu(int menu) {
        currentMenu = menu;

        for (int i = 0; i < UIElements.Count; i++) {
            foreach (UIElement element in UIElements[i]) {
                /*
                Disabled for now, but might need it if I add controller support because you might be able to target things outside of the menu.
                Makes the selectable turn grey, can fix by changing the disabled color in the editor.

                Selectable[] selectables = element.RT.GetComponentsInChildren<Selectable>();//Selectables are the base of any interactable thing (button, slider, dropdown...).
                foreach(Selectable s in selectables)
                    s.interactable = i == menu;
                */
                if (i == menu) {
                    element.TargetAnchoredPosition = element.OriginalAnchoredPosition;
                } else {
                    if (GetComponent<CanvasScaler>().screenMatchMode != CanvasScaler.ScreenMatchMode.Expand)//Assumes that the screen match mode is set to "Expand".
                        Debug.LogWarning("Warning: this script assumes the canvas' screen match mode has been set to \"Expand\", untested on any other mode");

                    float xPos = element.RT.anchoredPosition.x;//defaults to the position it is at currently (will not stay this way ever); could set it to anything.
                    float avgXAnchor = (element.RT.anchorMin.x + element.RT.anchorMax.x) / 2;//average of the min and max x anchors (essentially the center of the rectangle set by the anchors)

                    if (element.Left) {
                        xPos =
                            GetComponent<RectTransform>().rect.width
                            * -avgXAnchor
                            //negative so it will go left
                            - (element.RT.rect.width / 2);
                    } else {
                        xPos =
                             GetComponent<RectTransform>().rect.width
                            * (1 - avgXAnchor)
                            + (element.RT.rect.width / 2);
                    }
                    element.TargetAnchoredPosition = new Vector2(xPos, element.RT.anchoredPosition.y);
                }
            }
        }
    }

}