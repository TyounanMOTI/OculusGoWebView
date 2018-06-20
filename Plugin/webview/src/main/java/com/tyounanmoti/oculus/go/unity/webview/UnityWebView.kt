package com.tyounanmoti.oculus.go.unity.webview

import android.app.Activity
import android.graphics.Bitmap
import android.graphics.Canvas
import android.graphics.Paint
import android.opengl.GLES20
import android.opengl.GLUtils
import android.support.v4.view.ViewCompat
import android.view.View
import android.view.ViewGroup
import android.webkit.WebView
import android.webkit.WebViewClient

class UnityWebView constructor(activity: Activity, private val textureId: Int) {
    private var bitmap = Bitmap.createBitmap(1024, 1024, Bitmap.Config.ARGB_8888)

    init {
        activity.runOnUiThread {
            val webView = WebView(activity)
            val params = ViewGroup.LayoutParams(1024, 1024)
            activity.addContentView(webView, params)
            webView.elevation = -100.0f

            //webView.isDrawingCacheEnabled = true
            webView.settings.javaScriptEnabled = true
            webView.settings.domStorageEnabled = true
            val canvas = Canvas(bitmap)
            webView.webViewClient = object : WebViewClient() {
                override fun onPageFinished(view: WebView?, url: String?) {
                    super.onPageFinished(view, url)
                    //webView.buildDrawingCache(true)
                    webView.draw(canvas)
                }
            }

            webView.loadUrl("https://www.google.co.jp/")
        }
    }

    fun update() {
        GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, textureId)
        GLUtils.texSubImage2D(GLES20.GL_TEXTURE_2D, 0, 0, 0, bitmap)
    }
}