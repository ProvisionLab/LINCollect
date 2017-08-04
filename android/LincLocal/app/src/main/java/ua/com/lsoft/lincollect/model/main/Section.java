package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;

/**
 * Created by Evgeniy on 06.04.2017.
 */

public class Section implements Serializable {

    @SerializedName("Id")
    private long id;

    @SerializedName("SurveyId")
    private long surveyId;

    @SerializedName("IsAfterSurvey")
    private boolean isAfterSurvey;

    @SerializedName("Questions")
    private ArrayList<Question> questions;

    @SerializedName("QuestionAnswers")
    private ArrayList<QuestionAnswer> questionAnswers;

    public Section() {
    }

    public Section(long id, long surveyId, boolean isAfterSurvey, ArrayList<Question> questions, ArrayList<QuestionAnswer> questionAnswers) {
        this.id = id;
        this.surveyId = surveyId;
        this.isAfterSurvey = isAfterSurvey;
        this.questions = questions;
        this.questionAnswers = questionAnswers;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public long getSurveyId() {
        return surveyId;
    }

    public void setSurveyId(long surveyId) {
        this.surveyId = surveyId;
    }

    public boolean isAfterSurvey() {
        return isAfterSurvey;
    }

    public void setAfterSurvey(boolean afterSurvey) {
        isAfterSurvey = afterSurvey;
    }

    public ArrayList<Question> getQuestions() {
        return questions;
    }

    public void setQuestions(ArrayList<Question> questions) {
        this.questions = questions;
    }

    public ArrayList<QuestionAnswer> getQuestionAnswers() {
        return questionAnswers;
    }

    public void setQuestionAnswers(ArrayList<QuestionAnswer> questionAnswers) {
        this.questionAnswers = questionAnswers;
    }

    @Override
    public String toString() {
        return "Section{" +
                "id=" + id +
                ", surveyId=" + surveyId +
                ", isAfterSurvey=" + isAfterSurvey +
                ", questions=" + questions +
                ", questionAnswers=" + questionAnswers +
                '}';
    }
}
