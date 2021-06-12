package com.example.smartcarproject;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

public class KullaniciYonetimi extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_kullanici_yonetimi);
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
