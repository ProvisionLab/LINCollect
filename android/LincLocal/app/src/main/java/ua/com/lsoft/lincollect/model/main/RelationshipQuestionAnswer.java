package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

/**
 * Created by Evgeniy on 14-Apr-17.
 */

public class RelationshipQuestionAnswer {

    @SerializedName("QuestionId")
    private long questionId;

    private transient long relationshipId;

    @SerializedName("Values")
    private ArrayList<String> values;

    public RelationshipQuestionAnswer() {
    }

    public RelationshipQuestionAnswer(long questionId, long relationshipId) {
        this.questionId = questionId;
        this.relationshipId = relationshipId;
    }

    public RelationshipQuestionAnswer(long questionId, long relationshipId, ArrayList<String> values) {
        this.questionId = questionId;
        this.relationshipId = relationshipId;
        this.values = values;
    }

    public long getQuestionId() {
        return questionId;
    }

    public void setQuestionId(long questionId) {
        this.questionId = questionId;
    }

    public long getRelationshipId() {
        return relationshipId;
    }

    public void setRelationshipId(long relationshipId) {
        this.relationshipId = relationshipId;
    }

    public ArrayList<String> getValues() {
        return values;
    }

    public void setValues(ArrayList<String> values) {
        this.values = values;
    }

    @Override
    public String toString() {
        return "RelationshipQuestionAnswer{" +
                "questionId=" + questionId +
                ", relationshipId=" + relationshipId +
                ", values=" + values +
                '}';
    }
}
