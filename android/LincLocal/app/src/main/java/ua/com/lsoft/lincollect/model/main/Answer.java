package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

/**
 * Created by Evgeniy on 07.04.2017.
 */

public class Answer implements Serializable {

    @SerializedName("Id")
    private long id;

    @SerializedName("IsDefault")
    private boolean isDefault;

    @SerializedName("Text")
    private String text;

    @SerializedName("Value")
    private String value;

    public Answer() {
    }

    public Answer(long id, boolean isDefault, String text, String value) {
        this.id = id;
        this.isDefault = isDefault;
        this.text = text;
        this.value = value;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public boolean isDefault() {
        return isDefault;
    }

    public void setDefault(boolean aDefault) {
        isDefault = aDefault;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public String getValue() {
        return value;
    }

    public void setValue(String value) {
        this.value = value;
    }

    @Override
    public String toString() {
        return "Answer{" +
                "id=" + id +
                ", isDefault=" + isDefault +
                ", text='" + text + '\'' +
                ", value='" + value + '\'' +
                '}';
    }
}
