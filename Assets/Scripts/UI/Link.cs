using UnityEngine;

namespace CubeHopper.UI
{
    public class Link : MonoBehaviour
    {

        public void OpenLink(string link)
        {
            Application.OpenURL(link);
        }
    }
}
