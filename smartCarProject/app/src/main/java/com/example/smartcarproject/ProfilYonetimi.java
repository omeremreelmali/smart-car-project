package com.example.smartcarproject;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.TextView;

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

public class ProfilYonetimi extends AppCompatActivity {


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profil_yonetimi);

        final TextView adsoyad=(TextView) findViewById(R.id.arac_detay_plaka);
        final TextView tc=(TextView) findViewById(R.id.tc_text);
        final TextView email=(TextView) findViewById(R.id.email_text);
        final TextView roltext=(TextView) findViewById(R.id.rol_text);
        final TextView telefon=(TextView) findViewById(R.id.phone_text);
        int id =Login.userId;


        OkHttpClient client = new OkHttpClient();

        String json = "{\"id\": \""+id+"\"}";
        String url = getString(R.string.localhostip)+":4500/api/user/userget";
        RequestBody body = RequestBody.create(
                MediaType.parse("application/json"), json);

        Request request = new Request.Builder()
                .url(url)
                .post(body)
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
                    try{
                        JSONObject json = new JSONObject(response.body().string());
                        JSONArray userArray = json.getJSONArray("data");
                        JSONObject user = userArray.getJSONObject(0);
                        String ad_Soyad= user.getString("name")+" "+ user.getString("surname");
                        adsoyad.setText(ad_Soyad);
                        email.setText(user.getString("email"));
                        tc.setText(user.getString("tc"));
                        if (user.getInt("role")==99){
                            roltext.setText("Yönetici");
                        }
                        else{
                            roltext.setText("Kullanıcı");
                        }
                        if (user.getString("phone")=="0"){
                            telefon.setText("Telefon Yok");
                        }
                        else{
                            telefon.setText(user.getString("phone"));
                        }

                    }catch (JSONException e) {
                        e.printStackTrace();
                    }
                }
            }
        });
    }

    public void geribtnClick(View v){
        Intent geri= new Intent(getApplicationContext(),HomePage.class);
        startActivity(geri);
    }
}
