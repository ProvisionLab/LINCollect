package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;

/**
 * Created by Evgeniy on 04.04.2017.
 */

public class Question implements Serializable {

    @SerializedName("Id")
    private long id;

    @SerializedName("RespondentId")
    private long respondentId;

    @SerializedName("OrderId")
    private long orderId;

    @SerializedName("IsCompulsory")
    private boolean isCompulsory;

    @SerializedName("IsAfterSurvey")
    private boolean isAfterSurvey;

    @SerializedName("Introducing")
    private String intro;

    @SerializedName("ShortName")
    private String shortName;

    @SerializedName("Text")
    private String text;

    @SerializedName("QuestionFormatId")
    private long questionFormatId;

    @SerializedName("QuestionFormat")
    private QuestionFormat questionFormat;

    @SerializedName("Answers")
    private ArrayList<Answer> answers;

    @SerializedName("TextRowsCount")
    private int textRowsCount;

    @SerializedName("IsAnnotation")
    private String isAnnotation;

    @SerializedName("IsMultiple")
    private boolean isMultiple;

    @SerializedName("TextMin")
    private String textMin;

    @SerializedName("TextMax")
    private String textMax;

    @SerializedName("ValueMin")
    private String valueMin;

    @SerializedName("ValueMax")
    private String valueMax;

    @SerializedName("Resolution")
    private int resolution;

    @SerializedName("IsShowValue")
    private String isShowValue;

    @SerializedName("Rows")
    private String rows;

    public Question() {
    }

    public Question(long id, long respondentId, long orderId, boolean isCompulsory, boolean isAfterSurvey,
                    String intro, String shortName, String text, long questionFormatId, QuestionFormat questionFormat,
                    ArrayList<Answer> answers, int textRowsCount, String isAnnotation, boolean isMultiple,
                    String textMin, String textMax, String valueMin, String valueMax, int resolution, String isShowValue, String rows) {
        this.id = id;
        this.respondentId = respondentId;
        this.orderId = orderId;
        this.isCompulsory = isCompulsory;
        this.isAfterSurvey = isAfterSurvey;
        this.intro = intro;
        this.shortName = shortName;
        this.text = text;
        this.questionFormatId = questionFormatId;
        this.questionFormat = questionFormat;
        this.answers = answers;
        this.textRowsCount = textRowsCount;
        this.isAnnotation = isAnnotation;
        this.isMultiple = isMultiple;
        this.textMin = textMin;
        this.textMax = textMax;
        this.valueMin = valueMin;
        this.valueMax = valueMax;
        this.resolution = resolution;
        this.isShowValue = isShowValue;
        this.rows = rows;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public long getRespondentId() {
        return respondentId;
    }

    public void setRespondentId(long respondentId) {
        this.respondentId = respondentId;
    }

    public long getOrderId() {
        return orderId;
    }

    public void setOrderId(long orderId) {
        this.orderId = orderId;
    }

    public boolean isCompulsory() {
        return isCompulsory;
    }

    public void setCompulsory(boolean compulsory) {
        isCompulsory = compulsory;
    }

    public boolean isAfterSurvey() {
        return isAfterSurvey;
    }

    public void setAfterSurvey(boolean afterSurvey) {
        isAfterSurvey = afterSurvey;
    }

    public String getIntro() {
        return intro;
    }

    public void setIntro(String intro) {
        this.intro = intro;
    }

    public String getShortName() {
        return shortName;
    }

    public void setShortName(String shortName) {
        this.shortName = shortName;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public long getQuestionFormatId() {
        return questionFormatId;
    }

    public void setQuestionFormatId(long questionFormatId) {
        this.questionFormatId = questionFormatId;
    }

    public QuestionFormat getQuestionFormat() {
        return questionFormat;
    }

    public void setQuestionFormat(QuestionFormat questionFormat) {
        this.questionFormat = questionFormat;
    }

    public ArrayList<Answer> getAnswers() {
        return answers;
    }

    public void setAnswers(ArrayList<Answer> answers) {
        this.answers = answers;
    }

    public int getTextRowsCount() {
        return textRowsCount;
    }

    public void setTextRowsCount(int textRowsCount) {
        this.textRowsCount = textRowsCount;
    }

    public String getIsAnnotation() {
        return isAnnotation;
    }

    public void setIsAnnotation(String isAnnotation) {
        this.isAnnotation = isAnnotation;
    }

    public boolean isMultiple() {
        return isMultiple;
    }

    public void setMultiple(boolean multiple) {
        isMultiple = multiple;
    }

    public String getTextMin() {
        return textMin;
    }

    public void setTextMin(String textMin) {
        this.textMin = textMin;
    }

    public String getTextMax() {
        return textMax;
    }

    public void setTextMax(String textMax) {
        this.textMax = textMax;
    }

    public String getValueMin() {
        return valueMin;
    }

    public void setValueMin(String valueMin) {
        this.valueMin = valueMin;
    }

    public String getValueMax() {
        return valueMax;
    }

    public void setValueMax(String valueMax) {
        this.valueMax = valueMax;
    }

    public int getResolution() {
        return resolution;
    }

    public void setResolution(int resolution) {
        this.resolution = resolution;
    }

    public String getIsShowValue() {
        return isShowValue;
    }

    public void setIsShowValue(String isShowValue) {
        this.isShowValue = isShowValue;
    }

    public String getRows() {
        return rows;
    }

    public void setRows(String rows) {
        this.rows = rows;
    }

    @Override
    public String toString() {
        return "Question{" +
                "id=" + id +
                ", respondentId=" + respondentId +
                ", orderId=" + orderId +
                ", isCompulsory=" + isCompulsory +
                ", isAfterSurvey=" + isAfterSurvey +
                ", intro='" + intro + '\'' +
                ", shortName='" + shortName + '\'' +
                ", text='" + text + '\'' +
                ", questionFormatId=" + questionFormatId +
                ", questionFormat=" + questionFormat +
                ", questionAnswers=" + answers +
                ", textRowsCount=" + textRowsCount +
                ", isAnnotation='" + isAnnotation + '\'' +
                ", isMultiple=" + isMultiple +
                ", textMin='" + textMin + '\'' +
                ", textMax='" + textMax + '\'' +
                ", valueMin='" + valueMin + '\'' +
                ", valueMax='" + valueMax + '\'' +
                ", resolution=" + resolution +
                ", isShowValue='" + isShowValue + '\'' +
                ", rows='" + rows + '\'' +
                '}';
    }
}
