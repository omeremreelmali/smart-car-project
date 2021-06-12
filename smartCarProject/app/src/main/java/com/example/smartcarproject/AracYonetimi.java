package com.example.smartcarproject;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.ListView;

import com.example.smartcarproject.aracliste.Arac;
import com.example.smartcarproject.aracliste.CustomAdapter;
import com.squareup.okhttp.Callback;
import com.squareup.okhttp.MediaType;
import com.squareup.okhttp.OkHttpClient;
import com.squareup.okhttp.Request;
import com.squareup.okhttp.RequestBody;
import com.squareup.okhttp.Response;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class AracYonetimi extends AppCompatActivity {

    final List<Arac> araclar = new ArrayList<Arac>();
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_arac_yonetimi);

        OkHttpClient client = new OkHttpClient();
        int id = Login.userId;

        String url = getString(R.string.localhostip)+":4500/api/account/"+id;

        Request request = new Request.Builder()
                .url(url)
                .build();

        client.newCall(request).enqueue(new Callback() {
            @Override
            public void onFailure(Request request, IOException e) {
                Log.e("httpservice","failed");
                e.printStackTrace();
            }

            @Override
            public void onResponse(Response response) throws IOException {
                if(response.isSuccessful()) {

                    try {
                        JSONObject json = new JSONObject(response.body().string());
                      for(int i=0; i<json.getJSONArray("data").length(); i++){
                          JSONObject car = json.getJSONArray("data").getJSONObject(i);
                          String plate = car.getString("plate").toString();
                          int vehicleId=car.getInt("id");
                          araclar.add(new Arac(plate,vehicleId));
                      }
                    } catch (JSONException e) {
                        e.printStackTrace();
                    }

                }
            }
        });

        final ListView listView = (ListView) findViewById(R.id.araclistView);
        CustomAdapter adapter = new CustomAdapter(this, araclar);
        listView.setAdapter(adapter);
    }
    public void geribtnClick(View v){
        Intent geri= new Intent(getApplicationContext(),HomePage.class);
        startActivity(geri);
    }
    public void yenieklebtnClick(View v){
        Intent yeniekle= new Intent(getApplicationContext(),YeniEkle.class);
        startActivity(yeniekle);
    }
}
