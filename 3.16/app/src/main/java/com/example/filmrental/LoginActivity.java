package com.example.filmrental;

import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.*;

import androidx.appcompat.app.AppCompatActivity;

public class LoginActivity extends AppCompatActivity {

    private EditText usernameEditText, passwordEditText;
    private Button loginButton, exitButton;

    private DatabaseHelper dbHelper;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        dbHelper = new DatabaseHelper(this);

        // Логи для проверки базы
        String dbPath = getDatabasePath(DatabaseHelper.DATABASE_NAME).getAbsolutePath();
        Log.d("DB_DEBUG", "База находится по пути: " + dbPath);
        Log.d("DB_DEBUG", "Размер: " + new java.io.File(dbPath).length());

        usernameEditText = findViewById(R.id.editTextUsername);
        passwordEditText = findViewById(R.id.editTextPassword);
        loginButton = findViewById(R.id.buttonLogin);
        exitButton = findViewById(R.id.buttonExit);

        loginButton.setOnClickListener(view -> login());
        exitButton.setOnClickListener(v -> finishAffinity());
    }

    private void login() {
        String username = usernameEditText.getText().toString().trim();
        String password = passwordEditText.getText().toString().trim();

        Log.d("LOGIN_INPUT", "Введено имя: [" + username + "] пароль: [" + password + "]");

        if (username.isEmpty() || password.isEmpty()) {
            Toast.makeText(this, "Введите имя пользователя и пароль", Toast.LENGTH_SHORT).show();
            return;
        }

        SQLiteDatabase db = dbHelper.getReadableDatabase();

        // Проверка содержимого базы
        Cursor debugCursor = db.rawQuery("SELECT Username, Password, Role FROM Users", null);
        while (debugCursor.moveToNext()) {
            Log.d("USER_ROW", "Username: " + debugCursor.getString(0)
                    + " | Password: " + debugCursor.getString(1)
                    + " | Role: " + debugCursor.getString(2));
        }
        debugCursor.close();

        // Основной запрос
        Cursor cursor = db.rawQuery(
                "SELECT Role FROM Users WHERE Username = ? AND Password = ?",
                new String[]{username, password}
        );

        Log.d("LOGIN_DEBUG", "Найдено строк: " + cursor.getCount());

        if (cursor.moveToFirst()) {
            String role = cursor.getString(0);
            Toast.makeText(this, "Добро пожаловать, " + username + "! Роль: " + role, Toast.LENGTH_SHORT).show();

            if (role.equals("admin")) {
                startActivity(new Intent(this, AdminActivity.class));
            } else if (role.equals("seller")) {
                Intent intent = new Intent(this, MainActivity.class);
                intent.putExtra("userRole", role);
                startActivity(intent);
            }

            finish();
        } else {
            Toast.makeText(this, "Неверное имя пользователя или пароль", Toast.LENGTH_SHORT).show();
        }

        cursor.close();
        db.close();
    }
}
