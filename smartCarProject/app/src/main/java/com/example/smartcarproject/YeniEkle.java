package com.example.smartcarproject;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.support.v4.app.Fragment;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import android.support.v4.app.FragmentTransaction;

public class YeniEkle extends AppCompatActivity {

    Button eklemeturuButton;
    FragmentTransaction fragmentTransaction;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_yeni_ekle);
        eklemeturuButton= (Button)findViewById(R.id.eklemeTuruBtn);
        changeFragment(new AracEkle());
    }
    public void geribtnClick(View v){
        Intent geri= new Intent(getApplicationContext(),HomePage.class);
        startActivity(geri);
    }
    public void istekBtnClick(View v){
        changeFragment(new istekTalebi());
    }
    int sayac = 1;
    public void yenieklemeturubtnClick(View v){

        String butonText= eklemeturuButton.getText().toString();
        if (sayac==0)
        {
            eklemeturuButton.setText("Araç Düzenle");
            sayac++;
            changeFragment(new AracEkle());
        }
        else if(sayac==1){
            eklemeturuButton.setText("Araç Ekle");
            sayac--;
            changeFragment(new AracDuzenle());
        }
    }

    public void changeFragment(Fragment fragment){
        FragmentTransaction fragmentTransaction = getSupportFragmentManager().beginTransaction();
        fragmentTransaction.replace(R.id.fragmentLay,fragment);
        fragmentTransaction.commit();
    }
}
