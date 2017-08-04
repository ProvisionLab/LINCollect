package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Evgeniy on 04.04.2017.
 */

public class User {

    @SerializedName("Username")
    private String username;

    @SerializedName("Token")
    private String token;

    public User() {
    }

    public User(String username, String token) {
        this.username = username;
        this.token = token;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }

    @Override
    public String toString() {
        return "User{" +
                "username='" + username + '\'' +
                ", token='" + token + '\'' +
                '}';
    }
}
