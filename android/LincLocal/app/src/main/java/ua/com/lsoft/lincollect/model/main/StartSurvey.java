package ua.com.lsoft.lincollect.model.main;

/**
 * Created by Evgeniy on 06.04.2017.
 */

public class StartSurvey {

    private long surveyId;
    private String username;
    private String userEmail;

    public StartSurvey() {
    }

    public StartSurvey(long surveyId, String username, String userEmail) {
        this.surveyId = surveyId;
        this.username = username;
        this.userEmail = userEmail;
    }

    public long getSurveyId() {
        return surveyId;
    }

    public void setSurveyId(long surveyId) {
        this.surveyId = surveyId;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getUserEmail() {
        return userEmail;
    }

    public void setUserEmail(String userEmail) {
        this.userEmail = userEmail;
    }

    @Override
    public String toString() {
        return "StartSurvey{" +
                "surveyId=" + surveyId +
                ", username='" + username + '\'' +
                ", userEmail='" + userEmail + '\'' +
                '}';
    }
}
