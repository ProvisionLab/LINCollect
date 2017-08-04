package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

/**
 * Created by Evgeniy on 11.04.2017.
 */

public class AnswersModel {

    @SerializedName("UserLinkId")
    private String userLinkId;

    @SerializedName("AboutYouBefore")
    private SectionAnswers beforeSection;

    @SerializedName("AboutYouAfter")
    private SectionAnswers afterSection;

    @SerializedName("Items")
    private ArrayList<RelationshipAnswer> relationshipAnswer;

    public AnswersModel() {
    }

    public AnswersModel(String userLinkId, SectionAnswers beforeSection, SectionAnswers afterSection, ArrayList<RelationshipAnswer> relationshipAnswer) {
        this.userLinkId = userLinkId;
        this.beforeSection = beforeSection;
        this.afterSection = afterSection;
        this.relationshipAnswer = relationshipAnswer;
    }

    public String getUserLinkId() {
        return userLinkId;
    }

    public void setUserLinkId(String userLinkId) {
        this.userLinkId = userLinkId;
    }

    public SectionAnswers getBeforeSection() {
        return beforeSection;
    }

    public void setBeforeSection(SectionAnswers beforeSection) {
        this.beforeSection = beforeSection;
    }

    public SectionAnswers getAfterSection() {
        return afterSection;
    }

    public void setAfterSection(SectionAnswers afterSection) {
        this.afterSection = afterSection;
    }

    public ArrayList<RelationshipAnswer> getRelationshipAnswer() {
        return relationshipAnswer;
    }

    public void setRelationshipAnswer(ArrayList<RelationshipAnswer> relationshipAnswer) {
        this.relationshipAnswer = relationshipAnswer;
    }

    @Override
    public String toString() {
        return "AnswersModel{" +
                "userLinkId='" + userLinkId + '\'' +
                ", beforeSection=" + beforeSection +
                ", afterSection=" + afterSection +
                ", relationshipAnswer=" + relationshipAnswer +
                '}';
    }
}
