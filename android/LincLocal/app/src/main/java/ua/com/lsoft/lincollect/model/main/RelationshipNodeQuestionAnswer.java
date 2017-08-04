package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

/**
 * Created by Evgeniy on 14-Apr-17.
 */

public class RelationshipNodeQuestionAnswer extends RelationshipQuestionAnswer {

    @SerializedName("CompanyName")
    private String companyName;

    public RelationshipNodeQuestionAnswer() {
    }

    public RelationshipNodeQuestionAnswer(long questionId, long relationshipId, ArrayList<String> values, String companyName) {
        super(questionId, relationshipId, values);
        this.companyName = companyName;
    }

    public String getCompanyName() {
        return companyName;
    }

    public void setCompanyName(String companyName) {
        this.companyName = companyName;
    }

    @Override
    public String toString() {
        return "RelationshipNodeQuestionAnswer{" +
                "companyName='" + companyName + '\'' +
                "} " + super.toString();
    }
}
