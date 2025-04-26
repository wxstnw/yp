package com.example.filmrental;

import android.content.ContentValues;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.view.View;
import android.widget.*;
import androidx.appcompat.app.AppCompatActivity;

public class ClientEditActivity extends AppCompatActivity {

    private EditText nameEditText, phoneEditText, emailEditText;
    private Button saveButton;

    private DatabaseHelper dbHelper;
    private int clientId = -1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_client_edit);

        dbHelper = new DatabaseHelper(this);

        nameEditText = findViewById(R.id.editTextName);
        phoneEditText = findViewById(R.id.editTextPhone);
        emailEditText = findViewById(R.id.editTextEmail);
        saveButton = findViewById(R.id.buttonSaveClient);

        if (getIntent().hasExtra("clientId")) {
            clientId = getIntent().getIntExtra("clientId", -1);
            loadClientData();
        }

        saveButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                saveClient();
            }
        });
    }

    private void loadClientData() {
        SQLiteDatabase db = dbHelper.getReadableDatabase();
        Cursor cursor = db.rawQuery("SELECT * FROM Clients WHERE Id = ?", new String[]{String.valueOf(clientId)});
        if (cursor.moveToFirst()) {
            nameEditText.setText(cursor.getString(cursor.getColumnIndexOrThrow("FullName")));
            phoneEditText.setText(cursor.getString(cursor.getColumnIndexOrThrow("Phone")));
            emailEditText.setText(cursor.getString(cursor.getColumnIndexOrThrow("Email")));
        }
        cursor.close();
        db.close();
    }

    private void saveClient() {
        String name = nameEditText.getText().toString().trim();
        String phone = phoneEditText.getText().toString().trim();
        String email = emailEditText.getText().toString().trim();

        if (name.isEmpty() || phone.isEmpty()) {
            Toast.makeText(this, "Имя и телефон обязательны", Toast.LENGTH_SHORT).show();
            return;
        }

        SQLiteDatabase db = dbHelper.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put("FullName", name);
        values.put("Phone", phone);
        values.put("Email", email);

        if (clientId == -1) {
            db.insert("Clients", null, values);
            Toast.makeText(this, "Клиент добавлен", Toast.LENGTH_SHORT).show();
        } else {
            db.update("Clients", values, "Id = ?", new String[]{String.valueOf(clientId)});
            Toast.makeText(this, "Клиент обновлён", Toast.LENGTH_SHORT).show();
        }

        db.close();
        finish();
    }
}
