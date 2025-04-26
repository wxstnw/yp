package com.example.filmrental;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;
import java.util.ArrayList;

public class FilmsAdapter extends RecyclerView.Adapter<FilmsAdapter.FilmViewHolder> {

    public interface OnFilmClickListener {
        void onFilmClick(Film film);
    }

    private ArrayList<Film> films;
    private OnFilmClickListener listener;

    public FilmsAdapter(ArrayList<Film> films, OnFilmClickListener listener) {
        this.films = films;
        this.listener = listener;
    }

    @NonNull
    @Override
    public FilmViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_film, parent, false);
        return new FilmViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull FilmViewHolder holder, int position) {
        Film film = films.get(position);
        holder.titleText.setText(film.title);
        holder.genreText.setText("Жанр: " + film.genre);
        holder.yearText.setText("Год: " + film.year);
        holder.statusText.setText("Статус: " + film.status);

        holder.itemView.setOnClickListener(v -> listener.onFilmClick(film));
    }

    @Override
    public int getItemCount() {
        return films.size();
    }

    static class FilmViewHolder extends RecyclerView.ViewHolder {
        TextView titleText, genreText, yearText, statusText;

        public FilmViewHolder(@NonNull View itemView) {
            super(itemView);
            titleText = itemView.findViewById(R.id.textTitle);
            genreText = itemView.findViewById(R.id.textGenre);
            yearText = itemView.findViewById(R.id.textYear);
            statusText = itemView.findViewById(R.id.textStatus);
        }
    }
}
