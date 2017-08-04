package ua.com.lsoft.lincollect.model.main;

/**
 * Created by Evgeniy on 13-Apr-17.
 */

public class Value {

    private transient long questionId;
    private String value;

    public Value() {
    }

    public Value(long questionId, String value) {
        this.questionId = questionId;
        this.value = value;
    }

    public Value(String value) {
        this.value = value;
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

    @Override
    public String toString() {
        return "Value{" +
                "questionId=" + questionId +
                ", value='" + value + '\'' +
                '}';
    }
}
