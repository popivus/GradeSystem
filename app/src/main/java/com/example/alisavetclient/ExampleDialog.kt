package com.example.alisavetclient

import android.app.AlertDialog
import android.app.Dialog
import android.content.Context
import android.os.Bundle
import android.view.View
import android.widget.EditText
import androidx.appcompat.app.AppCompatDialogFragment
import java.lang.ClassCastException
class ExampleDialog : AppCompatDialogFragment() {
    private var editTextIP:EditText?=null
    private var listener: ExampleDialogListener?=null
    override fun onCreateDialog(savedInstanceState: Bundle?): Dialog {
        val builder = AlertDialog.Builder(activity)//builder для создания окна
        val inflater = activity!!.layoutInflater
        val view: View = inflater.inflate(R.layout.dialog_one, null)
        builder.setView(view)
            .setTitle("Введите IP компьютера администратора").setNegativeButton("Отмена"){dialogInterface, i->}.setPositiveButton("Ok"){dialogInterface, i->
                val address = editTextIP!!.text.toString()
                listener!!.applyTexts(address)
            }
        editTextIP = view.findViewById(R.id.edtAddress)
        return builder.create()
    }
    override fun onAttach(context: Context) {
        super.onAttach(context)
        listener = try{
            context as ExampleDialogListener }
        catch (e:ClassCastException){
            throw ClassCastException(
                context.toString()+"must implement ExampleDialogListener"
            )
        }
    }
    interface ExampleDialogListener{
        fun applyTexts(
            address:String?
        )
    }
}