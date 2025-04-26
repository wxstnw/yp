package com.example.filmrental;

public class Film {
    public int id;
    public String title;
    public String genre;
    public int year;
    public String status;

    public Film(int id, String title, String genre, int year, String status) {
        this.id = id;
        this.title = title;
        this.genre = genre;
        this.year = year;
        this.status = status;
    }
}
