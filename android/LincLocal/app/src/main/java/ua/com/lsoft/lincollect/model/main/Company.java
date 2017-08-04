package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

/**
 * Created by Evgeniy on 11.05.2016.
 */
public class Company implements Serializable {

    private long relationshipId;

    @SerializedName("Name")
    private String name;

    @SerializedName("Checked")
    private boolean checked;

    public Company() {
    }

    public Company(String name, boolean checked) {
        this.name = name;
        this.checked = checked;
    }

    public long getRelationshipId() {
        return relationshipId;
    }

    public void setRelationshipId(long relationshipId) {
        this.relationshipId = relationshipId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public boolean isChecked() {
        return checked;
    }

    public void setChecked(boolean checked) {
        this.checked = checked;
    }

    @Override
    public String toString() {
        return "Company{" +
                "relationshipId=" + relationshipId +
                ", name='" + name + '\'' +
                ", checked=" + checked +
                '}';
    }
}
