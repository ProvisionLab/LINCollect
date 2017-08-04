package ua.com.lsoft.lincollect.activity;

import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import com.google.gson.Gson;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import ua.com.lsoft.lincollect.R;
import ua.com.lsoft.lincollect.db.SurveyDBHelper;
import ua.com.lsoft.lincollect.model.main.AnswersModel;
import ua.com.lsoft.lincollect.model.main.RelationshipAnswer;
import ua.com.lsoft.lincollect.model.main.ResponseData;
import ua.com.lsoft.lincollect.model.main.SectionAnswers;
import ua.com.lsoft.lincollect.network.ApiService;
import ua.com.lsoft.lincollect.network.RetrofitApi;
import ua.com.lsoft.lincollect.utils.SharedPrefsUtil;

import static android.provider.Telephony.Carriers.BEARER;
import static ua.com.lsoft.lincollect.activity.LoginActivity.TOKEN;
import static ua.com.lsoft.lincollect.activity.StartActivity.SURVEY_ID;
import static ua.com.lsoft.lincollect.activity.StartActivity.SURVEY_NAME;
import static ua.com.lsoft.lincollect.activity.StartActivity.USER_LINK_ID;
import static ua.com.lsoft.lincollect.fragments.PageFragment.JSON_AFTER;
import static ua.com.lsoft.lincollect.fragments.PageFragment.JSON_BEFORE;

public class SurveyEndActivity extends AppCompatActivity {

    private Button startNewButton;
    private Button chooseDifferentButton;
    private long surveyId;
    private String surveyName;
    private String respondentName;
    private String companyName;
    private SurveyDBHelper surveyDBHelper;
    private static final String TAG = SurveyEndActivity.class.getSimpleName();

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_survey_end);

        surveyDBHelper = SurveyDBHelper.getInstance(this);
        surveyId = getIntent().getLongExtra(SURVEY_ID, -1);
        surveyName = getIntent().getStringExtra(SURVEY_NAME);

        startNewButton = (Button) findViewById(R.id.start_new);
        chooseDifferentButton = (Button) findViewById(R.id.choose_dif);

        String userLinkId = SharedPrefsUtil.getStringData(this, USER_LINK_ID);
        SectionAnswers before = new Gson().fromJson(SharedPrefsUtil.getStringData(this, JSON_BEFORE), SectionAnswers.class);
        SectionAnswers after = new Gson().fromJson(SharedPrefsUtil.getStringData(this, JSON_AFTER), SectionAnswers.class);
        ArrayList<RelationshipAnswer> relationshipAnswers = surveyDBHelper.getRelationshipAnswers();
        Log.d(TAG, "RelationshipAnswer " + relationshipAnswers);

        AnswersModel answersModel = new AnswersModel();
        answersModel.setUserLinkId(userLinkId);
        answersModel.setBeforeSection(before);
        answersModel.setAfterSection(after);
        answersModel.setRelationshipAnswer(relationshipAnswers);
        Log.d(TAG, "AnswerModel " + new Gson().toJson(answersModel));

        findViewById(R.id.progress_layout).setVisibility(View.VISIBLE);
        ApiService apiService = RetrofitApi.getInstance().getApiService();
        Call<ResponseData> sendSurveyAnswersCall = apiService.sendSurveyAnswers(BEARER + " " +
                SharedPrefsUtil.getStringData(this, TOKEN), answersModel);
        sendSurveyAnswersCall.enqueue(new Callback<ResponseData>() {
            @Override
            public void onResponse(Call<ResponseData> call, Response<ResponseData> response) {
                findViewById(R.id.progress_layout).setVisibility(View.GONE);
                if (response.isSuccessful()) {
                    ResponseData responseData = response.body();
                    Log.d(TAG, "Send survey response" + responseData);
                } else {
                    Log.d(TAG, "Response error " + response.raw().toString());
                }
            }

            @Override
            public void onFailure(Call<ResponseData> call, Throwable t) {
                Log.d(TAG, "Error " + t.getMessage());
                Toast.makeText(SurveyEndActivity.this, t.getMessage(), Toast.LENGTH_LONG).show();
                findViewById(R.id.progress_layout).setVisibility(View.GONE);
            }
        });

        startNewButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(SurveyEndActivity.this, SurveysListActivity.class);
                startActivity(intent);
                finish();
            }
        });

        chooseDifferentButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(SurveyEndActivity.this, StartActivity.class);
                intent.putExtra(SURVEY_ID, surveyId);
                intent.putExtra(SURVEY_NAME, surveyName);
                startActivity(intent);
                finish();
            }
        });
    }

    @Override
    protected void onStop() {
        super.onStop();
        SharedPrefsUtil.clearValue(this, USER_LINK_ID);
        SharedPrefsUtil.clearValue(this, JSON_BEFORE);
        SharedPrefsUtil.clearValue(this, JSON_AFTER);
    }
}
