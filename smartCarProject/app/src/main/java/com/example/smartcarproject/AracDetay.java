package com.example.smartcarproject;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.os.CountDownTimer;

import java.io.IOException;

import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.TextView;

import android.widget.Toast;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;
import com.squareup.okhttp.Call;
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

public class AracDetay extends AppCompatActivity implements  OnMapReadyCallback {
    public static int aracId ,guncelHiz, aracDurum, aracDurdurma,stop_state;
    public static String aracPlaka;
    public  LatLng konum;
    GoogleMap mGoogleMap;
    TextView hiz, plaka,durum;
    Button durdurmaButton;
    CountDownTimer countDownTimer;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_arac_detay);

        hiz=(TextView)findViewById(R.id.arac_hiz_label);
        plaka=(TextView)findViewById(R.id.arac_detay_plaka);
        durum=(TextView)findViewById(R.id.arac_durum_label);
        durdurmaButton=(Button) findViewById(R.id.durdur_btn);
        plaka.setText(aracPlaka);
        aracDetayGetir();


        countDownTimer =new CountDownTimer(30000,1500) {
            @Override
            public void onTick(long millisUntilFinished) {
               aracDetayGetir();
               detayGuncelle();
               onResume();
            }
            @Override
            public void onFinish() {
                timerStart();
            }
        };


            if (aracDurdurma==0)
            {
                durdurmaButton.setText("Aracı Durdur");
            }
            else if(aracDurdurma==1){
                durdurmaButton.setText("Araç kilidini kaldır.");
            }

        SupportMapFragment mapFragment=(SupportMapFragment) getSupportFragmentManager().findFragmentById(R.id.arac_detay_map);
        mapFragment.getMapAsync(this);


        timerStart();
    }

    public void timerStart()
    {
        countDownTimer.start();
    }

    public void aracDetayGetir(){
        OkHttpClient client = new OkHttpClient();

        String json = "{\"id\": \""+aracId+"\"}";
        String url = getString(R.string.localhostip)+":4500/api/cardetail/";
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
                        JSONArray aracArray = json.getJSONArray("data");
                        JSONObject aracDetayi = aracArray.getJSONObject(0);
                        AracDetay.guncelHiz = aracDetayi.getInt("speed");
                        AracDetay.aracDurum = aracDetayi.getInt("vehicle_state");
                        AracDetay.aracDurdurma = aracDetayi.getInt("stop_state");
                        konum=new LatLng(aracDetayi.getInt("enlem"),aracDetayi.getInt("boylam"));

                    }catch (JSONException e) {
                        e.printStackTrace();
                    }
                }
            }
        });
    }


    public void detayGuncelle(){
        hiz.setText(String.valueOf(guncelHiz)+ " KM/h");


        if (aracDurum==0){
                durum.setText("Aracınızın durumu stabil");
            //durum.setTextColor(0x00FF00);
        }
        else if(aracDurum==1){
            durum.setText("Araç kaza yaptı.");
        }
    }

    public void geriButton(View v){
        countDownTimer.cancel();
        Intent geri= new Intent(getApplicationContext(),AracYonetimi.class);
        startActivity(geri);
    }

    public void stopCar(View v){
        OkHttpClient client = new OkHttpClient();

        if (aracDurdurma==0)
        {
            stop_state=1;
        }
        else{
            stop_state=0;
        }
        String json = "{\"id\": \""+aracId+"\","+"\"stop_state\": \""+stop_state+"\"}";
        String url = getString(R.string.localhostip)+":4500/api/cardetail/stopcar";
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
                    if (stop_state==0){
                        durdurmaButton.setText("Aracı Durdur");
                    }
                    else {
                        durdurmaButton.setText("Araç kilidini kaldır.");
                    }
                }
            }
        });
    }


    @Override
    public void onMapReady(GoogleMap googleMap) {
        mGoogleMap=googleMap;
        if (konum!=null)
        {
            googleMap.addMarker(new MarkerOptions().position(konum).title("Buradasınız"));
            googleMap.moveCamera(CameraUpdateFactory.newLatLng(konum));
            googleMap.setMinZoomPreference(8.0f);
        }
    }
    @Override
    public void onResume() {
        super.onResume();

        if(mGoogleMap != null){ //prevent crashing if the map doesn't exist yet (eg. on starting activity)
            mGoogleMap.clear();
            mGoogleMap.addMarker(new MarkerOptions().position(konum).title("Buradasınız"));
            mGoogleMap.moveCamera(CameraUpdateFactory.newLatLng(konum));
            mGoogleMap.setMinZoomPreference(8.0f);
            // add markers from database to the map
        }
    }



}