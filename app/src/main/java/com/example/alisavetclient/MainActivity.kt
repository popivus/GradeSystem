package com.example.alisavetclient

import android.content.SharedPreferences.Editor
import android.os.Bundle
import android.os.CountDownTimer
import android.preference.PreferenceManager
import android.system.Os.socket
import android.util.Log
import android.view.View
import android.view.Window
import android.view.WindowManager
import android.view.animation.AnimationUtils
import android.widget.ImageView
import android.widget.RatingBar
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.activity_main.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.io.DataInputStream
import java.net.InetAddress
import java.net.InetSocketAddress
import java.net.Socket
import java.net.SocketAddress
import java.util.*


class MainActivity : AppCompatActivity(), ExampleDialog.ExampleDialogListener {
    //192.168.0.195
    private var address = ""
    var active: Boolean = false
    private val port = 5000
    private var editIP = 0;
    private var shouldDecline:Boolean = false
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN)
        getWindow().getDecorView().setSystemUiVisibility(View.SYSTEM_UI_FLAG_HIDE_NAVIGATION)
        setContentView(R.layout.activity_main)
        val myPreferences = PreferenceManager.getDefaultSharedPreferences(this@MainActivity)
        if (myPreferences.getString("IP", "") == "") {
            val exampleDialog = ExampleDialog()
            exampleDialog.show(supportFragmentManager, "IP")
        } else {
            address = myPreferences.getString("IP", "")!!
        }
        CoroutineScope(Dispatchers.IO).launch {
            clientGet(address, port)
        }
    }

    override fun onWindowFocusChanged(hasFocus: Boolean) {
        super.onWindowFocusChanged(hasFocus)
        if (hasFocus) {
            window.decorView.systemUiVisibility = (View.SYSTEM_UI_FLAG_LAYOUT_STABLE
                    or View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION
                    or View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
                    or View.SYSTEM_UI_FLAG_HIDE_NAVIGATION
                    or View.SYSTEM_UI_FLAG_FULLSCREEN
                    or View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY)
        }
    }

    fun changeIP_click(view: View) {
        if (editIP == 0) {
            val timer = object: CountDownTimer(3000, 1000) {
                override fun onTick(millisUntilFinished: Long) { }
                override fun onFinish() {
                    editIP = 0
                }
            }
            timer.start()
        }
        editIP++
        if (editIP == 10) {
            val exampleDialog = ExampleDialog()
            exampleDialog.show(supportFragmentManager, "IP")
        }
    }

    fun rateButtonClick(view: View){
        val rate = findViewById<RatingBar>(R.id.ratingBar)
        if(rate.rating>0) shouldDecline = true
        CoroutineScope(Dispatchers.IO).launch {
            if(rate.rating>0) {
                clientSend(address, port, rate.rating.toString())
                clientGet(address, port)
            }
        }
    }

    fun declineButtonClick(view: View) {
        CoroutineScope(Dispatchers.IO).launch {
            clientSend(address, port, "0.0")
            clientGet(address, port)
        }
    }

    private suspend fun clientSend(address: String, port: Int, value: String) {
        var message = ByteArray(8)
        var connection = Socket(address, port)
        var reader = DataInputStream(connection.getInputStream())
        var send = true
        runOnUiThread {
            val disappearingAnim = AnimationUtils.loadAnimation(this, R.anim.disappearing)
            imageEmployee.startAnimation(disappearingAnim)
            text1.startAnimation(disappearingAnim)
            text2.startAnimation(disappearingAnim)
            textEmployee.startAnimation(disappearingAnim)
            ratingBar.startAnimation(disappearingAnim)
            rateBtn.startAnimation(disappearingAnim)
            declineBtn.startAnimation(disappearingAnim)
            imageEmployee.visibility = View.INVISIBLE
            text1.visibility = View.INVISIBLE
            text2.visibility = View.INVISIBLE
            textEmployee.visibility = View.INVISIBLE
            ratingBar.visibility = View.INVISIBLE
            rateBtn.visibility = View.INVISIBLE
            declineBtn.visibility = View.INVISIBLE

            val appearanceAnim = AnimationUtils.loadAnimation(this, R.anim.appearance)
            logoImg.startAnimation(appearanceAnim)
            logoImg.visibility = View.VISIBLE
        }
        while (send) {
            var writer = connection.getOutputStream()
            writer.write(value.toByteArray())
            reader.read(message, 0, message.size)
            if (String(message) == "Received") {
                writer.close()
                reader.close()
                connection.close()
                send = false
            }
        }
    }

    private suspend fun clientGet(address: String, port: Int) {
        shouldDecline = false
        var disconnected = true
        while (disconnected) {
            try {
                var check = Socket()
                var a: SocketAddress = InetSocketAddress(address, port)
                check.connect(a, 1500)
                check.close()
                break
            } catch (ex : Exception) {

            }
        }
        /*var connection = Socket()
        var a: SocketAddress = InetSocketAddress(address, port)
        connection.connect(a, 5000)*/
        runOnUiThread {
            var image = findViewById<ImageView>(R.id.imageEmployee)
            image.setImageResource(R.drawable.ic_doctor)
        }
        var connection = Socket(address, port)
        var writer = connection.getOutputStream()
        var reader = DataInputStream(connection.getInputStream())
        var message = ByteArray(9)
        var sizePicture: Int
        while (String(message) != "Connected") {
            writer.write("Connect".toByteArray())
            reader.read(message, 0, message.size)
            if (String(message) == "Connected") {
                writer.close()
                reader.close()
                connection.close()
                active = true
            }
        }
        connection = Socket(address, port)
        writer = connection.getOutputStream()
        reader = DataInputStream(connection.getInputStream())
        writer.write("GO".toByteArray())
        var bytesRead: Int
        var picSize = ByteArray(10)
        bytesRead = reader.read(picSize, 0, picSize.size)
        sizePicture = String(picSize).toInt()
        Log.e("SIZE", sizePicture.toString())
        writer.close()
        reader.close()
        connection.close()
        var pic = ByteArray(sizePicture)
        var picCounter: Int = 0;
        var good = true
        while (picCounter < pic.size) {
            connection = Socket(address, port)
            writer = connection.getOutputStream()
            reader = DataInputStream(connection.getInputStream())
            if (good) {
                writer.write("IMAGE".toByteArray())
            } else {
                writer.write("ERRORIMAGE".toByteArray())
            }
            var bytesCount: Int
            var partSize = ByteArray(1024)
            bytesCount = reader.read(partSize, 0, partSize.size)
            if (bytesCount == 1024 || bytesCount == (pic.size % 1024)) {
                for (i: Int in 0..bytesCount - 1) {
                    if (picCounter < pic.size) {
                        pic[picCounter] = partSize[i]
                        picCounter++
                    }
                    else {
                        break
                    }
                }
                good = true
            } else {
                good = false
            }
            writer.close()
            reader.close()
            connection.close()
        }
        runOnUiThread {
            if (pic.size > 1) {
                var image = findViewById<ImageView>(R.id.imageEmployee)
                image.setImageBitmap(getImage.StringToBitMap(pic, picCounter))
            }
            else {
                var image = findViewById<ImageView>(R.id.imageEmployee)
                image.setImageResource(R.drawable.ic_doctor)
            }
        }
        connection = Socket(address, port)
        writer = connection.getOutputStream()
        var textreader = Scanner(connection.getInputStream())
        var textmessage: String? = null
        do {
            writer.write("getText".toByteArray())
            if(textreader.hasNextLine())
                textmessage = textreader.nextLine()
        }while (textmessage == null)
        writer.close()
        reader.close()
        connection.close()
        runOnUiThread {
            val text = findViewById<TextView>(R.id.textEmployee)
            text.text = textmessage
        }
        runOnUiThread {
            ratingBar.rating = 0.0f
            val disappearingAnim = AnimationUtils.loadAnimation(this, R.anim.disappearing)
            logoImg.startAnimation(disappearingAnim)
            logoImg.visibility = View.INVISIBLE

            val appearanceAnim = AnimationUtils.loadAnimation(this, R.anim.appearance)
            imageEmployee.startAnimation(appearanceAnim)
            text1.startAnimation(appearanceAnim)
            text2.startAnimation(appearanceAnim)
            textEmployee.startAnimation(appearanceAnim)
            ratingBar.startAnimation(appearanceAnim)
            rateBtn.startAnimation(appearanceAnim)
            declineBtn.startAnimation(appearanceAnim)
            imageEmployee.visibility = View.VISIBLE
            text1.visibility = View.VISIBLE
            text2.visibility = View.VISIBLE
            textEmployee.visibility = View.VISIBLE
            ratingBar.visibility = View.VISIBLE
            rateBtn.visibility = View.VISIBLE
            declineBtn.visibility = View.VISIBLE
        }
    }

    override fun applyTexts(address: String?) {
        val myPreferences = PreferenceManager.getDefaultSharedPreferences(this@MainActivity)
        val myEditor: Editor = myPreferences.edit()
        myEditor.putString("IP", address)
        myEditor.commit()
        this.address = address!!
        Toast.makeText(this, "Перезапустите приложение для применения настроек", Toast.LENGTH_LONG).show()
    }
}