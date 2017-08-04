package ua.com.lsoft.lincollect.model.additional;

import android.view.View;
import android.widget.TextView;

import java.util.ArrayList;

/**
 * Created by Evgeniy on 09.06.2016.
 */
public class ViewModel {

    private long questionId;
    private TextView questionText;
    private ArrayList<View> views;
    private String fieldType;
    private boolean isMandatory;

    public ViewModel() {
    }

    public ViewModel(long questionId, TextView questionText, ArrayList<View> views, String fieldType, boolean isMandatory) {
        this.questionId = questionId;
        this.questionText = questionText;
        this.views = views;
        this.fieldType = fieldType;
        this.isMandatory = isMandatory;
    }

    public long getQuestionId() {
        return questionId;
    }

    public void setQuestionId(long questionId) {
        this.questionId = questionId;
    }

    public TextView getQuestionText() {
        return questionText;
    }

    public void setQuestionText(TextView questionText) {
        this.questionText = questionText;
    }

    public ArrayList<View> getViews() {
        return views;
    }

    public void setViews(ArrayList<View> views) {
        this.views = views;
    }

    public String getFieldType() {
        return fieldType;
    }

    public void setFieldType(String fieldType) {
        this.fieldType = fieldType;
    }

    public boolean isMandatory() {
        return isMandatory;
    }

    public void setMandatory(boolean mandatory) {
        isMandatory = mandatory;
    }

    @Override
    public String toString() {
        return "ViewModel{" +
                "questionId=" + questionId +
                ", questionText=" + questionText +
                ", views=" + views +
                ", fieldType='" + fieldType + '\'' +
                ", isMandatory=" + isMandatory +
                '}';
    }
}
