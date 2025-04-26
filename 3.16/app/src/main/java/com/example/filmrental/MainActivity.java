package com.example.filmrental;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import androidx.appcompat.app.AppCompatActivity;

public class MainActivity extends AppCompatActivity {

    private String userRole;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        userRole = getIntent().getStringExtra("userRole");

        setTitle("Главное меню (" + userRole + ")");

        Button btnCatalog = findViewById(R.id.buttonCatalog);
        Button btnClients = findViewById(R.id.buttonClients);
        Button btnRent = findViewById(R.id.buttonRent);
        Button btnReturn = findViewById(R.id.buttonReturn);
        Button btnExit = findViewById(R.id.buttonExit);

        btnCatalog.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(MainActivity.this, FilmsActivity.class);
intent.putExtra("userRole", userRole);
startActivity(intent);
            }
        });

        btnClients.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(MainActivity.this, ClientsActivity.class));
            }
        });

        btnRent.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(MainActivity.this, RentActivity.class));
            }
        });

        btnReturn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(MainActivity.this, ReturnActivity.class));
            }
        });

        btnExit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finishAffinity(); // Закрыть приложение
            }
        });
    }
}
