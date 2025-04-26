package com.example.filmrental;

import android.content.ContentValues;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import java.util.ArrayList;

public class FilmsActivity extends AppCompatActivity {

    private RecyclerView recyclerView;
    private Button addButton, editButton, deleteButton, testAddButton;
    private FilmsAdapter adapter;
    private ArrayList<Film> filmList = new ArrayList<>();
    private int selectedFilmId = -1;

    private DatabaseHelper dbHelper;
    private String userRole;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_films);

        dbHelper = new DatabaseHelper(this);
        userRole = getIntent().getStringExtra("userRole");

        recyclerView = findViewById(R.id.recyclerViewFilms);
        addButton = findViewById(R.id.buttonAddFilm);
        editButton = findViewById(R.id.buttonEditFilm);
        deleteButton = findViewById(R.id.buttonDeleteFilm);
        testAddButton = findViewById(R.id.buttonTestAddFilm); // кнопка теста

        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        adapter = new FilmsAdapter(filmList, film -> selectedFilmId = film.id);
        recyclerView.setAdapter(adapter);

        if (!"seller".equals(userRole)) {
            addButton.setVisibility(View.GONE);
            editButton.setVisibility(View.GONE);
            deleteButton.setVisibility(View.GONE);
            testAddButton.setVisibility(View.GONE);
        }

        addButton.setOnClickListener(v -> {
            startActivity(new Intent(this, FilmEditActivity.class));
        });

        editButton.setOnClickListener(v -> {
            if (selectedFilmId == -1) {
                Toast.makeText(this, "Выберите фильм", Toast.LENGTH_SHORT).show();
            } else {
                Intent intent = new Intent(this, FilmEditActivity.class);
                intent.putExtra("filmId", selectedFilmId);
                startActivity(intent);
            }
        });

        deleteButton.setOnClickListener(v -> {
            if (selectedFilmId == -1) {
                Toast.makeText(this, "Выберите фильм", Toast.LENGTH_SHORT).show();
            } else {
                SQLiteDatabase db = dbHelper.getWritableDatabase();
                db.execSQL("DELETE FROM Films WHERE Id = ?", new Object[]{selectedFilmId});
                db.close();
                Toast.makeText(this, "Фильм удалён", Toast.LENGTH_SHORT).show();
                selectedFilmId = -1;
                loadFilms();
            }
        });

        // ✅ ЛОГИКА тестовой вставки
        testAddButton.setOnClickListener(v -> {
            SQLiteDatabase db = dbHelper.getWritableDatabase();

            ContentValues values = new ContentValues();
            values.put("Title", "Тестовый фильм");
            values.put("Genre", "Тест");
            values.put("Year", 2025);
            values.put("Duration", "120 мин");
            values.put("Description", "Это тестовая запись");
            values.put("Status", "Доступен");

            long result = db.insert("Films", null, values);
            Log.d("DB_WRITE", "Результат вставки: " + result);

            if (result != -1) {
                Toast.makeText(FilmsActivity.this, "Тестовый фильм добавлен", Toast.LENGTH_SHORT).show();
            } else {
                Toast.makeText(FilmsActivity.this, "Ошибка при добавлении", Toast.LENGTH_SHORT).show();
            }

            db.close();
            loadFilms();
        });
    }

    @Override
    protected void onResume() {
        super.onResume();
        loadFilms();
    }

    private void loadFilms() {
        filmList.clear();
        SQLiteDatabase db = dbHelper.getReadableDatabase();
        Cursor cursor = db.rawQuery("SELECT Id, Title, Genre, Year, Status FROM Films", null);
        while (cursor.moveToNext()) {
            filmList.add(new Film(
                    cursor.getInt(0),
                    cursor.getString(1),
                    cursor.getString(2),
                    cursor.getInt(3),
                    cursor.getString(4)
            ));
        }
        cursor.close();
        db.close();
        adapter.notifyDataSetChanged();
    }
}
