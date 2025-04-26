package com.example.filmrental;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;
import java.util.ArrayList;

public class RentalsAdapter extends RecyclerView.Adapter<RentalsAdapter.RentalViewHolder> {

    private ArrayList<Rental> rentals;
    private OnRentalClickListener clickListener;

    public interface OnRentalClickListener {
        void onRentalClick(Rental rental);
    }

    public RentalsAdapter(ArrayList<Rental> rentals) {
        this.rentals = rentals;
    }

    public RentalsAdapter(ArrayList<Rental> rentals, OnRentalClickListener clickListener) {
        this.rentals = rentals;
        this.clickListener = clickListener;
    }

    @NonNull
    @Override
    public RentalViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_rental, parent, false);
        return new RentalViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull RentalViewHolder holder, int position) {
        Rental rental = rentals.get(position);
        holder.clientText.setText("Клиент: " + rental.clientName);
        holder.filmText.setText("Фильм: " + rental.filmTitle);
        holder.startText.setText("Начало: " + rental.startDate);
        holder.endText.setText("Окончание: " + rental.endDate);

        holder.itemView.setOnClickListener(v -> {
            if (clickListener != null) {
                clickListener.onRentalClick(rental);
            }
        });
    }

    @Override
    public int getItemCount() {
        return rentals.size();
    }

    static class RentalViewHolder extends RecyclerView.ViewHolder {
        TextView clientText, filmText, startText, endText;

        public RentalViewHolder(@NonNull View itemView) {
            super(itemView);
            clientText = itemView.findViewById(R.id.textClient);
            filmText = itemView.findViewById(R.id.textFilm);
            startText = itemView.findViewById(R.id.textStart);
            endText = itemView.findViewById(R.id.textEnd);
        }
    }
}
