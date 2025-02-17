using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CubeHopper.UI
{
    public static class UItools
    {
        private const int UI_LAYER = 5;
        public static bool IsOnUI()
        {
            PointerEventData pointerED = new PointerEventData(EventSystem.current);
            pointerED.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerED, results);
            for (int i = results.Count - 1; i >= 0; i--)
            {
                if (results[i].gameObject.layer != UI_LAYER)
                {
                    results.RemoveAt(i);
                }
            }
            return results.Count > 0;
        }

    }
}
