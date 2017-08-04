package ua.com.lsoft.lincollect.activity;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.util.Patterns;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import ua.com.lsoft.lincollect.R;
import ua.com.lsoft.lincollect.db.SurveyDBHelper;
import ua.com.lsoft.lincollect.model.main.PassSurvey;
import ua.com.lsoft.lincollect.model.main.ResponseData;
import ua.com.lsoft.lincollect.model.main.StartSurvey;
import ua.com.lsoft.lincollect.model.main.StartSurveyId;
import ua.com.lsoft.lincollect.network.ApiService;
import ua.com.lsoft.lincollect.network.RetrofitApi;
import ua.com.lsoft.lincollect.utils.SharedPrefsUtil;

import static android.provider.Telephony.Carriers.BEARER;
import static ua.com.lsoft.lincollect.activity.AddCompanyActivity.ADDED_COMPANIES;
import static ua.com.lsoft.lincollect.activity.LoginActivity.TOKEN;

public class StartActivity extends AppCompatActivity {

    private EditText respondentNameEditText;
    private EditText respondentEmailEditText;
    private Button nextButton;
    private long id;
    private String name;
    private SurveyDBHelper surveyDBHelper;

    public static final String SURVEY = "survey";
    public static final String SURVEY_ID = "survey_id";
    public static final String SURVEY_NAME = "survey_name";
    public static final String USER_LINK_ID = "userLinkId";
    private static final String TAG = StartActivity.class.getSimpleName();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_start);

        surveyDBHelper = SurveyDBHelper.getInstance(this);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_HIDDEN);

        id = getIntent().getLongExtra(SURVEY_ID, -1);
        name = getIntent().getStringExtra(SURVEY_NAME);

        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayShowTitleEnabled(false);

        TextView titleTextView = (TextView) toolbar.findViewById(R.id.toolbar_title);
        titleTextView.setText(name);

        respondentNameEditText = (EditText) findViewById(R.id.respondent_name);
        respondentEmailEditText = (EditText) findViewById(R.id.respondent_email);
        nextButton = (Button) findViewById(R.id.nextBtn);

        nextButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (respondentNameEditText.getText().toString().isEmpty() || respondentEmailEditText.getText().toString().isEmpty()) {
                    Toast.makeText(StartActivity.this, getString(R.string.enter_name_or_email), Toast.LENGTH_LONG).show();
                } else if (!Patterns.EMAIL_ADDRESS.matcher(respondentEmailEditText.getText().toString()).matches()) {
                    Toast.makeText(StartActivity.this, getString(R.string.incorrect_email), Toast.LENGTH_LONG).show();
                } else {
                    StartSurvey startSurvey = new StartSurvey(id, respondentNameEditText.getText().toString(), respondentEmailEditText.getText().toString());
                    Log.d(TAG, "Start survey " + startSurvey);
                    findViewById(R.id.progress_layout).setVisibility(View.VISIBLE);
                    ApiService apiService = RetrofitApi.getInstance().getApiService();
                    Call<ResponseData<StartSurveyId>> getSurveyIdCall = apiService.getSurveyId(BEARER + " " +
                            SharedPrefsUtil.getStringData(StartActivity.this, TOKEN), startSurvey);
                    getSurveyIdCall.enqueue(new Callback<ResponseData<StartSurveyId>>() {
                        @Override
                        public void onResponse(Call<ResponseData<StartSurveyId>> call, Response<ResponseData<StartSurveyId>> response) {
                            findViewById(R.id.progress_layout).setVisibility(View.GONE);
                            if (response.isSuccessful()) {
                                ResponseData<StartSurveyId> responseData = response.body();
                                Log.d(TAG, "StartSurveyId " + response.body());
                                getSurvey(responseData.getData().getId());
                            } else {
                                Log.d(TAG, "Response error " + response.raw().toString());
                            }
                        }

                        @Override
                        public void onFailure(Call<ResponseData<StartSurveyId>> call, Throwable t) {
                            Log.d(TAG, "Error " + t.getMessage());
                            Toast.makeText(StartActivity.this, t.getMessage(), Toast.LENGTH_LONG).show();
                            findViewById(R.id.progress_layout).setVisibility(View.GONE);
                        }
                    });
                }
            }
        });
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        surveyDBHelper.clearAllTables();
        SharedPrefsUtil.clearValue(this, ADDED_COMPANIES);
    }

    private void getSurvey(long id) {
        findViewById(R.id.progress_layout).setVisibility(View.VISIBLE);
        ApiService apiService = RetrofitApi.getInstance().getApiService();
        Call<ResponseData<PassSurvey>> getSurveyIdCall = apiService.getSurveyById(BEARER + " " +
                SharedPrefsUtil.getStringData(this, TOKEN), id);
        getSurveyIdCall.enqueue(new Callback<ResponseData<PassSurvey>>() {
            @Override
            public void onResponse(Call<ResponseData<PassSurvey>> call, Response<ResponseData<PassSurvey>> response) {
                findViewById(R.id.progress_layout).setVisibility(View.GONE);
                if (response.isSuccessful()) {
                    final PassSurvey passSurvey = response.body().getData();
                    Log.d(TAG, "PassSurvey " + passSurvey);
                    if (passSurvey != null) {
                        SharedPrefsUtil.putStringData(StartActivity.this, USER_LINK_ID, passSurvey.getUserLinkId());
                        Intent intent = new Intent(StartActivity.this, QuizActivity.class);
                        intent.putExtra(SURVEY, passSurvey);
                        startActivity(intent);
                        finish();
                    }
                } else {
                    Log.d(TAG, "Response error " + response.raw().toString());
                }
            }

            @Override
            public void onFailure(Call<ResponseData<PassSurvey>> call, Throwable t) {
                Log.d(TAG, "Error " + t.getMessage());
                Toast.makeText(StartActivity.this, t.getMessage(), Toast.LENGTH_LONG).show();
                findViewById(R.id.progress_layout).setVisibility(View.GONE);
            }
        });
    }
}
