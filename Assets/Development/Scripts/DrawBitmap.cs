using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class DrawBitmap : MonoBehaviour {
#if UNITY_ANDROID
    [DllImport("liboculus_go_webview_native")]
    static extern IntPtr get_update_texture_func();

    delegate void logFunc([In, MarshalAs(UnmanagedType.LPStr)]string log);

    [DllImport("liboculus_go_webview_native", CharSet = CharSet.Ansi)]
    static extern void set_debug_log_func(logFunc func);

    void LogFunc(string message) {
        Debug.LogError(message);
    }

    Texture2D texture;

    void Start() {
        set_debug_log_func(LogFunc);
        Debug.Log("Log func set.");
        texture = new Texture2D(32, 32, TextureFormat.ARGB32, false, true);
        GetComponent<Renderer>().material.mainTexture = texture;

        StartCoroutine(UpdateTexture());
    }

    IEnumerator UpdateTexture() {
        while (true) {
            yield return new WaitForEndOfFrame();
            GL.IssuePluginEvent(get_update_texture_func(), texture.GetNativeTexturePtr().ToInt32());
        }
    }
#endif
}
