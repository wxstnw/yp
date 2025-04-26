package com.example.filmrental;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

import java.io.*;

public class DatabaseHelper extends SQLiteOpenHelper {

    public static final String DATABASE_NAME = "database.db";
    private static final int DATABASE_VERSION = 1;
    private final Context context;

    public DatabaseHelper(Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
        this.context = context;
        copyDatabaseIfNeeded();
    }

    private void copyDatabaseIfNeeded() {
        File dbFile = context.getDatabasePath(DATABASE_NAME);

        if (!dbFile.exists()) {
            dbFile.getParentFile().mkdirs();
            try (InputStream is = context.getAssets().open(DATABASE_NAME);
                 OutputStream os = new FileOutputStream(dbFile)) {

                byte[] buffer = new byte[1024];
                int length;
                while ((length = is.read(buffer)) > 0) {
                    os.write(buffer, 0, length);
                }

                os.flush();
                Log.d("DB_COPY", "База скопирована в: " + dbFile.getAbsolutePath());
            } catch (IOException e) {
                e.printStackTrace();
                Log.e("DB_COPY", "Ошибка копирования базы: " + e.getMessage());
            }
        } else {
            Log.d("DB_COPY", "База уже существует — копирование не требуется");
        }
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        // База уже готовая, ничего не создаём
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        // Миграция при необходимости
    }
}
