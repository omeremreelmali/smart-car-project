package com.example.smartcarproject;

import android.Manifest;
import android.app.FragmentTransaction;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Build;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;

import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;
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

public class HaritaYonetimi extends AppCompatActivity  implements  OnMapReadyCallback{


    public  static int vehicleID;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_harita_yonetimi);

        konumGetir();
        SupportMapFragment mapFragment = (SupportMapFragment) getSupportFragmentManager().findFragmentById(R.id.map);
        mapFragment.getMapAsync(this);

    }

    public LatLng konum;
    public void konumGetir(){

        OkHttpClient client = new OkHttpClient();
        String url = getString(R.string.localhostip)+":4500/api/map/"+vehicleID;


        final Request request = new Request.Builder()
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
                    try{
                        JSONObject json = new JSONObject(response.body().string());
                        JSONArray userArray = json.getJSONArray("data");
                        JSONObject successData = userArray.getJSONObject(0);
                        konum=new LatLng(successData.getInt("enlem"),successData.getInt("boylam"));
                    }catch (JSONException e) {
                        e.printStackTrace();
                    }
                }
            }
        });
    }
    @Override
    public void onMapReady(GoogleMap googleMap) {
        googleMap.addMarker(new MarkerOptions().position(konum).title("Buradasınız"));
        googleMap.moveCamera(CameraUpdateFactory.newLatLng(konum));
        googleMap.setMinZoomPreference(8.0f);
    }

    public void geribtnClick(View v){
        Intent geri= new Intent(getApplicationContext(),HomePage.class);
        startActivity(geri);
    }


}
