package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Evgeniy on 04.04.2017.
 */

public class ResponseData<T> {

    @SerializedName("Data")
    private T data;

    @SerializedName("HttpResponse")
    private String code;

    public ResponseData() {
    }

    public ResponseData(T data, String code) {
        this.data = data;
        this.code = code;
    }

    public T getData() {
        return data;
    }

    public void setData(T data) {
        this.data = data;
    }

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }

    @Override
    public String toString() {
        return "ResponseData{" +
                "data=" + data +
                ", code='" + code + '\'' +
                '}';
    }
}
