package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;

/**
 * Created by Evgeniy on 07.04.2017.
 */

public class Companies implements Serializable {

    @SerializedName("RelationshipId")
    private long relationshipId;

    @SerializedName("RelationshipName")
    private String relationshipName;

    @SerializedName("Names")
    private ArrayList<Company> companyNames;

    public Companies() {
    }

    public Companies(ArrayList<Company> companyNames) {
        this.companyNames = companyNames;
    }

    public Companies(long relationshipId, String relationshipName, ArrayList<Company> companyNames) {
        this.relationshipId = relationshipId;
        this.relationshipName = relationshipName;
        this.companyNames = companyNames;
    }

    public long getRelationshipId() {
        return relationshipId;
    }

    public void setRelationshipId(long relationshipId) {
        this.relationshipId = relationshipId;
    }

    public String getRelationshipName() {
        return relationshipName;
    }

    public void setRelationshipName(String relationshipName) {
        this.relationshipName = relationshipName;
    }

    public ArrayList<Company> getCompanyNames() {
        return companyNames;
    }

    public void setCompanyNames(ArrayList<Company> companyNames) {
        this.companyNames = companyNames;
    }

    @Override
    public String toString() {
        return "Companies{" +
                "relationshipId=" + relationshipId +
                ", relationshipName='" + relationshipName + '\'' +
                ", companyNames=" + companyNames +
                '}';
    }
}
