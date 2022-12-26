package com.example.alisavetclient;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Base64;
import android.widget.ImageView;
import java.io.ByteArrayInputStream;
import java.io.InputStream;

public class getImage {
    public static Bitmap StringToBitMap(byte[] pic, int bytesRead) {
        try {
            Bitmap bitmapimage = BitmapFactory.decodeByteArray(pic, 0, bytesRead);
            return bitmapimage;
        } catch (Exception e) {
            e.getMessage();
            return null;
        }
    }
}