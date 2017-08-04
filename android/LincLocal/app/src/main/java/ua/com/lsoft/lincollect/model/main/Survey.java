package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

/**
 * Created by Evgeniy on 19.04.2016.
 */
public class Survey implements Serializable {

    @SerializedName("Id")
    private long id;

    @SerializedName("LanguageId")
    private long languageId;

    @SerializedName("Language")
    private String language;

    @SerializedName("Name")
    private String name;

    public Survey() {
    }

    public Survey(long id, long languageId, String language, String name) {
        this.id = id;
        this.languageId = languageId;
        this.language = language;
        this.name = name;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public long getLanguageId() {
        return languageId;
    }

    public void setLanguageId(long languageId) {
        this.languageId = languageId;
    }

    public String getLanguage() {
        return language;
    }

    public void setLanguage(String language) {
        this.language = language;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    @Override
    public String toString() {
        return "Survey{" +
                "id=" + id +
                ", languageId=" + languageId +
                ", language='" + language + '\'' +
                ", name='" + name + '\'' +
                '}';
    }
}
