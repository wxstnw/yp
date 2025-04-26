package com.example.filmrental;

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

public class ReturnActivity extends AppCompatActivity {

    private RecyclerView recyclerView;
    private Button returnButton;

    private DatabaseHelper dbHelper;
    private ArrayList<Rental> rentalList = new ArrayList<>();
    private RentalsAdapter adapter;
    private int selectedRentalId = -1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_return);

        dbHelper = new DatabaseHelper(this);
        recyclerView = findViewById(R.id.recyclerViewReturn);
        returnButton = findViewById(R.id.buttonReturnFilm);

        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        adapter = new RentalsAdapter(rentalList, rental -> {
            selectedRentalId = rental.id;
            Log.d("RETURN_SELECT", "Выбран rentalId = " + selectedRentalId);
        });
        recyclerView.setAdapter(adapter);

        returnButton.setOnClickListener(view -> {
            try {
                returnFilm();
            } catch (Exception e) {
                Log.e("RETURN_ERROR", "Ошибка при возврате фильма", e);
                Toast.makeText(this, "Ошибка возврата: " + e.getMessage(), Toast.LENGTH_LONG).show();
            }
        });
    }

    @Override
    protected void onResume() {
        super.onResume();
        loadActiveRentals();
    }

    private void loadActiveRentals() {
        rentalList.clear();

        try (SQLiteDatabase db = dbHelper.getReadableDatabase();
             Cursor cursor = db.rawQuery(
                     "SELECT Rentals.Id, Clients.FullName, Films.Title, Rentals.StartDate, Rentals.EndDate " +
                             "FROM Rentals " +
                             "JOIN Clients ON Rentals.ClientId = Clients.Id " +
                             "JOIN Films ON Rentals.FilmId = Films.Id " +
                             "WHERE Rentals.Status = 'Активна'", null)) {

            while (cursor.moveToNext()) {
                rentalList.add(new Rental(
                        cursor.getInt(0),
                        cursor.getString(1),
                        cursor.getString(2),
                        cursor.getString(3),
                        cursor.getString(4)
                ));
            }

            adapter.notifyDataSetChanged();
        } catch (Exception e) {
            Log.e("RETURN_LOAD", "Ошибка при загрузке аренд", e);
            Toast.makeText(this, "Ошибка при загрузке аренд", Toast.LENGTH_SHORT).show();
        }
    }

    private void returnFilm() {
        if (selectedRentalId == -1) {
            Toast.makeText(this, "Выберите аренду для возврата", Toast.LENGTH_SHORT).show();
            return;
        }

        Log.d("RETURN_ACTION", "Оформляем возврат rentalId = " + selectedRentalId);

        try (SQLiteDatabase db = dbHelper.getWritableDatabase()) {
            db.beginTransaction();

            db.execSQL("UPDATE Rentals SET Status = 'Возвращена' WHERE Id = ?", new Object[]{selectedRentalId});
            db.execSQL("UPDATE Films SET Status = 'Доступен' WHERE Id = (SELECT FilmId FROM Rentals WHERE Id = ?)", new Object[]{selectedRentalId});

            db.setTransactionSuccessful();
            Toast.makeText(this, "Фильм возвращён", Toast.LENGTH_SHORT).show();

            db.endTransaction();
            selectedRentalId = -1;
            loadActiveRentals();
        } catch (Exception e) {
            Log.e("RETURN_SAVE", "Ошибка при возврате фильма", e);
            Toast.makeText(this, "Ошибка возврата: " + e.getMessage(), Toast.LENGTH_LONG).show();
        }
    }
}
