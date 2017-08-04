package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Evgeniy on 04.04.2017.
 */

public class ResponseResult {

    @SerializedName("Result")
    private User user;

    @SerializedName("HttpResponse")
    private String code;

    public ResponseResult() {
    }

    public ResponseResult(User user, String code) {
        this.user = user;
        this.code = code;
    }

    public User getUser() {
        return user;
    }

    public void setUser(User user) {
        this.user = user;
    }

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }

    @Override
    public String toString() {
        return "ResponseResult{" +
                "user=" + user +
                ", code='" + code + '\'' +
                '}';
    }
}
