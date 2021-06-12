package com.example.smartcarproject;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

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


/**
 * A simple {@link Fragment} subclass.
 * Use the {@link AracDuzenle#newInstance} factory method to
 * create an instance of this fragment.
 */
public class AracDuzenle extends Fragment {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    public AracDuzenle() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment AracDuzenle.
     */
    // TODO: Rename and change types and number of parameters
    public static AracDuzenle newInstance(String param1, String param2) {
        AracDuzenle fragment = new AracDuzenle();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            mParam1 = getArguments().getString(ARG_PARAM1);
            mParam2 = getArguments().getString(ARG_PARAM2);
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {


        final View myView=  inflater.inflate(R.layout.fragment_arac_duzenle, container, false);
        Button buttonTalep= (Button) myView.findViewById(R.id.talepOlusturBtn_d);


        buttonTalep.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                OkHttpClient client = new OkHttpClient();
                EditText aracplaka= (EditText) myView.findViewById(R.id.arac_plaka_d);
                EditText yakitturu= (EditText) myView.findViewById(R.id.arac_yakit_d);
                EditText renk= (EditText) myView.findViewById(R.id.arac_renk_d);
                EditText aracyeniplaka= (EditText) myView.findViewById(R.id.arac_yeni_plaka_d);

                if (aracplaka.getText().toString().trim().equals("") || renk.getText().toString().trim().equals("") || yakitturu.getText().toString().trim().equals("")){
                    Toast.makeText(myView.getContext(), "Boş alanları doldurunuz", Toast.LENGTH_SHORT).show();
                }
                else{
                    String json = "{\"plate\": \""+aracplaka.getText().toString()+"\"," + "\"fuel\": \""+yakitturu.getText().toString()+"\","+"\"color\": \""+renk.getText().toString()+"\","+"\"userid\": \""+Login.userId+"\","+"\"newplate\": \""+aracyeniplaka.getText().toString()+"\"}";
                    String url = getString(R.string.localhostip)+":4500/api/vforms/vehicleupdate";
                    RequestBody body = RequestBody.create(
                            MediaType.parse("application/json"), json);

                    final Request request = new Request.Builder()
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
                                    JSONObject successData = userArray.getJSONObject(0);
                                /*if (successs=="false"){
                                    baslik.setText("Hata");
                                }
                                else if(successs=="true"){
                                    baslik.setText("Başarılı");
                                }*/

                                }catch (JSONException e) {
                                    e.printStackTrace();
                                }

                            }
                        }
                    });

                    Toast.makeText(myView.getContext(), "Talebiniz Alındı.", Toast.LENGTH_SHORT).show();
                    aracplaka.setText("");
                    aracyeniplaka.setText("");
                    yakitturu.setText("");
                    renk.setText("");
                }


            }
        });







        return myView;

    }
}
