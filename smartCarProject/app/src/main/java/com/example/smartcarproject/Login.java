package com.example.smartcarproject;

import android.content.Intent;
import android.content.SharedPreferences;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

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

public class Login extends AppCompatActivity {
    private TextView name;
    private TextView password;
    public static int userId;



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        name = (TextView) findViewById(R.id.et_Email);
        password = (TextView) findViewById(R.id.et_pass);
    }


    public void btnClick(View v) throws IOException{
        String userName;
        String userPass;
        userName = name.getText().toString();
        userPass = password.getText().toString();

        OkHttpClient client = new OkHttpClient();

        String json = "{\"tc\": \""+userName+"\","+ "\"code\": \""+userPass+"\"}";
        String url = getString(R.string.localhostip)+":4500/api/user/";
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
                       Login.userId = user.getInt("id");

                   }catch (JSONException e) {
                       e.printStackTrace();
                   }
                   Intent homepageactivity = new Intent(getApplicationContext(), HomePage.class);
                   startActivity(homepageactivity);
               }
           }
       });



        }
}
