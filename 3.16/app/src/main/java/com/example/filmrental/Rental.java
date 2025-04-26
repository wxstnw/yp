package com.example.filmrental;

public class Rental {
    public int id;
    public String clientName;
    public String filmTitle;
    public String startDate;
    public String endDate;

    public Rental(int id, String clientName, String filmTitle, String startDate, String endDate) {
        this.id = id;
        this.clientName = clientName;
        this.filmTitle = filmTitle;
        this.startDate = startDate;
        this.endDate = endDate;
    }
}
