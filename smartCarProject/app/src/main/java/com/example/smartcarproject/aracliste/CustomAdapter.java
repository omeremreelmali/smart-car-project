package com.example.smartcarproject.aracliste;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.example.smartcarproject.AracDetay;
import com.example.smartcarproject.AracYonetimi;
import com.example.smartcarproject.HaritaYonetimi;
import com.example.smartcarproject.Login;
import com.example.smartcarproject.R;
import com.squareup.okhttp.Callback;
import com.squareup.okhttp.MediaType;
import com.squareup.okhttp.OkHttpClient;
import com.squareup.okhttp.Request;
import com.squareup.okhttp.RequestBody;
import com.squareup.okhttp.Response;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.util.List;

public class CustomAdapter extends BaseAdapter {
    private LayoutInflater aracInflater;
    private List<Arac> aracList;


    public CustomAdapter(Activity activity, List<Arac> userList) {
        aracInflater = (LayoutInflater) activity.getSystemService(
                Context.LAYOUT_INFLATER_SERVICE);
        this.aracList = userList;
    }
    @Override
    public int getCount() {
        return aracList.size();
    }

    @Override
    public Object getItem(int i) {
        return aracList.get(i);
    }

    @Override
    public long getItemId(int i) {
        return i;
    }

    @Override
    public View getView(final int i, View view, ViewGroup viewGroup) {
        final View lineView;
        lineView = aracInflater.inflate(R.layout.arac_custom_layout, null);
        Button durdurButton= (Button) lineView.findViewById(R.id.durdur_button);
        Button konumButton=(Button) lineView.findViewById(R.id.konum_button);
        TextView textViewAracPlaka = (TextView) lineView.findViewById(R.id.arac_custom_plaka);
        Arac arac= aracList.get(i);
        textViewAracPlaka.setText(arac.getAracPlaka());

        durdurButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                AracDetay.aracId=aracList.get(i).getAracId();
                AracDetay.aracPlaka=aracList.get(i).getAracPlaka();
                Intent arac_detay=new Intent(lineView.getContext(), AracDetay.class);
                arac_detay.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                lineView.getContext().startActivity(arac_detay);

                /*Toast.makeText(lineView.getContext(),aracList.get(i).getAracPlaka() + " Plakalı Aracınız Durduruldu", Toast.LENGTH_SHORT).show();*/
            }
        });
        konumButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                HaritaYonetimi.vehicleID=aracList.get(i).getAracId();
                Intent harita=new Intent(lineView.getContext(), HaritaYonetimi.class);
                harita.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                lineView.getContext().startActivity(harita);
            }
        });


        return lineView;
    }
}
