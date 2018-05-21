@file:JvmName("UpdateTextureKt")

package com.tyounanmoti.oculus.go.unity.webview

import android.graphics.Bitmap
import android.graphics.Canvas
import android.opengl.GLES20
import android.opengl.GLUtils

fun Update(texture_id: Int) {
    var bitmap = Bitmap.createBitmap(32, 32, Bitmap.Config.ARGB_8888)
    var canvas = Canvas(bitmap)
    canvas.drawARGB(255, 0, 255, 0)
    GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, texture_id)
    GLES20.glPixelStorei(GLES20.GL_UNPACK_ALIGNMENT, 1)
    GLUtils.texSubImage2D(GLES20.GL_TEXTURE_2D, 0, 0, 0, bitmap)
    GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, 0)
}
