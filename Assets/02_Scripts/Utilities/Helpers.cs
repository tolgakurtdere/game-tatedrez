using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TK
{
    public static class Helpers
    {
        private static int? s_uiLayer;
        private static int UILayer => s_uiLayer ??= LayerMask.NameToLayer("UI");

        public static bool IsMouseOverUI()
        {
            var eventDataCurPos = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurPos, results);
            return results.Any(r => r.gameObject.layer == UILayer);
        }
    }
}