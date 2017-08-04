package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

/**
 * Created by Evgeniy on 04.04.2017.
 */

public class QuestionFormat implements Serializable {

    @SerializedName("Id")
    private long id;

    @SerializedName("Name")
    private String name;

    @SerializedName("Code")
    private String code;

    public QuestionFormat() {
    }

    public QuestionFormat(long id, String name, String code) {
        this.id = id;
        this.name = name;
        this.code = code;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }

    @Override
    public String toString() {
        return "QuestionFormat{" +
                "id=" + id +
                ", name='" + name + '\'' +
                ", code='" + code + '\'' +
                '}';
    }
}
