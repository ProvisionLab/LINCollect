package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

/**
 * Created by Evgeniy on 04.04.2017.
 */

public class QuestionAnswer {

    @SerializedName("QuestionId")
    private long questionId;

    private transient long sectionId;

    @SerializedName("Values")
    private ArrayList<String> values;

    public QuestionAnswer() {
    }

    public QuestionAnswer(long questionId, ArrayList<String> values) {
        this.questionId = questionId;
        this.values = values;
    }

    public long getQuestionId() {
        return questionId;
    }

    public void setQuestionId(long questionId) {
        this.questionId = questionId;
    }

    public long getSectionId() {
        return sectionId;
    }

    public void setSectionId(long sectionId) {
        this.sectionId = sectionId;
    }

    public ArrayList<String> getValues() {
        return values;
    }

    public void setValues(ArrayList<String> values) {
        this.values = values;
    }

    @Override
    public String toString() {
        return "QuestionAnswer{" +
                "questionId=" + questionId +
                ", sectionId=" + sectionId +
                ", values=" + values +
                '}';
    }
}
