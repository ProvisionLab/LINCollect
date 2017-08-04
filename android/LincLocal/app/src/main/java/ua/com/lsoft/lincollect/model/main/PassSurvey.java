package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;

/**
 * Created by Evgeniy on 06.04.2017.
 */

public class PassSurvey implements Serializable {

    @SerializedName("SurveyId")
    private long surveyId;

    @SerializedName("UserLinkId")
    private String userLinkId;

    @SerializedName("SurveyName")
    private String surveyName;

    @SerializedName("AboutYouBefore")
    private Section sectionBefore;

    @SerializedName("AboutYouAfter")
    private Section sectionAfter;

    @SerializedName("Items")
    private ArrayList<Relationship> items;

    public PassSurvey() {
    }

    public PassSurvey(long surveyId, String userLinkId, String surveyName, Section sectionBefore, Section sectionAfter, ArrayList<Relationship> items) {
        this.surveyId = surveyId;
        this.userLinkId = userLinkId;
        this.surveyName = surveyName;
        this.sectionBefore = sectionBefore;
        this.sectionAfter = sectionAfter;
        this.items = items;
    }

    public long getSurveyId() {
        return surveyId;
    }

    public void setSurveyId(long surveyId) {
        this.surveyId = surveyId;
    }

    public String getUserLinkId() {
        return userLinkId;
    }

    public void setUserLinkId(String userLinkId) {
        this.userLinkId = userLinkId;
    }

    public String getSurveyName() {
        return surveyName;
    }

    public void setSurveyName(String surveyName) {
        this.surveyName = surveyName;
    }

    public Section getSectionBefore() {
        return sectionBefore;
    }

    public void setSectionBefore(Section sectionBefore) {
        this.sectionBefore = sectionBefore;
    }

    public Section getSectionAfter() {
        return sectionAfter;
    }

    public void setSectionAfter(Section sectionAfter) {
        this.sectionAfter = sectionAfter;
    }

    public ArrayList<Relationship> getItems() {
        return items;
    }

    public void setItems(ArrayList<Relationship> items) {
        this.items = items;
    }

    @Override
    public String toString() {
        return "PassSurvey{" +
                "surveyId=" + surveyId +
                ", userLinkId='" + userLinkId + '\'' +
                ", surveyName='" + surveyName + '\'' +
                ", sectionBefore=" + sectionBefore +
                ", sectionAfter=" + sectionAfter +
                ", items=" + items +
                '}';
    }
}
