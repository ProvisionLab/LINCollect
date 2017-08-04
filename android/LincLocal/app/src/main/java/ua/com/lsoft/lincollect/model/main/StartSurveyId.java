package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Evgeniy on 06.04.2017.
 */

public class StartSurveyId {

    @SerializedName("Id")
    private long id;

    public StartSurveyId() {
    }

    public StartSurveyId(long id) {
        this.id = id;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    @Override
    public String toString() {
        return "StartSurveyId{" +
                "id=" + id +
                '}';
    }
}
