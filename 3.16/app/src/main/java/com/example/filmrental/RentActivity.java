package com.example.filmrental;

import android.app.DatePickerDialog;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.*;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import java.util.*;

public class RentActivity extends AppCompatActivity {

    private Spinner clientSpinner, filmSpinner;
    private Button rentButton, startDateButton, endDateButton;
    private TextView startDateText, endDateText;
    private RecyclerView rentalsRecyclerView;

    private DatabaseHelper dbHelper;
    private ArrayList<Rental> rentalsList = new ArrayList<>();
    private RentalsAdapter adapter;

    private Calendar startDate = Calendar.getInstance();
    private Calendar endDate = Calendar.getInstance();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_rent);

        dbHelper = new DatabaseHelper(this);

        clientSpinner = findViewById(R.id.spinnerClient);
        filmSpinner = findViewById(R.id.spinnerFilm);
        rentButton = findViewById(R.id.buttonRent);
        startDateButton = findViewById(R.id.buttonStartDate);
        endDateButton = findViewById(R.id.buttonEndDate);
        startDateText = findViewById(R.id.textStartDate);
        endDateText = findViewById(R.id.textEndDate);
        rentalsRecyclerView = findViewById(R.id.recyclerViewRentals);

        rentalsRecyclerView.setLayoutManager(new LinearLayoutManager(this));
        adapter = new RentalsAdapter(rentalsList);
        rentalsRecyclerView.setAdapter(adapter);

        updateDateTexts();

        startDateButton.setOnClickListener(v -> pickDate(startDate, startDateText));
        endDateButton.setOnClickListener(v -> pickDate(endDate, endDateText));
        rentButton.setOnClickListener(v -> rentFilm());
    }

    @Override
    protected void onResume() {
        super.onResume();
        loadClients();
        loadAvailableFilms();
        loadRentals();
    }

    private void pickDate(Calendar date, TextView output) {
        DatePickerDialog dpd = new DatePickerDialog(this, (view, year, month, day) -> {
            date.set(year, month, day);
            updateDateTexts();
        }, date.get(Calendar.YEAR), date.get(Calendar.MONTH), date.get(Calendar.DAY_OF_MONTH));
        dpd.show();
    }

    private void updateDateTexts() {
        startDateText.setText(android.text.format.DateFormat.format("yyyy-MM-dd", startDate));
        endDateText.setText(android.text.format.DateFormat.format("yyyy-MM-dd", endDate));
    }

    private void loadClients() {
        try {
            ArrayList<String> clients = new ArrayList<>();
            ArrayList<Integer> clientIds = new ArrayList<>();

            SQLiteDatabase db = dbHelper.getReadableDatabase();
            Cursor cursor = db.rawQuery("SELECT Id, FullName FROM Clients", null);
            while (cursor.moveToNext()) {
                clientIds.add(cursor.getInt(0));
                clients.add(cursor.getString(1));
            }
            cursor.close();
            db.close();

            ArrayAdapter<String> adapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_dropdown_item, clients);
            clientSpinner.setAdapter(adapter);
            clientSpinner.setTag(clientIds);
        } catch (Exception e) {
            Log.e("RENT_CLIENTS", "Ошибка при загрузке клиентов", e);
            Toast.makeText(this, "Ошибка при загрузке клиентов", Toast.LENGTH_SHORT).show();
        }
    }

    private void loadAvailableFilms() {
        try {
            ArrayList<String> films = new ArrayList<>();
            ArrayList<Integer> filmIds = new ArrayList<>();

            SQLiteDatabase db = dbHelper.getReadableDatabase();
            Cursor cursor = db.rawQuery("SELECT Id, Title FROM Films WHERE Status = 'Доступен'", null);
            while (cursor.moveToNext()) {
                filmIds.add(cursor.getInt(0));
                films.add(cursor.getString(1));
            }
            cursor.close();
            db.close();

            ArrayAdapter<String> adapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_dropdown_item, films);
            filmSpinner.setAdapter(adapter);
            filmSpinner.setTag(filmIds);
        } catch (Exception e) {
            Log.e("RENT_FILMS", "Ошибка при загрузке фильмов", e);
            Toast.makeText(this, "Ошибка при загрузке фильмов", Toast.LENGTH_SHORT).show();
        }
    }

    private void loadRentals() {
        try {
            rentalsList.clear();
            SQLiteDatabase db = dbHelper.getReadableDatabase();
            Cursor cursor = db.rawQuery(
                    "SELECT Rentals.Id, Clients.FullName, Films.Title, Rentals.StartDate, Rentals.EndDate " +
                            "FROM Rentals " +
                            "JOIN Clients ON Rentals.ClientId = Clients.Id " +
                            "JOIN Films ON Rentals.FilmId = Films.Id " +
                            "WHERE Rentals.Status = 'Активна' " +
                            "ORDER BY Rentals.StartDate DESC", null);
            while (cursor.moveToNext()) {
                rentalsList.add(new Rental(
                        cursor.getInt(0),
                        cursor.getString(1),
                        cursor.getString(2),
                        cursor.getString(3),
                        cursor.getString(4)
                ));
            }
            cursor.close();
            db.close();
            adapter.notifyDataSetChanged();
        } catch (Exception e) {
            Log.e("RENT_LOAD", "Ошибка при загрузке аренд", e);
        }
    }

    private void rentFilm() {
        try {
            ArrayList<Integer> clientIds = (ArrayList<Integer>) clientSpinner.getTag();
            ArrayList<Integer> filmIds = (ArrayList<Integer>) filmSpinner.getTag();

            int clientIndex = clientSpinner.getSelectedItemPosition();
            int filmIndex = filmSpinner.getSelectedItemPosition();

            if (clientIndex == -1 || filmIndex == -1) {
                Toast.makeText(this, "Выберите клиента и фильм", Toast.LENGTH_SHORT).show();
                return;
            }

            int clientId = clientIds.get(clientIndex);
            int filmId = filmIds.get(filmIndex);

            if (endDate.before(startDate)) {
                Toast.makeText(this, "Дата окончания не может быть раньше начала", Toast.LENGTH_SHORT).show();
                return;
            }

            SQLiteDatabase db = dbHelper.getWritableDatabase();
            db.beginTransaction();
            db.execSQL("INSERT INTO Rentals (ClientId, FilmId, StartDate, EndDate, Status) VALUES (?, ?, ?, ?, 'Активна')",
                    new Object[]{clientId, filmId,
                            android.text.format.DateFormat.format("yyyy-MM-dd", startDate),
                            android.text.format.DateFormat.format("yyyy-MM-dd", endDate)});
            db.execSQL("UPDATE Films SET Status = 'Недоступен' WHERE Id = ?", new Object[]{filmId});
            db.setTransactionSuccessful();
            db.endTransaction();
            db.close();

            Toast.makeText(this, "Аренда оформлена", Toast.LENGTH_SHORT).show();
            loadAvailableFilms();
            loadRentals();
        } catch (Exception e) {
            Log.e("RENT_SAVE", "Ошибка при сохранении аренды", e);
            Toast.makeText(this, "Ошибка при оформлении аренды: " + e.getMessage(), Toast.LENGTH_LONG).show();
        }
    }
}
