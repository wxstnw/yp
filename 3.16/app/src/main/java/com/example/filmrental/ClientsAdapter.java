package com.example.filmrental;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;
import java.util.ArrayList;

public class ClientsAdapter extends RecyclerView.Adapter<ClientsAdapter.ClientViewHolder> {

    public interface OnClientClickListener {
        void onClientClick(Client client);
    }

    private ArrayList<Client> clients;
    private OnClientClickListener listener;

    public ClientsAdapter(ArrayList<Client> clients, OnClientClickListener listener) {
        this.clients = clients;
        this.listener = listener;
    }

    @NonNull
    @Override
    public ClientViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_client, parent, false);
        return new ClientViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ClientViewHolder holder, int position) {
        Client client = clients.get(position);
        holder.fullNameText.setText(client.fullName);
        holder.phoneText.setText("Телефон: " + client.phone);
        holder.emailText.setText("Email: " + client.email);

        holder.itemView.setOnClickListener(v -> listener.onClientClick(client));
    }

    @Override
    public int getItemCount() {
        return clients.size();
    }

    public static class ClientViewHolder extends RecyclerView.ViewHolder {
        TextView fullNameText, phoneText, emailText;

        public ClientViewHolder(@NonNull View itemView) {
            super(itemView);
            fullNameText = itemView.findViewById(R.id.textFullName);
            phoneText = itemView.findViewById(R.id.textPhone);
            emailText = itemView.findViewById(R.id.textEmail);
        }
    }
}
