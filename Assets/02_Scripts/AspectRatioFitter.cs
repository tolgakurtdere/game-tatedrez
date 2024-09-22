using UnityEngine;

namespace TK.Utility
{
    [RequireComponent(typeof(Camera))]
    public class AspectRatioFitter : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            // just an example of how we should NOT arrange camera size :)

            var ratio = Screen.height / (float)Screen.width;
            GetComponent<Camera>().orthographicSize = Mathf.Max(6, ratio * 3.4f);
        }
    }
}