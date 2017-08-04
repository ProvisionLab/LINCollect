package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Evgeniy on 04.04.2017.
 */

public class LoginModel {

    @SerializedName("Username")
    private String username;

    @SerializedName("Password")
    private String password;

    public LoginModel() {
    }

    public LoginModel(String username, String password) {
        this.username = username;
        this.password = password;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    @Override
    public String toString() {
        return "LoginModel{" +
                "username='" + username + '\'' +
                ", password='" + password + '\'' +
                '}';
    }
}
