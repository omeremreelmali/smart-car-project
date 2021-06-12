package com.example.smartcarproject;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

public class HomePage extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home_page);
    }
    public void aracyonetimbtnClick(View v){
        Intent aracyonetim= new Intent(getApplicationContext(),AracYonetimi.class);
        startActivity(aracyonetim);
    }
    public void istekbtnClick(View v){
        Intent aracyonetim= new Intent(getApplicationContext(),YeniEkle.class);
        startActivity(aracyonetim);
    }
    public void profilyonetimbtnClick(View v){
        Intent aracyonetim= new Intent(getApplicationContext(),ProfilYonetimi.class);
        startActivity(aracyonetim);
    }
}
