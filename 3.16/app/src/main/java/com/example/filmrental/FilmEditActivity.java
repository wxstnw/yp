package com.example.filmrental;

import android.content.ContentValues;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.view.View;
import android.widget.*;
import androidx.appcompat.app.AppCompatActivity;

public class FilmEditActivity extends AppCompatActivity {

    private EditText titleEditText, genreEditText, yearEditText, durationEditText, descriptionEditText;
    private Spinner statusSpinner;
    private Button saveButton;

    private DatabaseHelper dbHelper;
    private int filmId = -1; // -1 значит новый фильм

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_film_edit);

        dbHelper = new DatabaseHelper(this);

        titleEditText = findViewById(R.id.editTextTitle);
        genreEditText = findViewById(R.id.editTextGenre);
        yearEditText = findViewById(R.id.editTextYear);
        durationEditText = findViewById(R.id.editTextDuration);
        descriptionEditText = findViewById(R.id.editTextDescription);
        statusSpinner = findViewById(R.id.spinnerStatus);
        saveButton = findViewById(R.id.buttonSave);

        statusSpinner.setAdapter(new ArrayAdapter<>(this,
                android.R.layout.simple_spinner_dropdown_item,
                new String[]{"Доступен", "Занят", "Удалён"}));

        if (getIntent().hasExtra("filmId")) {
            filmId = getIntent().getIntExtra("filmId", -1);
            loadFilmData();
        }

        saveButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveFilm();
            }
        });
    }

    private void loadFilmData() {
        SQLiteDatabase db = dbHelper.getReadableDatabase();
        Cursor cursor = db.rawQuery("SELECT * FROM Films WHERE Id = ?", new String[]{String.valueOf(filmId)});
        if (cursor.moveToFirst()) {
            titleEditText.setText(cursor.getString(cursor.getColumnIndexOrThrow("Title")));
            genreEditText.setText(cursor.getString(cursor.getColumnIndexOrThrow("Genre")));
            yearEditText.setText(cursor.getString(cursor.getColumnIndexOrThrow("Year")));
            durationEditText.setText(cursor.getString(cursor.getColumnIndexOrThrow("Duration")));
            descriptionEditText.setText(cursor.getString(cursor.getColumnIndexOrThrow("Description")));

            String status = cursor.getString(cursor.getColumnIndexOrThrow("Status"));
            ArrayAdapter adapter = (ArrayAdapter) statusSpinner.getAdapter();
            int position = adapter.getPosition(status);
            statusSpinner.setSelection(position);
        }
        cursor.close();
        db.close();
    }

    private void saveFilm() {
        String title = titleEditText.getText().toString().trim();
        String genre = genreEditText.getText().toString().trim();
        String yearStr = yearEditText.getText().toString().trim();
        String duration = durationEditText.getText().toString().trim();
        String description = descriptionEditText.getText().toString().trim();
        String status = statusSpinner.getSelectedItem().toString();

        if (title.isEmpty() || genre.isEmpty() || yearStr.isEmpty() || duration.isEmpty()) {
            Toast.makeText(this, "Заполните обязательные поля", Toast.LENGTH_SHORT).show();
            return;
        }

        SQLiteDatabase db = dbHelper.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put("Title", title);
        values.put("Genre", genre);
        values.put("Year", Integer.parseInt(yearStr));
        values.put("Duration", duration);
        values.put("Description", description);
        values.put("Status", status);

        if (filmId == -1) {
            db.insert("Films", null, values);
            Toast.makeText(this, "Фильм добавлен", Toast.LENGTH_SHORT).show();
        } else {
            db.update("Films", values, "Id = ?", new String[]{String.valueOf(filmId)});
            Toast.makeText(this, "Фильм обновлён", Toast.LENGTH_SHORT).show();
        }

        db.close();
        finish();
    }
}
