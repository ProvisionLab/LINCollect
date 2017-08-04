package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

/**
 * Created by Evgeniy on 11.04.2017.
 */

public class RelationshipAnswer {

    @SerializedName("Id")
    private long id;

    private transient String userLinkId;

    @SerializedName("Companies")
    private Companies companies;

    @SerializedName("QuestionAnswers")
    private ArrayList<RelationshipQuestionAnswer> answers;

    @SerializedName("NQuestionAnswers")
    private ArrayList<RelationshipNodeQuestionAnswer> nodeAnswers;

    public RelationshipAnswer() {
    }

    public RelationshipAnswer(long id) {
        this.id = id;
    }

    public RelationshipAnswer(long id, String userLinkId, Companies companies, ArrayList<RelationshipQuestionAnswer> answers,
                              ArrayList<RelationshipNodeQuestionAnswer> nodeAnswers) {
        this.id = id;
        this.userLinkId = userLinkId;
        this.companies = companies;
        this.answers = answers;
        this.nodeAnswers = nodeAnswers;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public String getUserLinkId() {
        return userLinkId;
    }

    public void setUserLinkId(String userLinkId) {
        this.userLinkId = userLinkId;
    }

    public Companies getCompanies() {
        return companies;
    }

    public void setCompanies(Companies companies) {
        this.companies = companies;
    }

    public ArrayList<RelationshipQuestionAnswer> getAnswers() {
        return answers;
    }

    public void setAnswers(ArrayList<RelationshipQuestionAnswer> answers) {
        this.answers = answers;
    }

    public ArrayList<RelationshipNodeQuestionAnswer> getNodeAnswers() {
        return nodeAnswers;
    }

    public void setNodeAnswers(ArrayList<RelationshipNodeQuestionAnswer> nodeAnswers) {
        this.nodeAnswers = nodeAnswers;
    }

    @Override
    public String toString() {
        return "RelationshipAnswer{" +
                "id=" + id +
                ", userLinkId='" + userLinkId + '\'' +
                ", companies=" + companies +
                ", answers=" + answers +
                ", nodeAnswers=" + nodeAnswers +
                '}';
    }
}
