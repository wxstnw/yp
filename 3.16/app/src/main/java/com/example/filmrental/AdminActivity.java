package com.example.filmrental;

import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.view.View;
import android.widget.*;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import java.util.ArrayList;

public class AdminActivity extends AppCompatActivity {

    private EditText usernameEditText, passwordEditText;
    private Spinner roleSpinner;
    private Button addButton, deleteButton, logoutButton;
    private RecyclerView usersRecyclerView;

    private DatabaseHelper dbHelper;
    private UsersAdapter adapter;
    private ArrayList<User> usersList = new ArrayList<>();
    private int selectedUserId = -1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_admin);

        dbHelper = new DatabaseHelper(this);

        usernameEditText = findViewById(R.id.editTextUsername);
        passwordEditText = findViewById(R.id.editTextPassword);
        roleSpinner = findViewById(R.id.spinnerRole);
        addButton = findViewById(R.id.buttonAddUser);
        deleteButton = findViewById(R.id.buttonDeleteUser);
        logoutButton = findViewById(R.id.buttonLogout);
        usersRecyclerView = findViewById(R.id.recyclerViewUsers);

        roleSpinner.setAdapter(new ArrayAdapter<>(this, android.R.layout.simple_spinner_dropdown_item, new String[]{"admin", "seller"}));

        usersRecyclerView.setLayoutManager(new LinearLayoutManager(this));
        adapter = new UsersAdapter(usersList, new UsersAdapter.OnUserClickListener() {
            @Override
            public void onUserClick(User user) {
                selectedUserId = user.id;
                usernameEditText.setText(user.username);
                roleSpinner.setSelection(user.role.equals("admin") ? 0 : 1);
            }
        });
        usersRecyclerView.setAdapter(adapter);

        loadUsers();

        addButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                addUser();
            }
        });

        deleteButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                deleteUser();
            }
        });

        // ✅ Новый обработчик кнопки "Выход"
        logoutButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(AdminActivity.this, LoginActivity.class);
                intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
                startActivity(intent);
                finish();
            }
        });
    }

    private void loadUsers() {
        usersList.clear();
        SQLiteDatabase db = dbHelper.getReadableDatabase();
        Cursor cursor = db.rawQuery("SELECT Id, Username, Role FROM Users", null);
        while (cursor.moveToNext()) {
            usersList.add(new User(
                    cursor.getInt(0),
                    cursor.getString(1),
                    cursor.getString(2)
            ));
        }
        cursor.close();
        db.close();
        adapter.notifyDataSetChanged();
    }

    private void addUser() {
        String username = usernameEditText.getText().toString().trim();
        String password = passwordEditText.getText().toString().trim();
        String role = roleSpinner.getSelectedItem().toString();

        if (username.isEmpty() || password.isEmpty()) {
            Toast.makeText(this, "Заполните все поля", Toast.LENGTH_SHORT).show();
            return;
        }

        SQLiteDatabase db = dbHelper.getWritableDatabase();
        try {
            db.execSQL("INSERT INTO Users (Username, Password, Role) VALUES (?, ?, ?)",
                    new Object[]{username, password, role});
            Toast.makeText(this, "Пользователь добавлен", Toast.LENGTH_SHORT).show();
            usernameEditText.setText("");
            passwordEditText.setText("");
            roleSpinner.setSelection(0);
            loadUsers();
        } catch (Exception e) {
            Toast.makeText(this, "Ошибка: " + e.getMessage(), Toast.LENGTH_SHORT).show();
        }
        db.close();
    }

    private void deleteUser() {
        if (selectedUserId == -1) {
            Toast.makeText(this, "Выберите пользователя", Toast.LENGTH_SHORT).show();
            return;
        }

        SQLiteDatabase db = dbHelper.getWritableDatabase();
        db.execSQL("DELETE FROM Users WHERE Id = ?", new Object[]{selectedUserId});
        db.close();

        Toast.makeText(this, "Пользователь удалён", Toast.LENGTH_SHORT).show();
        selectedUserId = -1;
        loadUsers();
    }
}
