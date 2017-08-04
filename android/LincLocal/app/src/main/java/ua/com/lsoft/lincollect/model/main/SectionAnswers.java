package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

/**
 * Created by Evgeniy on 11.04.2017.
 */

public class SectionAnswers {

    @SerializedName("Id")
    private long id;

    private String userLinkId;

    @SerializedName("QuestionAnswers")
    private ArrayList<QuestionAnswer> answers;

    public SectionAnswers() {
    }

    public SectionAnswers(long id, ArrayList<QuestionAnswer> answers) {
        this.id = id;
        this.answers = answers;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public ArrayList<QuestionAnswer> getAnswers() {
        return answers;
    }

    public void setAnswers(ArrayList<QuestionAnswer> answers) {
        this.answers = answers;
    }

    @Override
    public String toString() {
        return "SectionAnswers{" +
                "id=" + id +
                ", answers=" + answers +
                '}';
    }
}
