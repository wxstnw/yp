package com.example.filmrental;

import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import java.util.ArrayList;

public class ClientsActivity extends AppCompatActivity {

    private RecyclerView recyclerView;
    private Button addButton, editButton, deleteButton;

    private ArrayList<Client> clientList = new ArrayList<>();
    private ClientsAdapter adapter;
    private int selectedClientId = -1;

    private DatabaseHelper dbHelper;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_clients);

        dbHelper = new DatabaseHelper(this);

        recyclerView = findViewById(R.id.recyclerViewClients);
        addButton = findViewById(R.id.buttonAddClient);
        editButton = findViewById(R.id.buttonEditClient);
        deleteButton = findViewById(R.id.buttonDeleteClient);

        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        adapter = new ClientsAdapter(clientList, client -> selectedClientId = client.id);
        recyclerView.setAdapter(adapter);

        addButton.setOnClickListener(v -> {
            startActivity(new Intent(this, ClientEditActivity.class));
        });

        editButton.setOnClickListener(v -> {
            if (selectedClientId == -1) {
                Toast.makeText(this, "Выберите клиента", Toast.LENGTH_SHORT).show();
            } else {
                Intent intent = new Intent(this, ClientEditActivity.class);
                intent.putExtra("clientId", selectedClientId);
                startActivity(intent);
            }
        });

        deleteButton.setOnClickListener(v -> {
            if (selectedClientId == -1) {
                Toast.makeText(this, "Выберите клиента", Toast.LENGTH_SHORT).show();
            } else {
                SQLiteDatabase db = dbHelper.getWritableDatabase();
                db.execSQL("DELETE FROM Clients WHERE Id = ?", new Object[]{selectedClientId});
                db.close();
                Toast.makeText(this, "Клиент удалён", Toast.LENGTH_SHORT).show();
                selectedClientId = -1;
                loadClients();
            }
        });
    }

    @Override
    protected void onResume() {
        super.onResume();
        loadClients();
    }

    private void loadClients() {
        clientList.clear();
        SQLiteDatabase db = dbHelper.getReadableDatabase();
        Cursor cursor = db.rawQuery("SELECT Id, FullName, Phone, Email FROM Clients", null);
        while (cursor.moveToNext()) {
            clientList.add(new Client(
                    cursor.getInt(0),
                    cursor.getString(1),
                    cursor.getString(2),
                    cursor.getString(3)
            ));
        }
        cursor.close();
        db.close();
        adapter.notifyDataSetChanged();
    }
}
