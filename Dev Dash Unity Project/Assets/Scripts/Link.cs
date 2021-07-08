using UnityEngine;
using System.Runtime.InteropServices;

public class Link : MonoBehaviour
{
    public void OpenLink(string _link)
    {
#if !UNITY_EDITOR
		openWindow(_link);
#endif
    }

    [DllImport("__Internal")]
    private static extern void openWindow(string url);
}
