package ua.com.lsoft.lincollect.model.main;

/**
 * Created by Evgeniy on 18.04.2017.
 */

public class NodeValue {

    private transient long questionId;
    private String value;
    private transient String company;

    public NodeValue() {
    }

    public NodeValue(long questionId, String value, String company) {
        this.questionId = questionId;
        this.value = value;
        this.company = company;
    }

    public long getQuestionId() {
        return questionId;
    }

    public void setQuestionId(long questionId) {
        this.questionId = questionId;
    }

    public String getValue() {
        return value;
    }

    public void setValue(String value) {
        this.value = value;
    }

    public String getCompany() {
        return company;
    }

    public void setCompany(String company) {
        this.company = company;
    }

    @Override
    public String toString() {
        return "NodeValue{" +
                "questionId=" + questionId +
                ", value='" + value + '\'' +
                ", company='" + company + '\'' +
                '}';
    }
}
